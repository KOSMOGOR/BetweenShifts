[UnityEngine.RequireComponent(typeof(BaseInteractable))]
public class SaveObjectState : BaseSaveObject
{
    BaseInteractable interactable;

    void Awake() {
        interactable = GetComponent<BaseInteractable>();
    }

    public override string GetSaveData() {
        if (interactable.currentState == -1 || interactable.currentState == 0) return null;
        return interactable.GetCurrentState().name;
    }

    protected override void LoadInternal(string saveData) {
        interactable.ChangeState(saveData);
        if (interactable.GetCurrentState().terminal) interactable.gameObject.SetActive(false);
    }
}
