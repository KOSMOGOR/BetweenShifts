using System;
using System.Collections;

[Serializable]
public class ActionDialogueText : ActionBase, IDialogRenderable
{
    public string dialogueText = "Test dialogue";

    public override IEnumerator DoAction() {
        DialogueRenderer.I.StartDialogue(dialogueText);
        while (!DialogueRenderer.I.DialogueDone) yield return null;
        DialogueRenderer.I.HideForAction(next);
    }
}
