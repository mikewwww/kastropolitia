using UnityEngine;
using UnityEditor;

public class FlattenTreeHierarchy : MonoBehaviour
{
    [MenuItem("Tools/Trees/Smart Flatten Tree Hierarchy")]
    public static void Flatten()
    {
        GameObject treesRoot = GameObject.Find("trees");
        if (treesRoot == null)
        {
            Debug.LogWarning("Δεν βρέθηκε GameObject με όνομα 'trees'");
            return;
        }

        int moved = 0;
        Transform[] allChildren = treesRoot.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in allChildren)
        {
            if (child == treesRoot.transform)
                continue;

            // Αν το object είναι root-level ήδη, το αγνοούμε
            if (child.parent == treesRoot.transform)
                continue;

            // Αν είναι μέρος από LODGroup (π.χ. LOD0, LOD1...), το αφήνουμε
            if (child.GetComponentInParent<LODGroup>() != null &&
                child != child.GetComponentInParent<LODGroup>().transform)
                continue;

            // Αν ο parent έχει Particle System, το αφήνουμε
            if (child.parent.GetComponent<ParticleSystem>() != null)
                continue;

            // Αν το ίδιο το αντικείμενο έχει LODGroup ή ParticleSystem, το κρατάμε ως έχει
            if (child.GetComponent<LODGroup>() != null || child.GetComponent<ParticleSystem>() != null)
                continue;

            // ✅ Μετακινούμε
            child.SetParent(treesRoot.transform, true);
            moved++;
        }

        Debug.Log($"Μετακινήθηκαν {moved} αντικείμενα στο root του 'trees' χωρίς να σπάσουν LODs ή Particle Systems.");
    }
}
