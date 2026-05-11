using System;
using System.Collections;

[Serializable]
public class ActionChangeAnotherInteractableState : ActionBase
{
    public BaseInteractable interactableToChange;
    public string newStateName = "State";

    public override IEnumerator DoAction() {
        if (interactableToChange != null) interactableToChange.ChangeState(newStateName);
        yield return null;
    }
}
