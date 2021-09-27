using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Weapon Profile")]
public class WeaponProfile : ScriptableObject
{
    [SerializeField] private WeaponType type;
    [SerializeField] private float speedMultiplier = 1;
    [SerializeField] private float jumpMultiplier = 1;
    [SerializeField] private float bobSpeedMultiplier = 1;
    [SerializeField] private float bobAmountMultiplier = 1;

    public WeaponType Type => type;
    public float SpeedMultiplier => speedMultiplier;
    public float JumpMultiplier => jumpMultiplier;
    public float BobSpeedMultiplier => bobSpeedMultiplier;
    public float BobAmountMultiplier => bobAmountMultiplier;
}
