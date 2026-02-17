using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollidersList : MonoBehaviour
{
    public string withComponent;
    readonly Dictionary<GameObject, int> objects = new();

    Type componentType;

    void Start() {
        componentType = Type.GetType(withComponent);
        if (componentType == null) print($"There is no Type {withComponent}");
    }

    void OnTriggerEnter(Collider other) {
        if (componentType != null && other.GetComponent(componentType) == null) return;
        GameObject gm = other.gameObject;
        if (objects.ContainsKey(gm)) objects[gm]++;
        else objects.Add(gm, 1);
    }

    void OnTriggerExit(Collider other) {
        GameObject gm = other.gameObject;
        if (objects.TryGetValue(gm, out int value)) {
            if (value <= 1) objects.Remove(other.gameObject);
            else objects[gm]--;
        }
    }

    public List<GameObject> GetObjects() {
        return objects.Keys.ToList();
    }
}
