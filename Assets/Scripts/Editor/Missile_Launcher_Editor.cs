using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Missile_Launcher_Manager))]
public class Missile_Launcher_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Missile_Launcher_Manager missile_manager = (Missile_Launcher_Manager)target;
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Launchers"))
        {
            missile_manager.CreateLaunchers();
        }
        if (GUILayout.Button("Delete Launchers"))
        {
            missile_manager.DeleteLaunchers();
        }
        GUILayout.EndHorizontal();
    }
}