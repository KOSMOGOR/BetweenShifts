using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

abstract public class InteractableMultipleActions : BaseInteractable
{
    [SerializeReference, SubclassSelector] public List<ActionBase> actions;

    new void Awake() {
        base.Awake();
        for (int i = 0; i < actions.Count - 1; i++) {
            actions[i].next = actions[i + 1];
            actions[i].interactable = this;
        }
        actions[^1].interactable = this;
    }

    override public void Interact() {
        StartCoroutine(InteractCoroutine(PlayerState.Interacting));
    }

    IEnumerator InteractCoroutine(PlayerState newState, bool returnToStartState = true) {
        PlayerState startState = Player.I.playerState;
        Player.I.playerState = newState;
        ActionBase current = actions[0];
        while (current != null) {
            yield return current.DoAction();
            current = current.next;
        }
        if (returnToStartState) Player.I.playerState = startState;
    }

    public ActionBase FindLabel(string labelText) {
        if (labelText == "") return null;
        return actions
            .Where(action => action is ActionLabel)
            .FirstOrDefault(action => (action as ActionLabel).label == labelText);
    }

    public ActionBase FindLabelOrNext(string labelText, ActionBase action) {
        if (labelText == "Next") return action.next;
        return FindLabel(labelText);
    }
}
