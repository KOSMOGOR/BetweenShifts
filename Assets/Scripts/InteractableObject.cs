using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public List<InteractableObjectAction> actions = new();

    public void Interact() {
        actions.ForEach(action => action.OnInteract());
    }
}
