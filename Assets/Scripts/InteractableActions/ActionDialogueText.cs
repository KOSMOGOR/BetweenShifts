using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class ActionDialogueText : InteractableObjectAction
{
    public string dialogueText = "Test dialogue";

    public override IEnumerator DoAction() {
        DialogueRenderer.I.StartDialogue(dialogueText);
        while (!DialogueRenderer.I.DialogueDone) yield return null;
        if (next is not ActionDialogueText) DialogueRenderer.I.Hide();
    }
}
