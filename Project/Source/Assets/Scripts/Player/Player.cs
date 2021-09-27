using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private void Awake()
    {
        instance = this;
    }

    public PlayerMovement movement;
    public WeaponSway weaponSway;
    public FPSCamera fpsCamera;

    public WeaponType currentWeapon = WeaponType.SMGPistol; // Temp, will be changed to a weapon class later
}
