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
    public KeyCode crouch = KeyCode.C;

    public KeyCode fire = KeyCode.Mouse0;
    public KeyCode reload = KeyCode.R;
    public KeyCode inspect = KeyCode.F;
    public KeyCode interact = KeyCode.E;

    public InputProfile()
    {
        forward = KeyCode.W;
        left = KeyCode.A;
        right = KeyCode.D;
        backwards = KeyCode.S;

        sprint = KeyCode.LeftShift;
        jump = KeyCode.Space;
        crouch = KeyCode.C;

        fire = KeyCode.Mouse0;
        reload = KeyCode.R;
        inspect = KeyCode.F;
        interact = KeyCode.E;
    }
}
