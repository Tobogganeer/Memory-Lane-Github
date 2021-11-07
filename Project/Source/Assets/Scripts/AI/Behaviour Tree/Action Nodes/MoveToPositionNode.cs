using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPositionNode : ActionNode
{
    public float distanceThreshold = 2;
    public float maxStationaryTime = 2;
    private float currentStationaryTime = 0;

    protected override void OnStart()
    {
        ai.agent.SetDestination(blackboard.destination);
        currentStationaryTime = 0;
    }

    protected override void OnStop()
    {
        ai.agent.ResetPath();
        currentStationaryTime = 0;
    }

    protected override State OnUpdate()
    {
        if (ai.agent.velocity.sqrMagnitude > 0.1f)
            currentStationaryTime = 0;
        else 
            currentStationaryTime += Time.deltaTime;

        if (ai.transform.position.SqrDistance(ai.agent.destination) < distanceThreshold * distanceThreshold)
            return State.Success;
        else if (currentStationaryTime > maxStationaryTime)
            return State.Failure;

        return State.Running;
    }
}
