using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(find_top_edge))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        find_top_edge myScript = (find_top_edge)target;
        if (GUILayout.Button("save rotation"))
        {
            myScript.save_rotation();
        }
    }
}