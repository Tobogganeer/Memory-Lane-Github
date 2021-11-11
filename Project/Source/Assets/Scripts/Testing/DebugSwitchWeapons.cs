using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSwitchWeapons : MonoBehaviour
{
    public GameObject p3k;
    public GameObject nateva;
    public GameObject mk_x;
    public GameObject molkor;
    public GameObject gr3_n;
    public GameObject xrm;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DisableAll();
            p3k.SetActive(true);
            Player.instance.currentWeapon = WeaponType.P3K;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DisableAll();
            nateva.SetActive(true);
            Player.instance.currentWeapon = WeaponType.Nateva;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DisableAll();
            mk_x.SetActive(true);
            Player.instance.currentWeapon = WeaponType.FNAL;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DisableAll();
            molkor.SetActive(true);
            Player.instance.currentWeapon = WeaponType.Molkor;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            DisableAll();
            gr3_n.SetActive(true);
            Player.instance.currentWeapon = WeaponType.GR3_N;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            DisableAll();
            xrm.SetActive(true);
            Player.instance.currentWeapon = WeaponType.XRM;
        }
    }

    private void DisableAll()
    {
        p3k.SetActive(false);
        nateva.SetActive(false);
        mk_x.SetActive(false);
        molkor.SetActive(false);
        gr3_n.SetActive(false);
        xrm.SetActive(false);
    }
}
