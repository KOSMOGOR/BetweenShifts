using System;
using System.Collections;

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
            nextAction = multipleActions.FindLabelOrNext(targetLabel, this);
        }
        next = nextAction;
        yield return null;
    }
}
