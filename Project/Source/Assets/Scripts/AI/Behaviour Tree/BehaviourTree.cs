using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = Node.State;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu()]
public class BehaviourTree : ScriptableObject
{
    public Node rootNode;
    public State treeState = State.Running;
    public List<Node> nodes = new List<Node>();
    public Blackboard blackboard = new Blackboard();

    public State Update()
    {
        if (treeState == State.Running)
            treeState = rootNode.Update();
        return treeState;
    }

#if UNITY_EDITOR
    //public Node CreateNode<T>() where T : Node
    //{
    //    Node node = ScriptableObject.CreateInstance<T>();
    //    node.name = node.GetType().Name;
    //
    //    node.guid = GUID.Generate().ToString();
    //    nodes.Add(node);
    //    AssetDatabase.AddObjectToAsset(node, this);
    //    AssetDatabase.SaveAssets();
    //
    //    return node;
    //}

    public Node CreateNode(System.Type type)
    {
        Node node = ScriptableObject.CreateInstance(type) as Node;
        node.name = node.GetType().Name;
        node.guid = GUID.Generate().ToString();

        Undo.RecordObject(this, "Behaviour Tree (CreateNode)");
        nodes.Add(node);

        if (!Application.isPlaying)
            AssetDatabase.AddObjectToAsset(node, this);

        Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree (CreateNode)");
        AssetDatabase.SaveAssets();

        return node;
    }

    public void DeleteNode(Node node)
    {
        Undo.RecordObject(node, "Behaviour Tree (DeleteNode)");
        nodes.Remove(node);

        //AssetDatabase.RemoveObjectFromAsset(node);
        Undo.DestroyObjectImmediate(node);
        AssetDatabase.SaveAssets();
    }

    public void AddChild(Node parent, Node child)
    {
        if (parent is DecoratorNode decorator)
        {
            Undo.RecordObject(decorator, "Behaviour Tree (AddChild)");
            decorator.child = child;
            EditorUtility.SetDirty(decorator);
        }

        else if (parent is RootNode root)
        {
            Undo.RecordObject(root, "Behaviour Tree (AddChild)");
            root.child = child;
            EditorUtility.SetDirty(root);
        }

        else if (parent is CompositeNode composite)
        {
            Undo.RecordObject(composite, "Behaviour Tree (AddChild)");
            composite.children.Add(child);
            EditorUtility.SetDirty(composite);
        }
    }

    public void RemoveChild(Node parent, Node child)
    {
        if (parent is DecoratorNode decorator)
        {
            Undo.RecordObject(decorator, "Behaviour Tree (RemoveChild)");
            decorator.child = null;
            EditorUtility.SetDirty(decorator);
        }

        else if (parent is RootNode root)
        {
            Undo.RecordObject(root, "Behaviour Tree (RemoveChild)");
            root.child = null;
            EditorUtility.SetDirty(root);
        }

        else if (parent is CompositeNode composite)
        {
            Undo.RecordObject(composite, "Behaviour Tree (RemoveChild)");
            composite.children.Remove(child);
            EditorUtility.SetDirty(composite);
        }
    }
#endif

    public List<Node> GetChildren(Node parent)
    {
        List<Node> children = new List<Node>();

        if (parent is DecoratorNode decorator && decorator.child != null)
            children.Add(decorator.child);

        else if (parent is RootNode root && root.child != null)
            children.Add(root.child);

        else if (parent is CompositeNode composite)
            return composite.children;

        return children;
    }

    public void Traverse(Node node, System.Action<Node> visitor)
    {
        if (node != null)
        {
            visitor.Invoke(node);
            List<Node> children = GetChildren(node);
            children.ForEach((n) => Traverse(n, visitor));
        }
    }

    public BehaviourTree Clone()
    {
        BehaviourTree tree = Instantiate(this);
        tree.rootNode = rootNode.Clone();
        tree.nodes = new List<Node>();
        Traverse(tree.rootNode, (n) =>
        {
            tree.nodes.Add(n);
        });

        return tree;
    }

    public void Bind(RangedAI ai)
    {
        Traverse(rootNode, node =>
        {
            node.ai = ai;
            node.blackboard = blackboard;
        });
    }
}
