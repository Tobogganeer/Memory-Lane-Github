using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public class InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

    Editor editor;

    public InspectorView() { }

    internal void UpdateSelection(NodeView nodeView)
    {
        if (nodeView == null) return;

        Clear();

        UnityEngine.Object.DestroyImmediate(editor);
        editor = Editor.CreateEditor(nodeView.node);
        //IMGUIContainer container = new IMGUIContainer(editor.OnInspectorGUI);
        IMGUIContainer container = new IMGUIContainer(() => 
        { 
            if (editor.target)
                editor.OnInspectorGUI(); 
        });
        Add(container);
    }
}
