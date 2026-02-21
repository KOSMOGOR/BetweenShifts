using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueRenderer : SingletonMonoBehaviour<DialogueRenderer>
{
    public Transform dialogueRoot;
    public TMP_Text dialogueTextField;
    public float charsPerSecond = 10;

    public bool DialogueDone { get; private set; } = false;

    WaitForSeconds waitBetweenChars;
    bool dialogueDonePrinting = false;

    override protected void AwakeNew() {
        dialogueRoot.gameObject.SetActive(false);
        // DontDestroyOnLoad(transform.parent.gameObject);
    }

    void OnEnable() { InputManager.I.Subscribe(gameObject); }
    void OnDisable() { InputManager.I.Unsubscribe(gameObject); }

    void OnValidate() {
        waitBetweenChars = new(1 / charsPerSecond);
    }

#pragma warning disable IDE0051, IDE0060
    void OnInteract(InputValue input) {
        if (dialogueDonePrinting) DialogueDone = true;
    }
#pragma warning restore IDE0051, IDE0060

    public void StartDialogue(string text) {
        DialogueDone = false;
        StartCoroutine(PrintDialogue(text));
    }

    IEnumerator PrintDialogue(string text) {
        dialogueTextField.text = "";
        dialogueRoot.gameObject.SetActive(true);
        dialogueDonePrinting = false;
        foreach (char ch in text) {
            dialogueTextField.text += ch;
            yield return waitBetweenChars;
        }
        dialogueDonePrinting = true;
    }

    public void Hide() {
        dialogueRoot.gameObject.SetActive(false);
    }

    public void HideForAction(InteractableAction action) {
        if (action == null || action is not IDialogRenderable) Hide();
    }
}

public interface IDialogRenderable {}
