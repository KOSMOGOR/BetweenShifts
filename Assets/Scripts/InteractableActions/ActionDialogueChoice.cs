using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class ActionDialogueChoice : ActionBase, IDialogRenderable
{
    public string dialogueText = "Test dialogue choice";
    public List<ChoiceLabel> dialogueChoices = new() {new("Choice 1", "Label1"), new("Choice 2", "Label2")};

    public override IEnumerator DoAction() {
        DialogueRenderer.I.StartDialogueWithChoices(dialogueText, dialogueChoices.Select(choiceLabel => choiceLabel.choice).ToList());
        while (!DialogueRenderer.I.DialogueDone) yield return null;
        string targetLabel = dialogueChoices[DialogueRenderer.I.CurrentChoice].label;
        ActionBase nextAction = null;
        if (interactable is InteractableMultipleActions multipleActions) nextAction = multipleActions.FindLabelOrNext(targetLabel, this);
        next = nextAction;
        DialogueRenderer.I.HideForAction(next);
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