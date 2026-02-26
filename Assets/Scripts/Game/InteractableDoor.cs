using UnityEngine;

public class InteractableDoor : BaseInteractable, IPlayerInteractable
{
    [Header("Target")]
    public SceneField targetScene;
    public int targetDoorNum = 0;
    [Header("This")]
    public int thisDoorNum = 0;
    public Transform playerPosition;

    public override void Interact() {
        if (!targetScene.HasScene()) return;
        SceneTransitionManager.I.StartSceneTransition(targetScene, targetDoorNum);
    }
}
