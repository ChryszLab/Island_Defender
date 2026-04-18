using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoSaveEditor
{

    private static double lastSaveTime;
    private static double saveInterval = 300;
    private static bool autoSaveEnabled = true;

    static AutoSaveEditor()
    {
        lastSaveTime = EditorApplication.timeSinceStartup;
        EditorApplication.update += Update;
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void Update()
    {
        if (!autoSaveEnabled) return;
        if (EditorApplication.isPlayingOrWillChangePlaymode) return;

        if (EditorApplication.timeSinceStartup - lastSaveTime >= saveInterval)
        {
            SaveAll("Automatisches Intervall-Speichern");
            lastSaveTime = EditorApplication.timeSinceStartup;
        }
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (!autoSaveEnabled) return;

        if (state == PlayModeStateChange.ExitingEditMode)
        {
            SaveAll("Speichern vor Play Mode");
        }
    }

    private static void SaveAll(string reason)
    {
        Debug.Log($"[AutoSave] {reason}...");
        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();
    }
}
