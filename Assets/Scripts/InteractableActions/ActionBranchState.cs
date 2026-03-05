using System;

[Serializable]
public class ActionBranchState : ActionBranchBase
{
    public string targetStateName = "State";

    override protected bool BranchCondition() {
        return interactable != null && interactable.GetCurrentState()?.name == targetStateName;
    }
}
