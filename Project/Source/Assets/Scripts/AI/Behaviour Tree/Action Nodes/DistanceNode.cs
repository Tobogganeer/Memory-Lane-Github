using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceNode : ActionNode
{
    public float distance;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return blackboard.distanceToCheck.SqrDistance(ai.transform.position)
            < distance * distance ? State.Success : State.Failure;
    }
}
