using System.Collections.Generic;
using UnityEngine;

public class SaveManager : SingletonMonoBehaviour<SaveManager>
{
    readonly Dictionary<string, string> saveData = new();

    public void SaveObject(BaseSaveObject saveObject) {
        string sd = saveObject.GetSaveData();
        if (!string.IsNullOrEmpty(sd)) saveData[saveObject.GetKey()] = sd;
    }

    public string GetSaveData(string key) {
        if (saveData.TryGetValue(key, out string sd)) return sd;
        return null;
    }

    public void SaveAllObjects() {
        foreach (BaseSaveObject so in FindObjectsByType<BaseSaveObject>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            SaveObject(so);
    }
}
