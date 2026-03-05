using System;
using System.Collections;

[Serializable]
public class ActionEnd : ActionBase
{
    public override IEnumerator DoAction() {
        next = null;
        yield return null;
    }
}
