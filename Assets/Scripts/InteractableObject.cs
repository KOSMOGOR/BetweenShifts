using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    List<InteractableObjectAction> actions;

    void Start() {
        actions = GetComponents<InteractableObjectAction>().ToList();
    }

    public void Interact() {
        actions.ForEach(action => action.OnInteract());
    }
}
