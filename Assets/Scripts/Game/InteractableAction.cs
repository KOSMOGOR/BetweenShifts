using System;
using System.Collections;

[Serializable]
abstract public class InteractableAction
{
    [NonSerialized] public InteractableAction next;

    abstract public IEnumerator DoAction();
}
