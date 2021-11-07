using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Blackboard
{
    public Vector3? lastSeenPlayerPos;
    public Vector3? currentPointOfInterest;
    public float lastSeenPlayerTime;

    public Vector3 distanceToCheck;
    public Vector3 destination;

    // Add required variables here
}
