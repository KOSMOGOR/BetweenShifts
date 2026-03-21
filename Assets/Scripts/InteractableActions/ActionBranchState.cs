using System;

[Serializable]
public class ActionBranchState : ActionBranchTrueFalse
{
    public string targetStateName = "State";

    protected override bool BranchCondition() {
        return interactable != null && interactable.GetCurrentState()?.name == targetStateName;
    }
}
