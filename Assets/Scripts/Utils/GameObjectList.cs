using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectList", menuName = "Scriptable Objects/GameObjectList")]
public class GameObjectList : ScriptableObject
{
    public List<GameObject> gameObjects = new();
}
