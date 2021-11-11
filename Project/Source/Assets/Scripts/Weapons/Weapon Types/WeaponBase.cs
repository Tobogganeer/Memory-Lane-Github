using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public const int WEAPON_RELOAD_TIMER_ID = 500;

    public WeaponType type;
    public bool fullAuto;
    public Transform shootFrom;
    public AudioArray fireSound;

    public abstract void OnTryFire();
    public abstract void OnTryReload();
    public abstract void OnDraw();
    public abstract void OnHolster();
    public abstract void OnTryInspect();

    public void PlayFireSound()
    {
        AudioManager.Play(fireSound, transform.position, transform);
    }

    public void PlayAudio(ReloadAudio[] audio)
    {
        foreach (ReloadAudio item in audio)
        {
            Timer.Run(() => AudioManager.Play(item.clipType, transform.position, transform), item.afterTime, WEAPON_RELOAD_TIMER_ID);
        }
    }
}

[System.Serializable]
public struct ReloadAudio
{
    public AudioArray clipType;
    public float afterTime;
}
