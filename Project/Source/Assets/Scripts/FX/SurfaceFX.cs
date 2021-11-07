using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceFX : MonoBehaviour
{
    public static SurfaceFX instance;
    private void Awake()
    {
        if (instance == null) instance = this;

        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

        hitClips.Clear();
        foreach (SurfaceEffectAudioClips aclips in i_hitClips)
        {
            hitClips.Add(aclips.array, aclips.clips);
        }

        leftFootstepClips.Clear();
        rightFootstepClips.Clear();
        foreach (SurfaceFootstepAudioClips aclips in i_footstepClips)
        {
            leftFootstepClips.Add(aclips.array, aclips.leftFootClips);
            rightFootstepClips.Add(aclips.array, aclips.rightFootClips);
        }

        surfaceEffects.Clear();
        foreach (SurfaceEffect effect in i_surfaceEffects)
        {
            surfaceEffects.Add(effect.type, effect);
        }
    }

    //public GameObject audioSourcePrefab;

    private static Dictionary<SurfaceType, AudioClip[]> hitClips = new Dictionary<SurfaceType, AudioClip[]>();
    public SurfaceEffectAudioClips[] i_hitClips;

    private static Dictionary<SurfaceFootstepType, AudioClip[]> leftFootstepClips = new Dictionary<SurfaceFootstepType, AudioClip[]>();
    private static Dictionary<SurfaceFootstepType, AudioClip[]> rightFootstepClips = new Dictionary<SurfaceFootstepType, AudioClip[]>();
    public SurfaceFootstepAudioClips[] i_footstepClips;

    public SurfaceEffect[] i_surfaceEffects;
    private static Dictionary<SurfaceType, SurfaceEffect> surfaceEffects = new Dictionary<SurfaceType, SurfaceEffect>();

    public bool suppressWarnings;

    public static void PlayHitSound(SurfaceType surfaceType, Vector3 position, Transform parent = null, float maxDistance = 10, float volume = 1, float minPitch = 0.85f, float maxPitch = 1.10f)
    {
        if (!hitClips.ContainsKey(surfaceType) || hitClips[surfaceType].Length == 0)
        {
            //Play(nullClip, position, parent, maxDistance, volume, minPitch, maxPitch);
            if (!instance.suppressWarnings)
                Debug.LogWarning("Could not find hit sound for " + surfaceType);
            return;
        }

        int clip = Random.Range(0, hitClips[surfaceType].Length);
        AudioManager.Play(hitClips[surfaceType][clip], position, parent, maxDistance, volume, minPitch, maxPitch);
    }

    public static void PlayFootstepSound(SurfaceFootstepType footstepType, Vector3 position, Foot foot)
    {
        if (foot == Foot.Left)
        {
            if (!leftFootstepClips.ContainsKey(footstepType) || leftFootstepClips[footstepType].Length == 0)
            {
                //Play(nullClip, position, parent, maxDistance, volume, minPitch, maxPitch);
                if (!instance.suppressWarnings)
                    Debug.LogWarning("Could not find footstep sound for " + footstepType);
                if (footstepType == SurfaceFootstepType.DefaultConcrete) return;
                int defaultClip = Random.Range(0, rightFootstepClips[SurfaceFootstepType.DefaultConcrete].Length);
                AudioManager.Play(rightFootstepClips[SurfaceFootstepType.DefaultConcrete][defaultClip], position);
                return;
            }

            int clip = Random.Range(0, rightFootstepClips[footstepType].Length);
            AudioManager.Play(rightFootstepClips[footstepType][clip], position);
        }
        else if (foot == Foot.Right)
        {
            if (!rightFootstepClips.ContainsKey(footstepType) || rightFootstepClips[footstepType].Length == 0)
            {
                //Play(nullClip, position, parent, maxDistance, volume, minPitch, maxPitch);
                Debug.LogWarning("Could not find footstep sound for " + footstepType);
                if (footstepType == SurfaceFootstepType.DefaultConcrete) return;
                int defaultClip = Random.Range(0, leftFootstepClips[SurfaceFootstepType.DefaultConcrete].Length);
                AudioManager.Play(leftFootstepClips[SurfaceFootstepType.DefaultConcrete][defaultClip], position);
                return;
            }

            int clip = Random.Range(0, leftFootstepClips[footstepType].Length);
            AudioManager.Play(leftFootstepClips[footstepType][clip], position);
        }

        
    }

    public static void PlayFootstepSound(SurfaceType surfaceType, Vector3 position, Foot foot)
    {
        PlayFootstepSound(surfaceType.GetFootstepType(), position, foot);
    }

    public static void SpawnFX(SurfaceType surfaceType, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (!surfaceEffects.TryGetValue(surfaceType, out SurfaceEffect effect))
        {
            if (!instance.suppressWarnings)
                Debug.LogWarning("Could not spawn surface effect for " + surfaceType);
        }

        Instantiate(effect.gameObject, position, rotation, parent);
    }

    public static void SpawnFX(SurfaceType surfaceType, Vector3 position, Transform parent = null)
    {
        SpawnFX(surfaceType, position, Quaternion.identity, parent);
    }
}

// Add support for each foot having different sounds

[System.Serializable]
public struct SurfaceEffectAudioClips
{
    public string name;
    public SurfaceType array;
    public AudioClip[] clips;
}

[System.Serializable]
public struct SurfaceFootstepAudioClips
{
    public string name;
    public SurfaceFootstepType array;
    public AudioClip[] leftFootClips;
    public AudioClip[] rightFootClips;
}
