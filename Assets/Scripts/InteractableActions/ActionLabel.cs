using System;
using System.Collections;

[Serializable]
public class ActionLabel : ActionBase
{
    public string label = "Label";

    public override IEnumerator DoAction() {
        yield return null;
    }
}
