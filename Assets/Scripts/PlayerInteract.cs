using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public CollidersList interactCollider;

#pragma warning disable IDE0051, IDE0060
    void OnInteract(InputValue input) {
        print(interactCollider.GetObjects().Count);
    }
#pragma warning restore IDE0051, IDE0060
}
