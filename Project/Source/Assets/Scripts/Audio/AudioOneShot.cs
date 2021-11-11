using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOneShot : MonoBehaviour
{
    public AudioArray audioArray;
    public SurfaceType surfaceHitType;
    public float range = 10;
    public float volume = 1;
    public float minPitch = 0.85f;
    public float maxPitch = 1.1f;

    public bool parentToThis = false;

    private void Start()
    {
        Transform parent = parentToThis ? transform : null;
        
        if (audioArray != AudioArray.Null)
            AudioManager.Play(audioArray, transform.position, parent, range, volume, minPitch, maxPitch);
        if (surfaceHitType != SurfaceType.None)
            SurfaceFX.PlayHitSound(surfaceHitType, transform.position, parent, range, volume, minPitch, maxPitch);
    }
}
