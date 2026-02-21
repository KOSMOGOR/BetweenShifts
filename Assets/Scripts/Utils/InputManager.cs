using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(PlayerInput))]
public class InputManager : SingletonMonoBehaviour<InputManager>
{
    public List<GameObject> subscribedObjects = new();

    protected override void AwakeNew() {
        subscribedObjects = new();
    }

    void OnMove(InputValue input) { subscribedObjects.ForEach(obj => obj.SendMessage(nameof(OnMove), input, SendMessageOptions.DontRequireReceiver)); }
    void OnInteract(InputValue input) { subscribedObjects.ForEach(obj => obj.SendMessage(nameof(OnInteract), input, SendMessageOptions.DontRequireReceiver)); }

    public void Subscribe(GameObject obj) {
        subscribedObjects.Add(obj);
    }

    public void Unsubscribe(GameObject obj) {
        subscribedObjects.Remove(obj);
    }
}
