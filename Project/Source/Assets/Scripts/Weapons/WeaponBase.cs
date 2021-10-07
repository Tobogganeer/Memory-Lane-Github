using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
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
}
