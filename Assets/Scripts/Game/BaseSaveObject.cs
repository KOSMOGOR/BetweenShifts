using UnityEngine;

abstract public class BaseSaveObject : MonoBehaviour
{
    void Start() {
        Load();
    }

    void OnDestroy() {
        SaveManager.I.SaveObject(this);
    }

    virtual public string GetKey() {
        return $"{gameObject.scene.name}.{gameObject.name}";
    }

    abstract public string GetSaveData();

    public void Load() {
        string saveData = SaveManager.I.GetSaveData(GetKey());
        if (!string.IsNullOrEmpty(saveData)) LoadInternal(saveData);
    }
    abstract protected void LoadInternal(string saveData);
}
