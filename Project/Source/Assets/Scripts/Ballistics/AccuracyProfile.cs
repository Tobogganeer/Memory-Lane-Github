using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AccuracyProfile
{
    public float standingInnaccuracy;
    
    public float walkingInnaccuracy;
    public float runningInnaccuracy;

    public float adsMult;
    public float crouchingMult;
    public float airborneMult;
}
