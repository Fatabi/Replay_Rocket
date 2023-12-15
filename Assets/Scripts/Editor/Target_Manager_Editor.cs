using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Target_Manager))]
public class Target_Manager_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Target_Manager target_manager = (Target_Manager)target;
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Missiles"))
        {
            target_manager.CreateTargetPrefabs();
        }
        if (GUILayout.Button("Delete Missiles"))
        {
            target_manager.DeleteTargetPrefabs();
        }
        GUILayout.EndHorizontal();
    }
}