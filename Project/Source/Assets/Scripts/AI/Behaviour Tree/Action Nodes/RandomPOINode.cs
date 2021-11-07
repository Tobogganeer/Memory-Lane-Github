using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPOINode : ActionNode
{
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        blackboard.destination = AIPointManager.GetPointOfInterest(ai.transform.position);
        return State.Success;
    }
}
