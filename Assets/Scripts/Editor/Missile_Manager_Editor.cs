using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Missile_Manager))]
public class Missile_Manager_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Missile_Manager missile_manager = (Missile_Manager)target;
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Missiles"))
        {
            missile_manager.CreateMissilePrefabs();
        }
        if (GUILayout.Button("Delete Missiles"))
        {
            missile_manager.DeleteMissilePrefabs();
        }
        GUILayout.EndHorizontal();
    }
}