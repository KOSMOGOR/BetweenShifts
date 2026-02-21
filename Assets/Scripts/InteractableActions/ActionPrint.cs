using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class ActionPrint : InteractableAction
{
    public string stringToPrint = "Test";

    public override IEnumerator DoAction() {
        Debug.Log(stringToPrint);
        yield return null;
    }
}
