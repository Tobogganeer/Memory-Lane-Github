using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public Player player;

    public Transform mouseSwayObj;
    public Transform movementSwayObj;
    public Transform movementBobObj;

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
    public float smoothSpeed = 5;
    private float time;

    public static event System.Action<Foot, float> OnFootstep;
    private Foot currentFoot = Foot.Right;

    private void Update()
    {
        MouseSway();
        MovementSway();
        MovementBob();
    }

    private void MouseSway()
    {
        float invert = invertMouse ? -1 : 1;
        Vector2 desiredMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * invert * mouseSwayAmount;
        //desiredMovement *= Time.deltaTime;
        desiredMovement.x = Mathf.Clamp(desiredMovement.x, -mouseMaxAmount, mouseMaxAmount);
        desiredMovement.y = Mathf.Clamp(desiredMovement.y, -mouseMaxAmount, mouseMaxAmount);

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
        movementSwayObj.localPosition = Vector3.Lerp(movementSwayObj.localPosition, desiredMovement, Time.deltaTime * movementSmoothAmount);
    }

    private void MovementBob()
    {
        Vector3 actualHorizontalVelocity = player.movement.LocalActualVelocity.Flattened();

        float velocityMag = actualHorizontalVelocity.magnitude;

        WeaponProfile currentProfile = Weapons.GetProfile(player.currentWeapon);

        time += Time.deltaTime * bobSpeed * velocityMag * currentProfile.BobSpeedMultiplier;

        float sinValue = Mathf.Sin(time);
        Vector3 offset;

        if (player.movement.grounded)
        //if (movement.applyDownforce)
        {
            float verticalMult = bobUp ? -1 : 1;
            float runningMult = Mathf.Lerp(1, runningMultiplier, player.movement.NormalizedSpeed);

            float movementScale = Mathf.InverseLerp(0, player.movement.movementProfile.runningSpeed, velocityMag);
            float multiplier = bobAmount * runningMult * currentProfile.BobAmountMultiplier;
            offset = new Vector3(sinValue, verticalMult * Mathf.Abs(sinValue)) * multiplier;
            offset += movingOffset * velocityMag * movementScale;
        }
        else
            offset = airOffset;

        if (velocityMag < 1f && player.movement.grounded) offset *= velocityMag;

        CalculateFootstep(sinValue, velocityMag);

        movementBobObj.localPosition = Vector3.Lerp(movementBobObj.localPosition, offset, Time.deltaTime * smoothSpeed);
        //Vector3 movement;
    }

    private void CalculateFootstep(float sinValue, float magnitude)
    {
        if (magnitude == 0 || !player.movement.grounded)
        {
            time = 0;
            currentFoot = Foot.Right;
        }

        if (sinValue > 0.9f && currentFoot == Foot.Right && player.movement.grounded)
        {
            OnFootstep?.Invoke(Foot.Right, magnitude);
            currentFoot = Foot.Left;
        }
        else if (sinValue < -0.9f && currentFoot == Foot.Left && player.movement.grounded)
        {
            OnFootstep?.Invoke(Foot.Left, magnitude);
            currentFoot = Foot.Right;
        }
    }
}
