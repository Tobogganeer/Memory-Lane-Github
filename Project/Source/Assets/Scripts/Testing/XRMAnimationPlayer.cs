using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRMAnimationPlayer : MonoBehaviour
{
    public Animator animator;

    private const float CROSS_FADE_TIME = 0.1f;
    private const float FIRE_CROSS_FADE_TIME = 0.03f;

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.P)) animator.CrossFadeInFixedTime("Reload Start", CROSS_FADE_TIME);
    //    if (Input.GetKeyDown(KeyCode.L)) animator.CrossFadeInFixedTime("Reload Finish", CROSS_FADE_TIME);
    //    if (Input.GetKeyDown(KeyCode.M)) animator.CrossFadeInFixedTime("Reload Finish Empty", CROSS_FADE_TIME);
    //    if (Input.GetKeyDown(KeyCode.R)) animator.CrossFadeInFixedTime("Reload Single", CROSS_FADE_TIME);
    //    if (Input.GetKeyDown(KeyCode.Mouse0)) animator.CrossFadeInFixedTime("Fire", FIRE_CROSS_FADE_TIME);
    //    if (Input.GetKeyDown(KeyCode.F)) animator.CrossFadeInFixedTime("Inspect", CROSS_FADE_TIME);
    //    if (Input.GetKeyDown(KeyCode.T)) animator.CrossFadeInFixedTime("Reload Double", CROSS_FADE_TIME);
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

    public void ReloadStart()
    {
        animator.CrossFadeInFixedTime("Reload Start", CROSS_FADE_TIME);
    }

    public void ReloadFinish()
    {
        animator.CrossFadeInFixedTime("Reload Finish", CROSS_FADE_TIME);
    }

    public void ReloadFinishEmpty()
    {
        animator.CrossFadeInFixedTime("Reload Finish Empty", CROSS_FADE_TIME);
    }

    public void ReloadDouble()
    {
        animator.CrossFadeInFixedTime("Reload Double", CROSS_FADE_TIME);
    }

    public void ReloadSingle()
    {
        animator.CrossFadeInFixedTime("Reload Single", CROSS_FADE_TIME);
    }
}
