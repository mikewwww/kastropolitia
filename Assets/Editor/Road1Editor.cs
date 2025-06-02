using UnityEngine;
using UnityEditor;
using Unity.Splines.Examples;

[CustomEditor(typeof(Road1))]
public class Road1Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Road1 road = (Road1)target;
        if (GUILayout.Button("🔁 Rebuild Roads"))
        {
            road.RebuildRoads();
        }
    }
}
