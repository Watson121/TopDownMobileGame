using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelManager))]
public class UpdateLevelList : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelManager levelManager = (LevelManager)target;

        if(GUILayout.Button("Update Level Selection List"))
        {
            levelManager.ClearLevelList();
            levelManager.UpdateLevelList_UI();
        }
    }




}
