using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSeePlayerNode : ActionNode
{
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        Vector3 playerPos = Player.Position;

        if (ai.vision.CanSee(playerPos))
        {
            blackboard.lastSeenPlayerTime = Time.time;
            blackboard.lastSeenPlayerPos = playerPos;
            blackboard.destination = playerPos;
            return State.Success;
        }
        else return State.Failure;
    }
}
