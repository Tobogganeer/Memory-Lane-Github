using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationPlayer : MonoBehaviour
{
    public Animator animator;

    private const float CROSS_FADE_TIME = 0.1f;
    private const float AIM_CROSS_FADE_TIME = 0.3f;
    private const float FIRE_CROSS_FADE_TIME = 0.03f;

    public void Draw()
    {
        animator.Play(CurrentWeaponName() + "Draw");
    }

    public void Fire()
    {
        animator.CrossFadeInFixedTime(CurrentWeaponName() + "Fire", FIRE_CROSS_FADE_TIME);
    }

    public void Inspect()
    {
        animator.CrossFadeInFixedTime(CurrentWeaponName() + "Inspect", CROSS_FADE_TIME);
    }

    public void Reload()
    {
        animator.CrossFadeInFixedTime(CurrentWeaponName() + "Reload", CROSS_FADE_TIME);
    }

    public void ReloadEmpty()
    {
        animator.CrossFadeInFixedTime(CurrentWeaponName() + "Reload_Empty", CROSS_FADE_TIME);
    }

    //public void Aim()
    //{
    //    animator.CrossFadeInFixedTime(CurrentWeaponName() + "Idle_Aim", AIM_CROSS_FADE_TIME);
    //}
    //
    //public void Idle()
    //{
    //    animator.CrossFadeInFixedTime(CurrentWeaponName() + "Idle", AIM_CROSS_FADE_TIME);
    //}

    public void FireAimed()
    {
        animator.CrossFadeInFixedTime(CurrentWeaponName() + "Fire_Aim", FIRE_CROSS_FADE_TIME);
    }

    //XRM
    public void ReloadStart()
    {
        animator.CrossFadeInFixedTime(CurrentWeaponName() + "Reload_Start", CROSS_FADE_TIME);
    }

    public void ReloadFinish()
    {
        animator.CrossFadeInFixedTime(CurrentWeaponName() + "Reload_Finish", CROSS_FADE_TIME);
    }

    public void ReloadFinishEmpty()
    {
        animator.CrossFadeInFixedTime(CurrentWeaponName() + "Reload_Finish_Empty", CROSS_FADE_TIME);
    }

    public void ReloadDouble()
    {
        animator.CrossFadeInFixedTime(CurrentWeaponName() + "Reload_Double", CROSS_FADE_TIME);
    }

    public void ReloadSingle()
    {
        animator.CrossFadeInFixedTime(CurrentWeaponName() + "Reload_Single", CROSS_FADE_TIME);
    }

    private string CurrentWeaponName()
    {
        return Player.instance.currentWeapon.GetName() + ":";
    }
}
