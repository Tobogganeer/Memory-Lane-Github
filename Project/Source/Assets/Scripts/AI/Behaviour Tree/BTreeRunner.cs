using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RangedAI))]
public class BTreeRunner : MonoBehaviour
{
    public BehaviourTree tree;

    private void Start()
    {
        tree = tree.Clone();
        tree.Bind(GetComponent<RangedAI>());
    }

    private void Update()
    {
        tree.Update();
    }
}
