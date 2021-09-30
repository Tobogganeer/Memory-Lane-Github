using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProfile
{
    public KeyCode forward = KeyCode.W;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode backwards = KeyCode.S;

    public KeyCode sprint = KeyCode.LeftShift;
    public KeyCode jump = KeyCode.Space;

    public InputProfile()
    {
        forward = KeyCode.W;
        left = KeyCode.A;
        right = KeyCode.D;
        backwards = KeyCode.S;

        sprint = KeyCode.LeftShift;
        jump = KeyCode.Space;
    }
}
