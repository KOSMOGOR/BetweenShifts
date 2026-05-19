using System;
using System.Collections;

[Serializable]
public class ActionJumpToLabel : ActionBase
{
    public string targetLabel = "Label";

    public override IEnumerator DoAction() {
        if (interactable is InteractableMultipleActions multipleActions) {
            next = multipleActions.FindLabelOrNext(targetLabel, this);
        }
        yield return null;
    }
}
