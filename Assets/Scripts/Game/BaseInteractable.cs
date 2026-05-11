using System;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseInteractable : MonoBehaviour
{
    public int currentState = -1;
    public List<InteractableState> states;

    public virtual void Interact() {}

    protected virtual void Awake() {
        if (states.Count > 0) currentState = 0;
    }

    public InteractableState GetCurrentState() {
        return currentState >= 0 ? states[currentState] : null;
    }

    public void ChangeState(string stateName, bool checkForTerminal = true) {
        for (int i = 0; i < states.Count; i++)
            if (states[i].name == stateName) {
                currentState = i;
                if (checkForTerminal) CheckForTerminal();
                return;
            }
        currentState = -1;
    }

    public void CheckForTerminal() {
        InteractableState state = GetCurrentState();
        if (state != null && state.terminal) gameObject.SetActive(false);
    }
}

[Serializable]
public class InteractableState
{
    public string name = "StateName";
    public bool terminal = false;
}
