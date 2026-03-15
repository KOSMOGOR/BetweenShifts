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

    WaitForSeconds waitBetweenChars;
    bool dialogueDonePrinting = false;
    readonly List<DialogueChoiceButton> dialogueChoiceButtons = new();
    bool choiceNeeded = false;

    override protected void AwakeNew() {
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
        if (Player.I.playerState != PlayerState.Interacting) return;
        if (InputManager.ConsumeInteract() || Mouse.current.leftButton.wasPressedThisFrame) {
            if (dialogueDonePrinting && (!choiceNeeded || CurrentChoice != -1)) DialogueDone = true;
        }
    }

    void BaseStartDialogue() {
        dialogueTextField.text = "";
        dialogueRoot.gameObject.SetActive(true);
        dialogueDonePrinting = false;
        DialogueDone = false;
        choiceNeeded = false;
        CurrentChoice = -1;
        dialogueChoiceButtons.ForEach(button => {
            button.Reset();
            button.gameObject.SetActive(false);
        });
    }

    public void StartDialogue(string text) {
        BaseStartDialogue();
        StartCoroutine(PrintDialogue(text));
    }

    public void StartDialogueWithChoices(string text, List<string> choices) {
        BaseStartDialogue();
        choiceNeeded = true;
        StartCoroutine(PrintDialogueWithChoices(text, choices));
    }

    IEnumerator PrintDialogue(string text) {
        foreach (char ch in text) {
            dialogueTextField.text += ch;
            yield return waitBetweenChars;
        }
        dialogueDonePrinting = true;
    }

    IEnumerator PrintDialogueWithChoices(string text, List<string> choices) {
        yield return PrintDialogue(text);
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

    public void Hide() {
        dialogueRoot.gameObject.SetActive(false);
    }

    public void HideForAction(ActionBase action) {
        dialogueChoiceButtons.ForEach(button => {
            button.Reset();
            button.gameObject.SetActive(false);
        });
        if (action == null || action is not IDialogRenderable) Hide();
    }
}

public interface IDialogRenderable {}

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
    }

    public void SetChoice(string text, Action<int> action) {
        textField.text = text;
        button.onClick.AddListener(() => action(index));
    }
}
