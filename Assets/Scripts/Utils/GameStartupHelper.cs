using System.Collections.Generic;
using UnityEngine;

public class GameStartupHelper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoad() {
        List<string> instantiatedNames = new();
        GameObjectList gameObjectList = Resources.Load<GameObjectList>("StartupGameObjects");
        if (gameObjectList != null) gameObjectList.gameObjects.ForEach(prefab => {
            GameObject gameObject = Object.Instantiate(prefab);
            instantiatedNames.Add(gameObject.name);
            if (gameObject.TryGetComponent(out Player player)) {
                GameObject playerPosition = GameObject.Find("PlayerDefaultPosition");
                if (playerPosition != null) gameObject.transform.position = playerPosition.transform.position;
            }
        });
        if (instantiatedNames.Count > 0) Debug.Log($"{nameof(GameStartupHelper)} has instantiated: {string.Join(", ", instantiatedNames)}");
        else Debug.Log($"{nameof(GameStartupHelper)} hasn't instantiated any object");
    }
}