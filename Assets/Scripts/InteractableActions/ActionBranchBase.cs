using System;
using System.Collections;
using System.Linq;

[Serializable]
abstract public class ActionBranchBase : ActionBase
{
    public string trueLabel;
    public string falseLabel;

    abstract protected bool BranchCondition();

    public override IEnumerator DoAction() {
        ActionBase nextAction = null;
        if (interactable is InteractableMultipleActions multipleActions) {
            string targetLabel = BranchCondition() ? trueLabel : falseLabel;
            if (targetLabel == "Next") nextAction = next;
            else
                nextAction = multipleActions.actions
                    .Where(action => action is ActionLabel)
                    .FirstOrDefault(action => (action as ActionLabel).label == targetLabel);
        }
        next = nextAction;
        yield return null;
    }
}
