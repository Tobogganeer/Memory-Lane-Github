using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellWeapon : WeaponBase
{
    public XRMAnimationPlayer animationPlayer;

    [Space]
    public int magazineSize;
    public int reserveAmmo;
    private int currentMagazineAmmo;
    private int currentReserveAmmo;

    [Space]
    public float doubleShellReloadTime = 1;
    public float singleShellReloadTime = 1.2f;
    public float reloadTransitionTime = 0.5f;
    public float reloadEmptyTransitionTime = 1f;

    [Space]
    public float drawTime = 1f;

    // VVV will change as ballistics model is updates (bullet pen, dropoff, accuracy etc)
    [Space]
    public float numPellets = 7;

    private float secondsPerShot;

    private float reloadTimer;
    private float reloadTransitionTimer;
    private float fireTimer;
    private float drawTimer;

    private bool tryingToCancelReload;
    private ShellReloadType lastReloadType = ShellReloadType.None;

    private bool wasEmptyReload;
    private bool transitioningIn;

    public Transform tracersFrom;

    public ReloadAudio[] singleShellAudio;
    public ReloadAudio[] doubleShellAudio;

    public ReloadAudio[] reloadStartAudio;
    public ReloadAudio[] reloadEndAudio;
    public ReloadAudio[] reloadEndEmptyAudio;

    //public float GetTimePerShot()
    //{
    //    if (secondsPerShot == 0)
    //    {
    //        secondsPerShot = 1f / (fireRateRPM / 60f);
    //    }
    //
    //    return secondsPerShot;
    //}

    private void Start()
    {
        currentMagazineAmmo = magazineSize;
        currentReserveAmmo = reserveAmmo;
        secondsPerShot = 1f / (Weapons.GetProfile(type).FireRateRPM / 60f);

        UpdateAmmoText();
    }

    private void Update()
    {
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
            if (currentMagazineAmmo <= 0 && CanReload())
            {
                wasEmptyReload = true;
                StartReload();
            }
        }

        if (drawTimer > 0)
        {
            drawTimer -= Time.deltaTime;
            if (currentMagazineAmmo <= 0 && currentReserveAmmo > 0)
            {
                wasEmptyReload = true;
                StartReload();
            }
        }

        if (reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
                OnReloadFinish();
        }

        if (reloadTransitionTimer > 0)
        {
            reloadTransitionTimer -= Time.deltaTime;
            if (reloadTransitionTimer <= 0)
                OnReloadTransitionFinish();
        }
    }

    public bool CanFire()
    {
        return fireTimer <= 0 && drawTimer <= 0 && reloadTimer <= 0 && (currentMagazineAmmo > 0 || CheatManager.InfiniteAmmo) && reloadTransitionTimer <= 0;// && GameManager.AcceptingInput;
    }

    public bool CanReload()
    {
        return currentMagazineAmmo < magazineSize && currentReserveAmmo > 0 && reloadTimer <= 0 && drawTimer <= 0 && fireTimer <= 0 && reloadTransitionTimer <= 0;
    }

    public override void OnTryFire()
    {
        if (reloadTimer > 0)
            tryingToCancelReload = true;

        if (CanFire()) Fire();

        if (currentMagazineAmmo <= 0 && CanReload())
            StartReload();
    }

    public override void OnTryReload()
    {
        if (CanReload()) StartReload();
    }

    public override void OnDraw()
    {
        fireTimer = 0;
        drawTimer = drawTime;
        reloadTimer = 0;
        reloadTransitionTimer = 0;
        tryingToCancelReload = false;

        UpdateAmmoText();
    }

    public override void OnHolster()
    {
        reloadTimer = 0;
        reloadTransitionTimer = 0;
        tryingToCancelReload = false;
    }

    public override void OnTryInspect()
    {
        if (fireTimer <= 0 && reloadTimer <= 0 && reloadTransitionTimer <= 0)
            animationPlayer.Inspect();
    }


    private void Fire()
    {
        fireTimer += secondsPerShot;
        if (!CheatManager.InfiniteAmmo)
            currentMagazineAmmo--;

        WeaponProfile profile = Weapons.GetProfile(type);

        for (int i = 0; i < numPellets; i++)
        {
            Vector3 fireDir = Ballistics.GetDirectionWithInnaccuracy(shootFrom.forward, GetInnaccuracy());

            BallisticsResult result = Ballistics.Cast(shootFrom.position, fireDir, profile.MaxRange, profile.LayerMask, profile.BallisticsSettings);
            if (!result.empty)
            {
                DamageResult damageResult = Ballistics.ApplyDamage(result, profile.BaseDamage, profile.RBForce);
                Ballistics.DrawResult(result, 5);

                if (damageResult.appliedDamage)
                {
                    // Hitmarker? Sound?
                    //AudioManager.Play(AudioArray.Hitmarker, tracersFrom.position, null);
                }
            }

            try
            {
                if (Random.Range(0f, 1f) < profile.TracerProbability)
                    FX.SpawnTracer(tracersFrom.position, fireDir, ref result);
            }
            catch { }
        }

        PlayFireSound();

        FPSCamera.Shake(profile.CamShakePreset);

        animationPlayer.Fire();

        UpdateAmmoText();

        //Vector2 inaccuracyVector = OptimizedRandom.insideUnitCircle * CalculateInaccuracy(ownerPlayer);
        //Quaternion inaccuracyRotation = Quaternion.Euler(inaccuracyVector.x, inaccuracyVector.y, 0);
        //Debug.Log(ownerPlayer.movement.GetActualMovementVector01().magnitude);

        //if (Physics.Raycast(from, inaccuracyRotation * direction, out RaycastHit hit, maxRange, shootLayerMask, QueryTriggerInteraction.Collide))
        //{
        //    if (hit.collider.TryGetComponent(out Hitbox hitbox))
        //    {
        //        hitbox.OnHit(new WeaponHitInformation(ownerPlayer, hit, damage, taggingPower));
        //    }
        //    else
        //    {
        //        SurfaceType type = SurfaceType.CONCRETE;
        //
        //        if (hit.collider.TryGetComponent(out SurfaceProperty surface))
        //            type = surface.surfaceType;
        //
        //        HitEffects.PlayHitEffectNetwork(SurfaceProperties.GetData(type).hitEffect, hit.point, Quaternion.LookRotation(hit.normal), HitEffectSpawnType.BOTH);
        //    }
        //
        //    FX.SpawnTracer(from + direction.normalized, hit.point);
        //    // move tracer to result
        //}
        //else
        //{
        //    FX.SpawnTracer(from + direction, from + direction * maxRange);
        //}

        //try
        //{
        //    PlayShootAudio(ownerPlayer);
        //}
        //catch (System.Exception ex)
        //{
        //    Debug.LogWarning("Error playing weapon shoot audio! " + ex);
        //}
    }

    private void Reload()
    {
        //int missingShells = magazineSize - currentMagazineAmmo;
        //
        //if (missingShells == 0 || reserveAmmo == 0) return;

        //StartReload();

       int missingShells = magazineSize - currentMagazineAmmo;
       
       if (missingShells == 0 || currentReserveAmmo == 0) return;
       
       if (missingShells >= 2 && currentReserveAmmo >= 2)
       {
           ReloadDouble();
           return;
       }
       else if (missingShells >= 1 && currentReserveAmmo >= 1)
       {
           ReloadSingle();
           return;
       }
    }

    private void StartReload()
    {
        reloadTransitionTimer = reloadTransitionTime;
        transitioningIn = true;
        animationPlayer.ReloadStart();

        PlayAudio(reloadStartAudio);
    }

    private void EndReload()
    {
        reloadTransitionTimer = reloadTransitionTime;
        transitioningIn = false;
        animationPlayer.ReloadFinish();

        PlayAudio(reloadEndAudio);
    }

    private void EndReloadEmpty()
    {
        reloadTransitionTimer = reloadEmptyTransitionTime;
        transitioningIn = false;
        animationPlayer.ReloadFinishEmpty();

        PlayAudio(reloadEndEmptyAudio);
    }

    private void ReloadSingle()
    {
        reloadTimer = singleShellReloadTime;
        tryingToCancelReload = false;
        lastReloadType = ShellReloadType.Single;

        animationPlayer.ReloadSingle();

        PlayAudio(singleShellAudio);
        //try
        //{
        //    PlayReloadAudio(player);
        //}
        //catch (System.Exception ex)
        //{
        //    Debug.LogWarning("Error playing weapon reload audio! " + ex);
        //}
    }

    private void ReloadDouble()
    {
        reloadTimer = doubleShellReloadTime;
        tryingToCancelReload = false;
        lastReloadType = ShellReloadType.Double;

        animationPlayer.ReloadDouble();

        PlayAudio(doubleShellAudio);
        //try
        //{
        //    PlayReloadAudio(player);
        //}
        //catch (System.Exception ex)
        //{
        //    Debug.LogWarning("Error playing weapon reload audio! " + ex);
        //}
    }

    private void OnReloadFinish()
    {
        if (lastReloadType == ShellReloadType.Single)
        {
            currentMagazineAmmo++;
            if (!CheatManager.InfiniteAmmo)
                currentReserveAmmo--;
        }
        else if (lastReloadType == ShellReloadType.Double)
        {
            currentMagazineAmmo += 2;
            if (!CheatManager.InfiniteAmmo)
                currentReserveAmmo -= 2;
        }

        UpdateAmmoText();

        currentMagazineAmmo = Mathf.Min(currentMagazineAmmo, magazineSize);
        currentReserveAmmo = Mathf.Max(currentReserveAmmo, 0);
        lastReloadType = ShellReloadType.None;

        int missingShells = magazineSize - currentMagazineAmmo;

        if (tryingToCancelReload || missingShells == 0 || currentReserveAmmo == 0)
        {
            if (wasEmptyReload) EndReloadEmpty();
            else EndReload();

            wasEmptyReload = false;
            tryingToCancelReload = false;
            return;
        }

        if (missingShells >= 2 && currentReserveAmmo >= 2)
        {
            ReloadDouble();
            return;
        }
        else if (missingShells >= 1 && currentReserveAmmo >= 1)
        {
            ReloadSingle();
            return;
        }
        else
        {
            if (wasEmptyReload) EndReloadEmpty();
            else EndReload();

            wasEmptyReload = false;
            tryingToCancelReload = false;
        }

        //if (missingShells <= reserveAmmo)
        //{
        //    currentMagazineAmmo = magazineSize;
        //    reserveAmmo -= missingShells;
        //}
        //else
        //{
        //    currentMagazineAmmo += reserveAmmo;
        //    reserveAmmo = 0;
        //}

        if (currentMagazineAmmo > magazineSize)
        {
            currentMagazineAmmo = magazineSize;
        }

        UpdateAmmoText();
    }

    private void OnReloadTransitionFinish()
    {
        if (transitioningIn) Reload();
    }

    private void UpdateAmmoText()
    {
        HUD.SetAmmoCounterText(currentMagazineAmmo, magazineSize, currentReserveAmmo);
    }

    private float GetInnaccuracy()
    {
        AccuracyProfile profile = Weapons.GetProfile(type).AccuracyProfile;

        float normalizedSpeed = Player.Movement.FromStillToMaxSpeed01;

        if (Player.Movement.Crouched)
            return Mathf.Lerp(profile.crouchingInnaccuracy, profile.standingInnaccuracy, normalizedSpeed);
        else if (Player.Movement.Sprinting)
            return Mathf.Lerp(profile.standingInnaccuracy, profile.runningInnaccuracy, normalizedSpeed);
        else
            return Mathf.Lerp(profile.standingInnaccuracy, profile.walkingInnaccuracy, normalizedSpeed);
    }

    private enum ShellReloadType
    {
        None,
        Single,
        Double
    }

    public void RefillAmmo()
    {
        currentReserveAmmo = reserveAmmo;
        currentMagazineAmmo = magazineSize;
    }
}
