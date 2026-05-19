using System;
using System.Collections;

[Serializable]
abstract public class ActionBase
{
    [NonSerialized] public ActionBase next;
    [NonSerialized] public BaseInteractable interactable;

    abstract public IEnumerator DoAction();
}
