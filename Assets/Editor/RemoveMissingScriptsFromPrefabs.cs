using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq; // ✅ πρόσθεσε αυτό

public class RemoveMissingScriptsFromPrefabs
{
    [MenuItem("Tools/Cleanup/Remove Missing Scripts In All Prefabs")]
    static void CleanPrefabs()
    {
        string[] prefabPaths = Directory.GetFiles("Assets", "*.prefab", SearchOption.AllDirectories);
        int count = 0;

        foreach (string path in prefabPaths)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null)
                continue;

            GameObject instance = PrefabUtility.LoadPrefabContents(path);
            int removed = 0;

            GameObject[] children = instance.GetComponentsInChildren<Transform>(true)
                .Select(t => t.gameObject).ToArray();

            foreach (GameObject go in children)
            {
                removed += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
            }

            if (removed > 0)
            {
                PrefabUtility.SaveAsPrefabAsset(instance, path);
                Debug.Log($"🧹 Καθαρίστηκαν {removed} missing scripts στο prefab: {path}");
                count++;
            }

            PrefabUtility.UnloadPrefabContents(instance);
        }

        Debug.Log($"✅ Ολοκληρώθηκε. Καθαρίστηκαν missing scripts από {count} prefabs.");
    }
}
