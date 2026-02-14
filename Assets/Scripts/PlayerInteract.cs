using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public CollidersList interactColliders;

#pragma warning disable IDE0051, IDE0060
    void OnInteract(InputValue input) {
        List<GameObject> interactableObjects = interactColliders.GetObjects();
        if (interactableObjects.Count == 0) return;
        interactableObjects
            // .Where(obj => obj.TryGetComponent<InteractableObject>(out var io))
            .OrderBy(obj => Vector3.Distance(obj.transform.position, transform.position))
            .First()
            .GetComponent<InteractableObject>().Interact();
    }
#pragma warning restore IDE0051, IDE0060
}
