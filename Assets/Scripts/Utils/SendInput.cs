using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class SendInput : SingletonMonoBehaviour<SendInput>
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
