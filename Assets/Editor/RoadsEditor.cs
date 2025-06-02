using UnityEngine;
using UnityEditor;
using Unity.Splines.Examples;

[CustomEditor(typeof(Roads))]
public class RoadsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Roads road = (Roads)target;

        if (GUILayout.Button("🔁 Rebuild Roads"))
        {
            road.RebuildRoads();
        }
    }
}
