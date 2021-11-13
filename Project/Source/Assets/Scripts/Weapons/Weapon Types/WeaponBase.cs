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

    public void FireFX(WeaponProfile profile, WeaponAnimationPlayer animationPlayer)
    {
        FPSCamera.Shake(profile.CamShakePreset);
        float recoil = profile.RecoilAmount;

        if (!WeaponSway.IsInADS)
        {
            animationPlayer.Fire();
        }
        else
        {
            animationPlayer.FireAimed();
            recoil *= profile.AimingRecoilMultiplier;
        }

        if (Player.Movement.Crouched) recoil *= profile.CrouchingRecoilMultiplier;

        FPSCamera.AddRecoil(recoil);
    }

    public float GetInnaccuracy()
    {
        AccuracyProfile profile = Weapons.GetProfile(type).AccuracyProfile;

        float normalizedSpeed = Player.Movement.FromStillToMaxSpeed01;
        float accuracy;

        if (Player.Movement.Sprinting)
            accuracy = Mathf.Lerp(profile.standingInnaccuracy, profile.runningInnaccuracy, normalizedSpeed);
        else
            accuracy = Mathf.Lerp(profile.standingInnaccuracy, profile.walkingInnaccuracy, normalizedSpeed);

        if (WeaponSway.IsInADS)
            accuracy *= profile.adsMult;
        if (Player.Movement.Crouched)
            accuracy *= profile.crouchingMult;
        if (!Player.Movement.grounded)
            accuracy *= profile.airborneMult;

        return accuracy;
    }
}

[System.Serializable]
public struct ReloadAudio
{
    public AudioArray clipType;
    public float afterTime;
}
