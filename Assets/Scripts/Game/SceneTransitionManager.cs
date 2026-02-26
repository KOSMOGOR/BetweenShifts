using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : SingletonMonoBehaviour<SceneTransitionManager>
{
    static readonly WaitForSeconds transitionWait = new(1f);

    public void StartSceneTransition(string scene, int targetDoorNum) {
        StartCoroutine(SceneTransitionCoroutine(scene, targetDoorNum));
    }

    IEnumerator SceneTransitionCoroutine(string scene, int targetDoorNum) {
        Player.I.playerState = PlayerState.Cutscene;
        yield return transitionWait;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone) yield return null;
        InteractableDoor door = FindObjectsByType<InteractableDoor>(FindObjectsSortMode.None).Where(door => door.thisDoorNum == targetDoorNum).FirstOrDefault();
        if (door != null) Player.I.transform.position = door.playerPosition.position;
        yield return transitionWait;
        Player.I.playerState = PlayerState.Free;
    }
}
