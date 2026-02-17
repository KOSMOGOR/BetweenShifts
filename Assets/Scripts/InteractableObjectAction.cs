using System;
using System.Collections;

[Serializable]
abstract public class InteractableObjectAction
{
    [NonSerialized] public InteractableObjectAction next;

    abstract public IEnumerator DoAction();
}
