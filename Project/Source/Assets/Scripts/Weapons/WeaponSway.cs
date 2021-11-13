using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public static WeaponSway instance;
    private void Awake()
    {
        instance = this;
    }

    public Transform mouseSwayObj;
    public Transform movementSwayObj;
    public Transform movementBobObj;
    public Transform adsObj;

    [Header("Mouse")]
    public bool invertMouse = true;
    public float mouseSwayAmount = 0.03f;
    public float mouseMaxAmount = 0.04f;
    public float mouseSmoothAmount = 12f;

    [Header("Movement")]
    public bool invertMovement = false;
    public float movementSwayAmount = 0.04f;
    public float movementMaxAmount = 0.1f;
    public float movementSmoothAmount = 5f;
    public float verticalMultiplier = 1.5f;

    [Header("Movement Bob")]
    public bool bobUp = true;
    public float bobSpeed = 2.5f;
    public float bobAmount = 0.03f;
    public Vector3 airOffset = new Vector3(0, -0.1f, -0.05f);
    public Vector3 movingOffset = new Vector3(0, -0.01f, -0.01f);
    public float runningMultiplier = 1.5f;
    public float defaultSmoothSpeed = 5;
    public float aimingSmoothSpeed = 15;

    [Space]
    public float crouchingMultiplier = 0.6f;
    private float time;

    [Header("ADS")]
    public float adsBobSwayMultiplier = 0.1f;
    public float adsSpeed = 5;

    [Space]
    public bool debugAlwaysADS;
    public bool debugAlwaysCrouch;

    public static event System.Action<Foot, float> OnFootstep;
    private Foot currentFoot = Foot.Right;

    private Player player => Player.instance;

    public static bool IsInADS => IsInADS_Method();
    public static float MaxADSInfluence;

    private bool isCrouched => player.movement.Crouched || debugAlwaysCrouch;
    private float smoothSpeed;

    private void Update()
    {
        MouseSway();
        MovementSway();
        MovementBob();
        ADS();
    }

    private void MouseSway()
    {
        float invert = invertMouse ? -1 : 1;
        Vector2 desiredMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * invert * mouseSwayAmount;
        //desiredMovement *= Time.deltaTime;
        desiredMovement.x = Mathf.Clamp(desiredMovement.x, -mouseMaxAmount, mouseMaxAmount);
        desiredMovement.y = Mathf.Clamp(desiredMovement.y, -mouseMaxAmount, mouseMaxAmount);

        if (IsInADS) desiredMovement *= adsBobSwayMultiplier;

        //Vector3 finalPosition = new Vector3(desiredMovement.x, 0, desiredMovement.y);
        mouseSwayObj.localPosition = Vector3.Lerp(mouseSwayObj.localPosition, desiredMovement, Time.deltaTime * mouseSmoothAmount);
    }

    private void MovementSway()
    {
        float invert = invertMovement ? -1 : 1;
        Vector3 desiredMovement = invert * player.movement.LocalActualVelocity * movementSwayAmount;
        desiredMovement.x = Mathf.Clamp(desiredMovement.x, -movementMaxAmount, movementMaxAmount);
        desiredMovement.y = -1 * verticalMultiplier * Mathf.Clamp(desiredMovement.y,
            -movementMaxAmount * verticalMultiplier, movementMaxAmount * verticalMultiplier);
        desiredMovement.z = Mathf.Clamp(desiredMovement.z, -movementMaxAmount, movementMaxAmount);

        if (IsInADS) desiredMovement *= adsBobSwayMultiplier;

        movementSwayObj.localPosition = Vector3.Lerp(movementSwayObj.localPosition, desiredMovement, Time.deltaTime * movementSmoothAmount);
    }

    private void MovementBob()
    {
        Vector3 actualHorizontalVelocity = player.movement.LocalActualVelocity.Flattened();

        float velocityMag = actualHorizontalVelocity.magnitude;

        WeaponProfile currentProfile = Weapons.GetProfile(player.currentWeapon);

        time += Time.deltaTime * bobSpeed * velocityMag * currentProfile.BobSpeedMultiplier;

        float sinValue = Mathf.Sin(time);
        Vector3 offset = Vector3.zero;
        Vector3 rotOffset = Vector3.zero;

        if (player.movement.grounded)
        //if (movement.applyDownforce)
        {
            float verticalMult = bobUp ? -1 : 1;
            float runningMult = Mathf.Lerp(1, runningMultiplier, player.movement.NormalizedSpeed);
            float crouchingMult = isCrouched ? crouchingMultiplier : 1f;

            float movementScale = Mathf.InverseLerp(0, player.movement.movementProfile.runningSpeed, velocityMag);
            float multiplier = bobAmount * runningMult * crouchingMult * currentProfile.BobAmountMultiplier;
            offset = new Vector3(sinValue, verticalMult * Mathf.Abs(sinValue)) * multiplier;
            offset += movingOffset * velocityMag * movementScale;
        }
        else
            offset = airOffset;

        if (velocityMag < 1f && player.movement.grounded) offset *= velocityMag;

        if (isCrouched)
        {
            CrouchOffsets offsets = currentProfile.CrouchOffsets;

            float influence = IsInADS ? MaxADSInfluence : 0f;
            Vector3 crouchOffset = Vector3.Lerp(offsets.pos, Vector3.zero, influence);
            offset += crouchOffset;
            rotOffset += Vector3.Lerp(offsets.rot, offsets.rot_aim, influence);
        }

        CalculateFootstep(sinValue, velocityMag);

        if (IsInADS) offset *= adsBobSwayMultiplier;

        float desiredSmoothSpeed = IsInADS ? aimingSmoothSpeed : defaultSmoothSpeed;
        smoothSpeed = Mathf.Lerp(smoothSpeed, desiredSmoothSpeed, Time.deltaTime * 5);
        // Un-ADS-ing while crouched was very slow

        movementBobObj.localPosition = Vector3.Lerp(movementBobObj.localPosition, offset, Time.deltaTime * smoothSpeed);
        movementBobObj.localRotation = Quaternion.Slerp(movementBobObj.localRotation, Quaternion.Euler(rotOffset), Time.deltaTime * smoothSpeed);
        //Vector3 movement;
    }

    private void ADS()
    {
        if (!IsInADS)
        {
            adsObj.localPosition = Vector3.Lerp(adsObj.localPosition, Vector3.zero, Time.deltaTime * adsSpeed);
            return;
        }
        
        WeaponProfile currentProfile = Weapons.GetProfile(player.currentWeapon);
        adsObj.localPosition = Vector3.Lerp(adsObj.localPosition, currentProfile.ADSOffset * MaxADSInfluence, Time.deltaTime * adsSpeed);
    }

    private void CalculateFootstep(float sinValue, float magnitude)
    {
        if (magnitude < 1f || !player.movement.grounded)
        {
            time = 0;
            currentFoot = Foot.Right;
        }

        if (sinValue > 0.5f && currentFoot == Foot.Right && player.movement.grounded)
        {
            OnFootstep?.Invoke(Foot.Right, magnitude);
            currentFoot = Foot.Left;
        }
        else if (sinValue < -0.5f && currentFoot == Foot.Left && player.movement.grounded)
        {
            OnFootstep?.Invoke(Foot.Left, magnitude);
            currentFoot = Foot.Right;
        }
    }

    public static bool IsInADS_Method()
    {
        bool wantsToADS = Input.GetKey(KeyCode.Mouse1) || instance.debugAlwaysADS;
        bool grounded = Player.Movement.grounded;
        bool running = Player.Movement.Sprinting && Player.Movement.ActualVelocity.sqrMagnitude > 0.1f;
        bool crouched = Player.Movement.Crouched;

        return wantsToADS && grounded && (!running || crouched);
    }

    [System.Serializable]
    public struct CrouchOffsets
    {
        public Vector3 pos;
        public Vector3 rot;
        public Vector3 rot_aim;
    }
}
