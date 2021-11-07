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
    public HitPoints health;
    public HitPoints armour;

    public WeaponType currentWeapon = WeaponType.P3K; // Temp, will be changed to a weapon class later

    public static PlayerMovement Movement => instance.movement;
    public static WeaponSway WeaponSway => instance.weaponSway;
    public static FPSCamera FPSCamera => instance.fpsCamera;
    public static Transform Transform => instance.transform;
    public static Vector3 Position => instance.transform.position;
    public static Quaternion Rotation => instance.transform.rotation;
    public static Vector3 LookDirection => instance.fpsCamera.transform.forward;
    public static HitPoints Health => instance.health;
    public static HitPoints Armour => instance.armour;
    public static bool IsDead => Health.CurrentHP <= 0;

    public static void SetPosition(Vector3 position)
    {
        Movement.Controller.enabled = false;
        Transform.position = position;
        Movement.Controller.enabled = false;
    }

    public static void LookAt(Vector3 point)
    {
        FPSCamera.LookAt_Local(point);
    }

    public static void TakeDamage(float damage)
    {
        if (damage > Armour.CurrentHP)
        {
            Health.TakeDamage(damage - Armour.CurrentHP);
            Armour.SetHP(0);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
            SceneManager.ReloadCurrentLevel();

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
