using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.Drawing.Printing;
public class GitEditor : EditorWindow
{
    private string commitMessage = "Update vom Unity Editor";
    [MenuItem("Tools/Git Editor")]
    public static void ShowDialog()
    {
        GitEditor window = GetWindow<GitEditor>("Git Editor");
        window.minSize = new Vector2(400, 300);
        window.Show();
    }
    void OnGUI()
    {
        GUILayout.Label("Git Steuerung", EditorStyles.boldLabel);

        if (GUILayout.Button("Git Status"))
        {
            RunGitCommand("status");
        }

        if (GUILayout.Button("Git Pull (Main)"))
        {
            RunGitCommand("pull origin main");
        }
        EditorGUILayout.Space();
        GUILayout.Label("Veränderungen hochladen", EditorStyles.boldLabel);

        // Textfeld für die Commit-Nachricht
        commitMessage = EditorGUILayout.TextField("Commit Nachricht", commitMessage);

        if (GUILayout.Button("Git Add, Commit & Push"))
        {
            if (EditorUtility.DisplayDialog("Git Push", "Möchtest du alle Änderungen pushen?", "Ja", "Abbrechen"))
            {
                RunGitCommand("add .");
                RunGitCommand($"commit -m \"{commitMessage}\"");
                RunGitCommand("push origin main");
            }
        }

    }

    void RunGitCommand(string command)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo("git", command);
        startInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(Application.dataPath);
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;

        Process process = Process.Start(startInfo);
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if(!string.IsNullOrEmpty(output))
            UnityEngine.Debug.Log("Git Output: " + output);
        

        if(!string.IsNullOrEmpty(error)) 
            UnityEngine.Debug.LogError("Git Error: " + error);

    }
}
