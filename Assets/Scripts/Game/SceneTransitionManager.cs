using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : SingletonMonoBehaviour<SceneTransitionManager>
{
    public float transitionTime = 1f;
    public Image screenOverlay;

    protected override void AwakeNew() {
        Color color = screenOverlay.color;
        color.a = 0;
        screenOverlay.color = color;
    }

    public void StartSceneTransition(string scene, int targetDoorNum) {
        StartCoroutine(SceneTransitionCoroutine(scene, targetDoorNum));
    }

    IEnumerator SceneTransitionCoroutine(string scene, int targetDoorNum) {
        float timePassed = 0;
        Player.I.playerState = PlayerState.Cutscene;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        asyncLoad.allowSceneActivation = false;
        Color color = screenOverlay.color;
        color.a = 0;
        screenOverlay.color = color;
        timePassed = 0;
        while (timePassed < transitionTime) {
            timePassed += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, timePassed / transitionTime);
            screenOverlay.color = color;
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
        while (!asyncLoad.isDone) yield return null;
        InteractableDoor door = FindObjectsByType<InteractableDoor>(FindObjectsSortMode.None).Where(door => door.thisDoorNum == targetDoorNum).FirstOrDefault();
        if (door != null) {
            Player.I.transform.position = door.playerPosition.position;
            Player.I.GetComponent<PlayerMovement>().StickToGround();
        }
        timePassed = 0;
        while (timePassed < transitionTime) {
            timePassed += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, timePassed / transitionTime);
            screenOverlay.color = color;
            yield return null;
        }
        Player.I.playerState = PlayerState.Free;
    }
}
