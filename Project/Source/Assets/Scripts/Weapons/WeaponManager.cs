using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponBase p3k;
    public WeaponBase nateva;
    public WeaponBase mk_x;
    public WeaponBase molkor;
    public WeaponBase gr3_n;
    public WeaponBase xrm;

    private WeaponBase currentWeapon;

    private void Start()
    {
        EnableWeapon(p3k);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EnableWeapon(p3k);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EnableWeapon(nateva);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EnableWeapon(mk_x);
        if (Input.GetKeyDown(KeyCode.Alpha4)) EnableWeapon(molkor);
        if (Input.GetKeyDown(KeyCode.Alpha5)) EnableWeapon(gr3_n);
        if (Input.GetKeyDown(KeyCode.Alpha6)) EnableWeapon(xrm);

        if (currentWeapon == null) return;

        if (Inputs.inputProfile == null) Inputs.inputProfile = new InputProfile();

        if (Input.GetKeyDown(Inputs.Fire) || (currentWeapon.fullAuto && Input.GetKey(Inputs.Fire))) currentWeapon.OnTryFire();
        if (Input.GetKeyDown(Inputs.Reload)) currentWeapon.OnTryReload();
        if (Input.GetKeyDown(Inputs.Inspect)) currentWeapon.OnTryInspect();
    }

    private void EnableWeapon(WeaponBase weapon)
    {
        DisableAll();
        weapon.gameObject.SetActive(true);
        Player.instance.currentWeapon = weapon.type;
        currentWeapon = weapon;
        currentWeapon.OnDraw();
    }

    private void DisableAll()
    {
        if (currentWeapon != null) currentWeapon.OnHolster();

        p3k.gameObject.SetActive(false);
        nateva.gameObject.SetActive(false);
        mk_x.gameObject.SetActive(false);
        molkor.gameObject.SetActive(false);
        gr3_n.gameObject.SetActive(false);
        xrm.gameObject.SetActive(false);
    }
}
