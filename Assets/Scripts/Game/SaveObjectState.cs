[UnityEngine.RequireComponent(typeof(BaseInteractable))]
public class SaveObjectState : BaseSaveObject
{
    BaseInteractable interactable;

    void Awake() {
        interactable = GetComponent<BaseInteractable>();
    }

    override public string GetSaveData() {
        if (interactable.currentState == -1 || interactable.currentState == 0) return null;
        return interactable.GetCurrentState().name;
    }

    override protected void LoadInternal(string saveData) {
        interactable.ChangeState(saveData);
    }
}
