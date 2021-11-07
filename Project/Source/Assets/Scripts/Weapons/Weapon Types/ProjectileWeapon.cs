using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    public WeaponAnimationPlayer animationPlayer;

    [Space]
    public int magazineSize;
    public int reserveAmmo;
    private int currentMagazineAmmo;

    [Space]
    public float reloadTime = 2f;
    public float reloadEmptyTime = 2f;

    [Space]
    public float drawTime = 1f;

    [Space]
    public float fireRateRPM;
    private float secondsPerShot;

    private float reloadTimer;
    private float fireTimer;
    private float drawTimer;

    public GameObject projectilePrefab;

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
        secondsPerShot = 1f / (fireRateRPM / 60f);

        UpdateAmmoText();
    }

    private void Update()
    {
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
            if (currentMagazineAmmo <= 0 && CanReload())
                ReloadEmpty();
        }

        if (drawTimer > 0)
        {
            drawTimer -= Time.deltaTime;
            if (currentMagazineAmmo <= 0 && reserveAmmo > 0)
                ReloadEmpty();
        }

        if (reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
                OnReloadFinish();
        }
    }

    public bool CanFire()
    {
        return fireTimer <= 0 && drawTimer <= 0 && reloadTimer <= 0 && (currentMagazineAmmo > 0 || CheatManager.InfiniteAmmo);// && GameManager.AcceptingInput;
    }

    public bool CanReload()
    {
        return currentMagazineAmmo < magazineSize && reserveAmmo > 0 && reloadTimer <= 0 && drawTimer <= 0 && fireTimer <= 0;
    }

    public override void OnTryFire()
    {
        if (CanFire()) Fire();

        if (currentMagazineAmmo <= 0 && CanReload())
            ReloadEmpty();
    }

    public override void OnTryReload()
    {
        if (CanReload()) Reload();
    }

    public override void OnDraw()
    {
        fireTimer = 0;
        drawTimer = drawTime;
        reloadTimer = 0;

        UpdateAmmoText();
    }

    public override void OnHolster()
    {
        reloadTimer = 0;
    }

    public override void OnTryInspect()
    {
        if (fireTimer <= 0 && reloadTimer <= 0)
            animationPlayer.Inspect();
    }


    private void Fire()
    {
        fireTimer += secondsPerShot;
        if (!CheatManager.InfiniteAmmo)
            currentMagazineAmmo--;

        Instantiate(projectilePrefab, shootFrom.position, Quaternion.LookRotation(shootFrom.forward, Vector3.up));

        PlayFireSound();

        FPSCamera.Shake(Weapons.GetProfile(type).CamShakePreset);

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
        reloadTimer = reloadTime;

        animationPlayer.Reload();

        //try
        //{
        //    PlayReloadAudio(player);
        //}
        //catch (System.Exception ex)
        //{
        //    Debug.LogWarning("Error playing weapon reload audio! " + ex);
        //}
    }

    private void ReloadEmpty()
    {
        reloadTimer = reloadEmptyTime;

        animationPlayer.ReloadEmpty();

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
        int bulletsNeeded = magazineSize - currentMagazineAmmo;

        if (bulletsNeeded <= reserveAmmo)
        {
            currentMagazineAmmo = magazineSize;
            if (!CheatManager.InfiniteAmmo)
                reserveAmmo -= bulletsNeeded;
        }
        else
        {
            currentMagazineAmmo += reserveAmmo;
            reserveAmmo = 0;
        }

        if (currentMagazineAmmo > magazineSize)
        {
            currentMagazineAmmo = magazineSize;
        }

        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        HUD.SetAmmoCounterText(currentMagazineAmmo, magazineSize, reserveAmmo);
    }
}
