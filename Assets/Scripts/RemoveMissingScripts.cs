using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using System.Collections.Generic;

public class RemoveMissingScriptsInAllScenes
{
    [MenuItem("Tools/Cleanup/Remove Missing Scripts In ALL Scenes")]
    static void RemoveMissingScriptsAllScenes()
    {
        string[] scenePaths = Directory.GetFiles("Assets", "*.unity", SearchOption.AllDirectories);
        string currentScene = EditorSceneManager.GetActiveScene().path;

        int totalObjectsCleaned = 0;
        int totalScenesModified = 0;

        foreach (string scenePath in scenePaths)
        {
            var scene = EditorSceneManager.OpenScene(scenePath);
            int cleaned = 0;

            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                cleaned += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
            }

            if (cleaned > 0)
            {
                totalScenesModified++;
                totalObjectsCleaned += cleaned;
                EditorSceneManager.MarkSceneDirty(scene);
                EditorSceneManager.SaveScene(scene);
                Debug.Log($"🧹 Καθαρίστηκαν {cleaned} scripts στη σκηνή: {scenePath}");
            }
        }

        // Επιστροφή στην αρχική σκηνή
        if (!string.IsNullOrEmpty(currentScene))
            EditorSceneManager.OpenScene(currentScene);

        Debug.Log($"✅ Ολοκληρώθηκε. Καθαρίστηκαν {totalObjectsCleaned} scripts από {totalScenesModified} σκηνές.");
    }
}
