using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class ActionDialogueText : ActionBase, IDialogRenderable
{
    [TextArea]
    public string dialogueText = "Test dialogue";
    public DialogueCharacter dialogueCharacter;

    public override IEnumerator DoAction() {
        DialogueCharacter character = dialogueCharacter;
        if (character == null && interactable is InteractableMultipleActions) character = (interactable as InteractableMultipleActions).defaultDialogueCharacter;
        DialogueRenderer.I.StartDialogue(dialogueText, character);
        while (!DialogueRenderer.I.DialogueDone) yield return null;
        DialogueRenderer.I.HideForAction(next);
    }
}
