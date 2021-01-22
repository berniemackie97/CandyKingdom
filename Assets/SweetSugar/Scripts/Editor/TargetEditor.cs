using UnityEditor;
using UnityEngine;

public class TargetEditor : EditorWindow
{
    private static TargetEditor window;
    TargetEditorScriptable targetObject;
    [MenuItem("Sweet Sugar/Target editor")]
    public static void Init()
    {

        // Get existing open window or if none, make a new one:
        window = (TargetEditor)GetWindow(typeof(TargetEditor));
        window.Show();
    }
    void OnEnable()
    {
        targetObject = AssetDatabase.LoadAssetAtPath("Assets/SweetSugar/Resources/Levels/TargetEditorScriptable.asset", typeof(TargetEditorScriptable)) as TargetEditorScriptable;
    }

    void OnFocus()
    {
        // var targetObjectPrefab = AssetDatabase.LoadAssetAtPath("Assets/SweetSugar/Resources/TargetEditor.prefab", typeof(GameObject)) as GameObject;
        // targetObject = targetObjectPrefab.GetComponent<TargetMono>();
    }

    void OnGUI()
    {
        GUILayout.Space(10);
        GuiList.Show(targetObject.targets, () => {        Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/SweetSugar/Resources/Levels/TargetEditorScriptable.asset");});
        GUILayout.Space(30);
        if (GUILayout.Button("Save"))
        {
            SaveSettings();
        }
    }

    void SaveSettings()
    {
        EditorUtility.SetDirty(targetObject);
        AssetDatabase.SaveAssets();
    }
}



