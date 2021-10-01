using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
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

        clips.Clear();
        foreach (AudioClips aclips in audioClips)
        {
            clips.Add(aclips.array, aclips.clips);
        }

        nullClip = clips[AudioArray.Null][0];
    }

    //public GameObject audioSourcePrefab;

    private static Dictionary<AudioArray, AudioClip[]> clips = new Dictionary<AudioArray, AudioClip[]>();
    public AudioClips[] audioClips;
    private static AudioClip nullClip;

    public static void Play(AudioArray sound, Vector3 position, Transform parent, float maxDistance = 10, float volume = 1, float minPitch = 0.85f, float maxPitch = 1.10f)
    {
        if (!clips.ContainsKey(sound) || clips[sound].Length == 0)
        {
            Play(nullClip, position, parent, maxDistance, volume, minPitch, maxPitch);
            Debug.LogWarning("Could not find clip for " + sound);
            return;
        }

        int clip = Random.Range(0, clips[sound].Length);
        Play(clips[sound][clip], position, parent, maxDistance, volume, minPitch, maxPitch);
    }

    public static void Play(AudioClip sound, Vector3 position, Transform parent, float maxDistance = 10, float volume = 1, float minPitch = 0.85f, float maxPitch = 1.10f)
    {
        //AudioSource source = Instantiate(instance.audioSourcePrefab, position, Quaternion.identity, parent).GetComponent<AudioSource>();
        GameObject sourceObj = ObjectPoolManager.GetObject(PooledObject.AudioSource);
        if (sourceObj != null)
        {
            sourceObj.transform.SetParent(parent);
            sourceObj.transform.position = position;

            AudioSource source = sourceObj.GetComponent<AudioSource>();

            source.clip = sound;
            source.maxDistance = maxDistance;
            float pitch = Random.Range(minPitch, maxPitch);
            source.pitch = pitch;
            source.volume = volume;
            source.Play();

            sourceObj.GetComponent<PooledAudioSource>().DisableAfterTime(source.clip.length * (pitch + 0.1f));
            //Destroy(source.gameObject, source.clip.length * (pitch + 0.1f));
        }
        else
        {
            Debug.Log($"Couldn't play {sound.name} as received null audio source from pool");
        }
    }
}



[System.Serializable]
public struct AudioClips
{
    public string name;
    public AudioArray array;
    public AudioClip[] clips;
}

