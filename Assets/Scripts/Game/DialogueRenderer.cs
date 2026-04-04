using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueRenderer : SingletonMonoBehaviour<DialogueRenderer>
{
    public Transform dialogueRoot;
    public TMP_Text dialogueTextField;
    public float charsPerSecond = 10;
    public Transform dialogueChoiceRoot;

    public bool DialogueDone { get; private set; } = false;
    public int CurrentChoice { get; private set; } = -1;

    bool dialogueActive = false;
    WaitForSeconds waitBetweenChars;
    bool dialogueDonePrinting = false;
    readonly List<DialogueChoiceButton> dialogueChoiceButtons = new();
    bool choiceNeeded = false;
    int availableChoices = 0;

    // current dialogue data
    string text;
    List<string> choices = new();
    Coroutine currentDialogueCoroutine;

    protected override void AwakeNew() {
        OnValidate();
        dialogueRoot.gameObject.SetActive(false);
        int index = 0;
        foreach (Transform child in dialogueChoiceRoot) {
            dialogueChoiceButtons.Add(new(index++, child));
        }
    }

    void OnValidate() {
        waitBetweenChars = charsPerSecond == 0 ? new(0) : new(1 / charsPerSecond);
    }

    void Update() {
        if (!dialogueActive) return;
        if (Keyboard.current.wKey.wasPressedThisFrame) ChangeChoiceDelta(-1);
        if (Keyboard.current.sKey.wasPressedThisFrame) ChangeChoiceDelta(1);
        if (InputManager.ConsumeInteract() || Mouse.current.leftButton.wasPressedThisFrame) {
            if (!dialogueDonePrinting && currentDialogueCoroutine != null) SkipPrint();
            else if (dialogueDonePrinting && (!choiceNeeded || CurrentChoice != -1)) DialogueDone = true;
        }
    }

    void BaseStartDialogue() {
        dialogueActive = true;
        dialogueTextField.text = "";
        dialogueRoot.gameObject.SetActive(true);
        dialogueDonePrinting = false;
        DialogueDone = false;
        choiceNeeded = false;
        CurrentChoice = -1;
        dialogueChoiceButtons.ForEach(button => button.Reset());
    }

    public void StartDialogue(string text) {
        BaseStartDialogue();
        this.text = text;
        choices.Clear();
        currentDialogueCoroutine = StartCoroutine(PrintDialogue());
    }

    public void StartDialogueWithChoices(string text, List<string> choices) {
        BaseStartDialogue();
        choiceNeeded = true;
        CurrentChoice = 0;
        availableChoices = choices.Count;
        dialogueChoiceButtons[0].SelectButton();
        this.text = text;
        this.choices = choices;
        currentDialogueCoroutine = StartCoroutine(PrintDialogueWithChoices());
    }

    IEnumerator PrintDialogue() {
        foreach (char ch in text) {
            dialogueTextField.text += ch;
            yield return waitBetweenChars;
        }
        dialogueDonePrinting = true;
    }

    IEnumerator PrintDialogueWithChoices() {
        yield return PrintDialogue();
        for (int i = 0; i < Mathf.Min(dialogueChoiceButtons.Count, choices.Count); i++) {
            DialogueChoiceButton dcb = dialogueChoiceButtons[i];
            dcb.gameObject.SetActive(true);
            dcb.SetChoice(choices[i], Choose);
        }
    }

    void SkipPrint() {
        if (currentDialogueCoroutine == null) return;
        StopCoroutine(currentDialogueCoroutine);
        currentDialogueCoroutine = null;
        dialogueTextField.text = text;
        dialogueDonePrinting = true;
        for (int i = 0; i < Mathf.Min(dialogueChoiceButtons.Count, choices.Count); i++) {
            DialogueChoiceButton dcb = dialogueChoiceButtons[i];
            dcb.gameObject.SetActive(true);
            dcb.SetChoice(choices[i], Choose);
        }
    }

    void Choose(int ind) {
        CurrentChoice = ind;
        DialogueDone = true;
    }

    void ChangeChoiceDelta(int delta) {
        CurrentChoice = (CurrentChoice - delta + availableChoices) % availableChoices;
        dialogueChoiceButtons[CurrentChoice].SelectButton();
    }

    public void Hide() {
        dialogueRoot.gameObject.SetActive(false);
        dialogueActive = false;
    }

    public void HideForAction(ActionBase action) {
        dialogueChoiceButtons.ForEach(button => button.Reset());
        if (action == null || action is not IDialogRenderable) Hide();
    }

    class DialogueChoiceButton {
        readonly int index;
        public readonly GameObject gameObject;
        readonly Button button;
        readonly TMP_Text textField;

        public DialogueChoiceButton(int index, Transform obj) {
            this.index = index;
            gameObject = obj.gameObject;
            button = obj.GetComponentInChildren<Button>();
            textField = obj.GetComponentInChildren<TMP_Text>();
        }

        public void Reset() {
            button.onClick.RemoveAllListeners();
            textField.text = "";
            gameObject.SetActive(false);
        }

        public void SetChoice(string text, Action<int> action) {
            textField.text = text;
            button.onClick.AddListener(() => action(index));
        }

        public void SelectButton() {
            button.Select();
        }
    }
}

public interface IDialogRenderable {}
