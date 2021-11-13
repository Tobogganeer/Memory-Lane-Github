using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Weapon Profile")]
public class WeaponProfile : ScriptableObject
{
    [SerializeField] private WeaponType type;
    [SerializeField] private float speedMultiplier = 1;
    [SerializeField] private float jumpMultiplier = 1;
    [SerializeField] private float adsSpeedMultiplier = 0.7f;
    [SerializeField] private float bobSpeedMultiplier = 1;
    [SerializeField] private float bobAmountMultiplier = 1;
    [SerializeField] private MilkShake.ShakePreset camShakePreset;
    [SerializeField] private float fireRateRPM;
    [SerializeField] private Vector3 adsOffset;
    [SerializeField] private float recoilAmount = 10;
    [SerializeField] private float aimingRecoilMultiplier = 0.7f;
    [SerializeField] private float crouchingRecoilMultiplier = 0.7f;
    [SerializeField] private WeaponSway.CrouchOffsets crouchOffsets;

    [Header("Following doesn't apply to GR3-N")]
    [SerializeField] private BallisticsSettings ballisticsSettings;
    [SerializeField] private float maxRange;
    [SerializeField] private float baseDamage;
    [SerializeField] private AccuracyProfile accuracyProfile;
    [Range(0f, 1f)]
    [SerializeField] private float tracerProbability;
    [SerializeField] private LayerMask layermask;
    [SerializeField] private float rbForce;
    [SerializeField] private Hitbox.DamageRegions hitboxDamageMultipliers;

    public WeaponType Type => type;
    public float SpeedMultiplier => speedMultiplier;
    public float JumpMultiplier => jumpMultiplier;
    public float ADSSpeedMultiplier => adsSpeedMultiplier;
    public float BobSpeedMultiplier => bobSpeedMultiplier;
    public float BobAmountMultiplier => bobAmountMultiplier;
    public MilkShake.ShakePreset CamShakePreset => camShakePreset;
    public float FireRateRPM => fireRateRPM;
    public Vector3 ADSOffset => adsOffset;
    public float RecoilAmount => recoilAmount;
    public float AimingRecoilMultiplier => aimingRecoilMultiplier;
    public float CrouchingRecoilMultiplier => crouchingRecoilMultiplier;
    public WeaponSway.CrouchOffsets CrouchOffsets => crouchOffsets;

    public BallisticsSettings BallisticsSettings => ballisticsSettings;
    public float MaxRange => maxRange;
    public float BaseDamage => baseDamage;
    public AccuracyProfile AccuracyProfile => accuracyProfile;
    public float TracerProbability => tracerProbability;
    public LayerMask LayerMask => layermask;
    public float RBForce => rbForce;
    public Hitbox.DamageRegions HitboxDamageMultipliers => hitboxDamageMultipliers;
}
