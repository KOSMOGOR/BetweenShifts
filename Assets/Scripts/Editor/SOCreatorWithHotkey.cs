using UnityEngine;
using UnityEditor;
using System.IO;

public static class SOCreatorWithHotkey
{
    // puts new SO in rename mode. name it, don't press Enter, press hotkey again to save it and create a new one
    static readonly bool isInRenameMode = true;

    // % = ctrl, # = shift, & = alt, _ = (no modifier key).
    [MenuItem("Assets/Create/SO _F11", false, priority = 65)]
    static void CreateScriptableObject() {
        Object obj = Selection.activeObject;
        if (obj != null && obj.GetType() == typeof(MonoScript)) {
            var script = obj as MonoScript;
            if (script != null && script.GetClass().IsSubclassOf(typeof(ScriptableObject))) {
                var asset = ScriptableObject.CreateInstance(script.GetClass());
                string path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(script));
                if (isInRenameMode) {
                    ProjectWindowUtil.CreateAsset(asset, $"{path}/{script.name}.asset");
                    // Selection.activeObject = obj;
                } else {
                    AssetDatabase.CreateAsset(asset, $"{path}/{script.name}.asset");
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    Selection.activeObject = asset;
                }
            }
        }
    }
}
