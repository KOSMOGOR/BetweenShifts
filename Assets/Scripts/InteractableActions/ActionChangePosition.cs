using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class ActionChangePosition : ActionBase
{
    public Vector3 newPosition;

    public override IEnumerator DoAction() {
        if (interactable != null) interactable.transform.position = newPosition;
        yield return null;
    }
}
