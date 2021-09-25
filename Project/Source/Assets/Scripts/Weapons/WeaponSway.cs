using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public PlayerMovement movement;

    public Transform mouseSwayObj;
    public Transform movementSwayObj;
    public Transform movementBobObj;
    public Transform extendObj;

    public UnityEngine.UI.Image cooldownImage;

    [Header("Mouse")]
    public bool invertMouse = true;
    public float mouse_swayAmount;
    public float mouse_maxAmount;
    public float mouse_smoothAmount;

    [Header("Movement")]
    public bool invertMovement = true;
    public float movement_swayAmount;
    public float movement_maxAmount;
    public float movement_smoothAmount;
    public float verticalMultiplier = 3;

    [Header("Movement Bob")]
    public float bobSpeed = 3;
    public float bobAmount = 2;
    public Vector3 airOffset = Vector3.down;
    public Vector3 movingOffset = Vector3.down;
    public float smoothSpeed = 5;
    private float time;

    [Header("Extending")]
    public float extendingDistance;
    public float extendingSmoothing;
    public float attackCooldown = 0.5f;

    private float currentExtend;
    private float currentCooldown;

    //private Vector3 mouse_initialPosition;
    //private Vector3 movement_initialPosition;

    //private void Start()
    //{
    //    mouse_initialPosition = mouseSwayObj.localPosition;
    //    movement_initialPosition = movementSwayObj.localPosition;
    //}

    private void Update()
    {
        MouseSway();
        MovementSway();
        MovementBob();
        Extend();
    }

    private void MouseSway()
    {
        // Moves the ball when you move the mouse
        float invert = invertMouse ? -1 : 1;
        Vector2 desiredMovement = new Vector2(invert * Input.GetAxis("Mouse X"), invert * Input.GetAxis("Mouse Y")) * mouse_swayAmount;
        desiredMovement.x = Mathf.Clamp(desiredMovement.x, -mouse_maxAmount, mouse_maxAmount);
        desiredMovement.y = Mathf.Clamp(desiredMovement.y, -mouse_maxAmount, mouse_maxAmount);

        //Vector3 finalPosition = new Vector3(desiredMovement.x, 0, desiredMovement.y);
        mouseSwayObj.localPosition = Vector3.Lerp(mouseSwayObj.localPosition, (Vector3)desiredMovement, Time.deltaTime * mouse_smoothAmount);
    }

    private void MovementSway()
    {
        // Moves the ball when you move the character
        float invert = invertMovement ? -1 : 1;
        Vector3 desiredMovement = invert * movement.LocalActualVelocity * movement_swayAmount;
        desiredMovement.x = Mathf.Clamp(desiredMovement.x, -movement_maxAmount, movement_maxAmount);
        desiredMovement.y = -1 * verticalMultiplier * Mathf.Clamp(desiredMovement.y,
            -movement_maxAmount * verticalMultiplier, movement_maxAmount * verticalMultiplier);
        desiredMovement.z = Mathf.Clamp(desiredMovement.z, -movement_maxAmount, movement_maxAmount);
        movementSwayObj.localPosition = Vector3.Lerp(movementSwayObj.localPosition, desiredMovement, Time.deltaTime * movement_smoothAmount);
    }

    private void MovementBob()
    {
        // Makes the ball bob back and forth when you move
        Vector3 actualHorizontalVelocity = movement.LocalActualVelocity.Flattened();

        float velocityMag = actualHorizontalVelocity.magnitude;
        time += Time.deltaTime * bobSpeed * velocityMag;

        float sinValue = Mathf.Sin(time);
        Vector3 offset;

        if (movement.grounded)
        {
            offset = new Vector3(sinValue, -Mathf.Abs(sinValue)) * bobAmount;
            offset += movingOffset * velocityMag;
        }
        else
            offset = airOffset;

        movementBobObj.localPosition = Vector3.Lerp(movementBobObj.localPosition, offset, Time.deltaTime * smoothSpeed);
        //Vector3 movement;
    }

    private void Extend()
    {
        // Lets you extend the ball with left click
        //Vector3 offset = Vector3.zero;

        currentExtend -= Time.deltaTime * extendingSmoothing;
        currentExtend = Mathf.Max(currentExtend, 0);

        currentCooldown -= Time.deltaTime;
        currentCooldown = Mathf.Max(currentCooldown, 0);

        cooldownImage.fillAmount = Mathf.InverseLerp(0, attackCooldown, currentCooldown);

        if (Input.GetKeyDown(KeyCode.Mouse0) && currentCooldown <= 0)
        {
            currentExtend = extendingDistance;
            currentCooldown = attackCooldown;
        }
            //offset = Vector3.forward * extendingDistance;

        extendObj.localPosition = Vector3.Lerp(extendObj.localPosition, Vector3.forward * currentExtend, Time.deltaTime * extendingSmoothing);
    }
}
