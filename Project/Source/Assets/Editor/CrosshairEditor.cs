using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Crosshair))]
public class CrosshairEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        DrawDefaultInspector();

        Crosshair crosshair = (Crosshair)target;

        EditorGUILayout.LabelField("Current Size Delta", crosshair.rectTransform.sizeDelta.ToString());

        if (GUILayout.Button("Apply Size"))
        {
            crosshair.ApplySize();
        }
    }
}
