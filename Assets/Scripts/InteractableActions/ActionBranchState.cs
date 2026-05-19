using System;

[Serializable]
public class ActionBranchState : ActionBranchTrueFalse
{
    public string targetStateName = "State";

    protected override bool BranchCondition() {
        UnityEngine.Debug.Log(interactable.GetCurrentState());
        UnityEngine.Debug.Log(interactable.GetCurrentState()?.name);
        return interactable != null && interactable.GetCurrentState()?.name == targetStateName;
    }
}
