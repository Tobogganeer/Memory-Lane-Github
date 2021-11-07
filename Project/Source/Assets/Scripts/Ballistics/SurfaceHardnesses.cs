using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SurfaceHardnesses
{
    public const float DEFAULT_HARDNESS = 2.0f;

    public static float GetHardness(SurfaceType type)
    {
        return DEFAULT_HARDNESS;
    }
}
