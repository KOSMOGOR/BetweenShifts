using System;
using System.Collections;
using UnityEngine;

[Serializable]
abstract public class ActionBranchBase : ActionBase
{
    protected virtual IEnumerator BeforeBranch() { yield return null; }
    protected virtual IEnumerator AfterBranch() { yield return null; }
    abstract protected string SelectNextBranch();

    public override IEnumerator DoAction() {
        yield return BeforeBranch();
        ActionBase nextAction = null;
        if (interactable is InteractableMultipleActions multipleActions) {
            string targetLabel = SelectNextBranch();
            nextAction = multipleActions.FindLabelOrNext(targetLabel, this);
        }
        next = nextAction;
        yield return AfterBranch();
        yield return null;
    }
}
