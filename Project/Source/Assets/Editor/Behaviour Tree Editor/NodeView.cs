using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> OnNodeSelected;

    public Node node;
    public Port inputPort;
    public Port outputPort;

    public NodeView(Node node) : base("Assets/Editor/Behaviour Tree Editor/NodeView.uxml")
    {
        if (node == null) return;

        this.node = node;
        this.title = node.name;
        this.viewDataKey = node.guid;
        style.left = node.position.x;
        style.top = node.position.y;

        CreateInputPorts();
        CreateOutputPorts();
        SetupClasses();

        Label descriptionLabel = this.Q<Label>("description");
        descriptionLabel.bindingPath = "description";
        descriptionLabel.Bind(new SerializedObject(node));
    }

    private void SetupClasses()
    {
        if (node is ActionNode)
            AddToClassList("action");
        else if (node is CompositeNode)
            AddToClassList("composite");
        else if (node is DecoratorNode)
            AddToClassList("decorator");
        else if (node is RootNode)
            AddToClassList("root");
    }

    private void CreateInputPorts()
    {
        if (node is ActionNode)
            inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        else if (node is CompositeNode)
            inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        else if (node is DecoratorNode)
            inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));

        if (inputPort != null)
        {
            inputPort.portName = "";
            inputPort.style.flexDirection = FlexDirection.Column;
            inputContainer.Add(inputPort);
        }
    }

    private void CreateOutputPorts()
    {
        //if (node is ActionNode) { }
        //    inputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
        if (node is CompositeNode)
            outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
        else if (node is DecoratorNode)
            outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        else if (node is RootNode)
            outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));

        if (outputPort != null)
        {
            outputPort.portName = "";
            outputPort.style.flexDirection = FlexDirection.ColumnReverse;
            outputContainer.Add(outputPort);
        }
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);

        Undo.RecordObject(node, "Set Position");
        
        node.position.x = newPos.xMin;
        node.position.y = newPos.yMin;

        EditorUtility.SetDirty(node);
    }

    public override void OnSelected()
    {
        base.OnSelected();
        OnNodeSelected?.Invoke(this);
    }

    public void SortChildren()
    {
        if (node is CompositeNode composite)
        {
            composite.children.Sort(SortByHorizontalPosition);
        }
    }

    private int SortByHorizontalPosition(Node left, Node right)
    {
        return left.position.x < right.position.x ? -1 : 1;
    }

    public void Update()
    {
        RemoveFromClassList("running");
        RemoveFromClassList("failure");
        RemoveFromClassList("success");

        if (Application.isPlaying)
        {
            switch (node.state)
            {
                case Node.State.Running:
                    if (node.started)
                        AddToClassList("running");
                    break;
                case Node.State.Failure:
                    AddToClassList("failure");
                    break;
                case Node.State.Success:
                    AddToClassList("success");
                    break;
            }
        }
    }
}
