using System;
using System.Collections;

[Serializable]
public class ActionDialogueText : ActionBase, IDialogRenderable
{
    public string dialogueText = "Test dialogue";
    public DialogueCharacter dialogueSpeaker;

    public override IEnumerator DoAction() {
        DialogueCharacter speaker = dialogueSpeaker;
        if (speaker == null && interactable is InteractableMultipleActions) speaker = (interactable as InteractableMultipleActions).defaultDialogueCharacter;
        DialogueRenderer.I.StartDialogue(dialogueText, speaker);
        while (!DialogueRenderer.I.DialogueDone) yield return null;
        DialogueRenderer.I.HideForAction(next);
    }
}
