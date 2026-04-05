using System;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseInteractable : MonoBehaviour
{
    public int currentState = -1;
    public List<InteractableState> states;

    public virtual void Interact() {}

    protected void Awake() {
        if (states.Count > 0) currentState = 0;
    }

    public InteractableState GetCurrentState() {
        return states[currentState];
    }
    public void ChangeState(string stateName) {
        for (int i = 0; i < states.Count; i++)
            if (states[i].name == stateName) {
                currentState = i;
                return;
            }
        currentState = -1;
    }
}

[Serializable]
public class InteractableState
{
    public string name = "StateName";
    public bool terminal = false;
}
