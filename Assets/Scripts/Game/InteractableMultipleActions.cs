using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class InteractableMultipleActions : BaseInteractable
{
    [SerializeReference, SubclassSelector] List<InteractableAction> actions;

    void Awake() {
        for (int i = 0; i < actions.Count - 1; i++) {
            actions[i].next = actions[i + 1];
        }
    }

    override public void Interact() {
        StartCoroutine(InteractCoroutine(PlayerState.Interacting));
    }

    IEnumerator InteractCoroutine(PlayerState newState, bool returnToStartState = true) {
        PlayerState startState = Player.I.playerState;
        Player.I.playerState = newState;
        InteractableAction current = actions[0];
        while (current != null) {
            yield return current.DoAction();
            current = current.next;
        }
        if (returnToStartState) Player.I.playerState = startState;
    }
}
