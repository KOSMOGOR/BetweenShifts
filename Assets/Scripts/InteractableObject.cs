using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeReference, SubclassSelector] List<InteractableObjectAction> actions;

    void Awake() {
        for (int i = 0; i < actions.Count - 1; i++) {
            actions[i].next = actions[i + 1];
        }
    }

    public void Interact() {
        StartCoroutine(InteractCoroutine());
    }

    IEnumerator InteractCoroutine() {
        Player.I.playerState = PlayerState.Interacting;
        InteractableObjectAction current = actions[0];
        while (current != null) {
            yield return current.DoAction();
            current = current.next;
        }
        Player.I.playerState = PlayerState.Free;
    }
}
