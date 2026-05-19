public class InteractableObject : InteractableMultipleActions, IPlayerInteractable
{
    protected override void Awake() {
        base.Awake();
        Outline outline = gameObject.AddComponent<Outline>();
        outline.enabled = false;
    }
}
