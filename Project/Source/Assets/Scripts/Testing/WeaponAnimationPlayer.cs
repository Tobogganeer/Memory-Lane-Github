using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationPlayer : MonoBehaviour
{
    public Animator animator;

    private const float CROSS_FADE_TIME = 0.1f;
    private const float FIRE_CROSS_FADE_TIME = 0.03f;

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R)) animator.CrossFadeInFixedTime("Reload", CROSS_FADE_TIME);
    //    if (Input.GetKeyDown(KeyCode.Mouse0)) animator.CrossFadeInFixedTime("Fire", FIRE_CROSS_FADE_TIME);
    //    if (Input.GetKeyDown(KeyCode.F)) animator.CrossFadeInFixedTime("Inspect", CROSS_FADE_TIME);
    //    if (Input.GetKeyDown(KeyCode.T)) animator.CrossFadeInFixedTime("Reload Empty", CROSS_FADE_TIME);
    //    if (Input.GetKeyDown(KeyCode.E)) animator.CrossFadeInFixedTime("Fiddle", CROSS_FADE_TIME);
    //    if (Input.GetKeyDown(KeyCode.Mouse1)) animator.CrossFadeInFixedTime("Draw", CROSS_FADE_TIME);
    //}

    public void Draw()
    {
        animator.CrossFadeInFixedTime("Draw", CROSS_FADE_TIME);
    }

    public void Fire()
    {
        animator.CrossFadeInFixedTime("Fire", FIRE_CROSS_FADE_TIME);
    }

    public void Fiddle()
    {
        animator.CrossFadeInFixedTime("Fiddle", CROSS_FADE_TIME);
    }

    public void Inspect()
    {
        animator.CrossFadeInFixedTime("Inspect", CROSS_FADE_TIME);
    }

    public void Reload()
    {
        animator.CrossFadeInFixedTime("Reload", CROSS_FADE_TIME);
    }

    public void ReloadEmpty()
    {
        animator.CrossFadeInFixedTime("Reload Empty", CROSS_FADE_TIME);
    }
}
