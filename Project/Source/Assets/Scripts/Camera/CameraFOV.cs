using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{
    public Camera[] cameras;
    private float[] fovs;
    //private static float desiredFOV;
    private static float desiredMultiplier = 1;
    public float transitionSpeed = 5;

    public float adsMult = 1.3f;

    private void Start()
    {
        fovs = new float[cameras.Length];
        for (int i = 0; i < fovs.Length; i++)
        {
            fovs[i] = cameras[i].fieldOfView;
        }
    }

    private void Update()
    {
        float mult = desiredMultiplier * (WeaponSway.IsInADS ? Mathf.Lerp(1f, adsMult, WeaponSway.MaxADSInfluence) : 1f);

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].fieldOfView = Mathf.Lerp(cameras[i].fieldOfView, fovs[i] * mult, Time.deltaTime * transitionSpeed);
        }
    }

    //public static void Set(float newFOV)
    //{
    //    desiredFOV = newFOV;
    //}

    public static void Set(float multiplier)
    {
        desiredMultiplier = multiplier;
    }
}
