using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private void Awake()
    {
        instance = this;
    }

    private CharacterController controller;
    public Player player;

    public CharacterController Controller => instance.controller;

    // Tooltips show text when you hover the cursor above the var in the inspector

    #region Public Variables
    [Header("Change")]
    public MovementProfile movementProfile;
    //[Min(0f)] public float walkingSpeed = 4;
    //[Min(0f)] public float runningSpeed = 6;
    [Min(0f)] public float speedChangeSmoothing = 5;
    private float currentSpeed;

    //public float gravity = 10;
    //
    //[Min(0f)] public float walkingJumpHeight = 3;
    //[Min(0f)] public float runningJumpHeight = 4;
    private float currentJumpHeight;
    private float secondsFromJump;
    private bool justJumped;

    //[Min(0f)] public float groundAcceleration = 12;
    //[Min(0f)] public float airAcceleration = 1;

    public float edgePushMultiplier = 0.5f;


    [Header("Don't change")]
    //[Tooltip(Tooltips.GROUND_SIZE)]
    public float groundedRaycastSize = 0.5f;

    //[Tooltip(Tooltips.GROUND_LENGTH)]
    public float groundedRaycastLength = 0.6f;

    //[Tooltip(Tooltips.DOWNFORCE_SIZE)]
    public float downforceCheckSize = 0.2f;

    //[Tooltip(Tooltips.DOWNFORCE_LENGTH)]
    public float downforceCheckLength = 0.5f;

    //[Tooltip(Tooltips.MAX_FALL_SPEED)]
    [Min(10f)] public float maxFallSpeed = 35;

    //[Tooltip(Tooltips.DOWNFORCE)]
    public float downForce = 3;

    //[Tooltip(Tooltips.GROUND_LAYER)]
    public LayerMask groundLayermask;
    #endregion


    public bool grounded { get; private set; }
    public bool wasGrounded { get; private set; }
    public bool applyDownforce { get; private set; }

    private Vector3 currentVelocity;
    private Vector3 desiredVelocity;
    private Vector3 transformedDesiredVelocity;
    private Vector3 actualVelocity;

    private Vector3 lastPos;

    /// <summary>
    /// The current velocity in local space
    /// </summary>
    public Vector3 CurrentVelocity => currentVelocity;
    /// <summary>
    /// The desired velocity in world space
    /// </summary>
    public Vector3 DesiredVelocity => desiredVelocity;
    /// <summary>
    /// The desired velocity in local space, with speed applied
    /// </summary>
    public Vector3 TransformedDesiredVelocity => transformedDesiredVelocity;
    /// <summary>
    /// The actual velocity of the character, in world space
    /// </summary>
    public Vector3 ActualVelocity => actualVelocity;
    /// <summary>
    /// The actual velocity of the character, in local space
    /// </summary>
    public Vector3 LocalActualVelocity => transform.InverseTransformDirection(actualVelocity);

    public bool Moving => actualVelocity.Flattened().sqrMagnitude > 0.05f && desiredVelocity.sqrMagnitude > 0.05f;
    public bool Sprinting => Input.GetKey(Inputs.Sprint);

    /// <summary>
    /// A value between 0-1 depending on if the player is walking or running
    /// </summary>
    public float NormalizedSpeed => Mathf.InverseLerp(movementProfile.walkingSpeed, movementProfile.runningSpeed, currentSpeed);

    public static event Action<float> OnLand;
    private float airtime;
    private Vector3 currentNormal;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller == null) controller = GetComponent<CharacterController>();

        Inputs.Update();

        UpdateGrounded();

        UpdateSpeed();

        Move();

        UpdateFOV();

        UpdateCrosshair();

        wasGrounded = grounded;
        actualVelocity = (transform.position - lastPos) / Time.deltaTime;
        lastPos = transform.position;
    }

    private void Move()
    {
        //desiredVelocity.x = Input.GetAxis("Horizontal");
        //desiredVelocity.z = Input.GetAxis("Vertical");
        desiredVelocity.x = Inputs.Horizontal;
        desiredVelocity.z = Inputs.Vertical;
        // Sets the desired velocity

        float y = currentVelocity.y;

        Vector3 flatVel = currentVelocity.Flattened();

        transformedDesiredVelocity = transform.right * desiredVelocity.x + transform.forward * desiredVelocity.z;
        transformedDesiredVelocity = Vector3.ClampMagnitude(transformedDesiredVelocity, 1);
        transformedDesiredVelocity *= currentSpeed;

        float accel = grounded ? movementProfile.groundAcceleration : movementProfile.airAcceleration;

        currentVelocity = Vector3.Lerp(flatVel, transformedDesiredVelocity, Time.deltaTime * accel).WithY(y);

        bool jump = Input.GetKeyDown(Inputs.Jump);

        if (grounded)
        {
            if (!wasGrounded)
            {
                OnLand?.Invoke(airtime);
                airtime = 0;
            }

            if (jump)
            {
                float movingJumpBonus = Mathf.Clamp(desiredVelocity.sqrMagnitude, 0, 0.5f); // 0.5 unit bonus if you are moving
                currentVelocity.y = currentJumpHeight + movingJumpBonus; // If you are grounded and want to jump, jump
                justJumped = true;
            }
            else if (applyDownforce && !justJumped) currentVelocity.y = -downForce; // Otherwise, apply downforce
        }
        else
        {
            airtime += Time.deltaTime;
        }

        if (justJumped) secondsFromJump += Time.deltaTime;

        if (secondsFromJump > 0.2f)
        {
            justJumped = false;
            secondsFromJump = 0;
        }

        currentVelocity.y = Mathf.Clamp(currentVelocity.y - movementProfile.gravity * Time.deltaTime, -maxFallSpeed, 5000);
        // Adds gravity and also clamps the max vertical speed

        float angle = Vector3.Angle(Vector3.up, currentNormal);

        if (controller.collisionFlags.HasFlag(CollisionFlags.Below) && angle > controller.slopeLimit)
        {
            Vector3 direction = currentNormal.normalized;//.Flattened().normalized;
            float factor = Mathf.InverseLerp(controller.slopeLimit, 90, angle);
            //factor = Mathf.Max(factor, 0.1f);
            currentVelocity += direction * edgePushMultiplier * factor;
        }

        controller.Move(currentVelocity * Time.deltaTime);
    }

    private void UpdateFOV()
    {
        float value = Moving && Sprinting ? NormalizedSpeed : 0f;
        float multiplier = 1f + value * 0.3f; // Will go between 1 and 1.3

        CameraFOV.Set(multiplier);
    }

    private void UpdateCrosshair()
    {
        float speedMagnitude = actualVelocity.magnitude;
        float multiplier = 1 + speedMagnitude * 0.5f;

        Crosshair.Set(multiplier);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (controller.collisionFlags.HasFlag(CollisionFlags.Below))
            currentNormal = hit.normal;
        else
            currentNormal = Vector3.up;
    }

    private void UpdateGrounded()
    {
        grounded = Physics.CheckSphere(transform.position - new Vector3(0, groundedRaycastLength + controller.skinWidth, 0), groundedRaycastSize, groundLayermask);
        applyDownforce = Physics.CheckSphere(transform.position - new Vector3(0, downforceCheckLength + controller.skinWidth, 0), downforceCheckSize, groundLayermask);

        //grounded = Physics.SphereCast(new Ray(transform.position, Vector3.down), groundedRaycastSize, groundedRaycastLength + controller.skinWidth, groundLayermask);
        //applyDownforce = Physics.SphereCast(new Ray(transform.position, Vector3.down), downforceCheckSize, downforceCheckLength + controller.skinWidth, groundLayermask);
    }

    private void UpdateSpeed()
    {
        float airMultiplier = grounded ? 1f : movementProfile.airSpeedMultiplier;
        WeaponProfile currentProfile = Weapons.GetProfile(player.currentWeapon);

        float baseSpeed = Sprinting && Moving ? movementProfile.runningSpeed : movementProfile.walkingSpeed;
        float desiredSpeed = baseSpeed * airMultiplier * currentProfile.SpeedMultiplier;

        float baseJump = Sprinting && Moving ? movementProfile.runningJumpHeight : movementProfile.walkingJumpHeight;
        float desiredJump = baseJump * currentProfile.JumpMultiplier;

        currentSpeed = Mathf.Lerp(currentSpeed, desiredSpeed, Time.deltaTime * speedChangeSmoothing);
        currentJumpHeight = Mathf.Lerp(currentJumpHeight, desiredJump, Time.deltaTime * speedChangeSmoothing);
    }

    private void OnDrawGizmosSelected()
    {
        if (controller == null) controller = GetComponent<CharacterController>();

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + Vector3.down * (groundedRaycastLength + controller.skinWidth), groundedRaycastSize);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + Vector3.down * (downforceCheckLength + controller.skinWidth), downforceCheckSize);
        
        // Just draws the ground check rays so you can make sure they intersect the ground
    }
}
