using UnityEngine;

public class InteractableDoor : BaseInteractable, IPlayerInteractable
{
    [Header("Target")]
    public SceneField targetScene;
    public int targetDoorNum = 0;
    [Header("This")]
    public int thisDoorNum = 0;
    public Transform playerPosition;

    protected override void Awake() {
        base.Awake();
        Outline outline = gameObject.AddComponent<Outline>();
        outline.enabled = false;
    }

    public override void Interact() {
        if (!targetScene.HasScene()) return;
        SceneTransitionManager.I.StartSceneTransition(targetScene, targetDoorNum);
        SoundManager.I.PlaySound(GameSound.Door);
    }
}
