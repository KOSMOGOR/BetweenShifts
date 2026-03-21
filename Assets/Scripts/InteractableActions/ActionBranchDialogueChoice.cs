using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class ActionBranchDialogueChoice : ActionBranchBase, IDialogRenderable
{
    public string dialogueText = "Test dialogue choice";
    public List<ChoiceLabel> dialogueChoices = new() {new("Choice 1", "Label1"), new("Choice 2", "Label2")};
    string targetLabel = null;

    protected override IEnumerator BeforeBranch() {
        DialogueRenderer.I.StartDialogueWithChoices(dialogueText, dialogueChoices.Select(choiceLabel => choiceLabel.choice).ToList());
        while (!DialogueRenderer.I.DialogueDone) yield return null;
        targetLabel = dialogueChoices[DialogueRenderer.I.CurrentChoice].label;
    }

    protected override IEnumerator AfterBranch() {
        DialogueRenderer.I.HideForAction(next);
        yield return null;
    }

    protected override string SelectNextBranch() {
        return targetLabel;
    }

    [Serializable]
    public class ChoiceLabel {
        public string choice;
        public string label;

        public ChoiceLabel(string choice, string label) {
            this.choice = choice;
            this.label = label;
        }
    }
}