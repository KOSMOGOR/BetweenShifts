using System;
using System.Collections;

[Serializable]
public class ActionChangeState : ActionBase
{
    public string newStateName = "State";

    public override IEnumerator DoAction() {
        if (interactable != null) interactable.ChangeState(newStateName);
        yield return null;
    }
}
