using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Movement Profile")]
public class MovementProfile : ScriptableObject
{
    [Min(0f)] public float walkingSpeed = 3.5f;
    [Min(0f)] public float runningSpeed = 6.5f;
    [Min(0f)] public float airSpeedMultiplier = 1.2f;

    public float gravity = 10f;

    [Min(0f)] public float walkingJumpHeight = 3.5f;
    [Min(0f)] public float runningJumpHeight = 4.5f;

    [Min(0f)] public float groundAcceleration = 10;
    [Min(0f)] public float airAcceleration = 2;
}
