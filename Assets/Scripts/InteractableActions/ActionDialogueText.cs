using System;
using System.Collections;

[Serializable]
public class ActionDialogueText : ActionBase, IDialogRenderable
{
    public string dialogueText = "Test dialogue";
    public string dialogueSpeaker = "TestSpeaker";

    public override IEnumerator DoAction() {
        DialogueRenderer.I.StartDialogue(dialogueText, dialogueSpeaker);
        while (!DialogueRenderer.I.DialogueDone) yield return null;
        DialogueRenderer.I.HideForAction(next);
    }
}
