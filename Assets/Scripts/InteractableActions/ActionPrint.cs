using UnityEngine;

public class ActionPrint : InteractableObjectAction
{
    public string stringToPrint = "Test";

    public override void OnInteract() {
        Debug.Log(stringToPrint);
    }
}
