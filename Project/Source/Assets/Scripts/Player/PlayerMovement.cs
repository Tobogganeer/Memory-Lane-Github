using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    // Tooltips show text when you hover the cursor above the var in the inspector

    #region Public Variables
    [Header("Change")]
    //[Tooltip(Tooltips.SPEED)]
    [Min(0f)] public float speed = 5;

    //[Tooltip(Tooltips.GRAVITY)]
    public float gravity = 10;

    //[Tooltip(Tooltips.JUMP_HEIGHT)]
    [Min(0f)] public float jumpHeight = 3;

    //[Tooltip(Tooltips.GROUND_ACCEL)]
    [Min(0f)] public float groundAcceleration = 12;

    //[Tooltip(Tooltips.AIR_ACCEL)]
    [Min(0f)] public float airAcceleration = 1;



    [Header("Don't change")]
    //[Tooltip(Tooltips.GROUND_SIZE)]
    public float groundedRaycastSize = 0.5f;

    //[Tooltip(Tooltips.GROUND_LENGTH)]
    public float groundedRaycastLength = 0.6f;

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

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller == null) controller = GetComponent<CharacterController>();

        grounded = Physics.SphereCast(new Ray(transform.position, Vector3.down), groundedRaycastSize, groundedRaycastLength + controller.skinWidth, groundLayermask);
        applyDownforce = Physics.SphereCast(new Ray(transform.position, Vector3.down), groundedRaycastSize, downforceCheckLength + controller.skinWidth, groundLayermask);
        // Grounded is used to check if you can jump, Downforce used on ramps so you dont bump down them

        desiredVelocity.x = Input.GetAxis("Horizontal");
        desiredVelocity.z = Input.GetAxis("Vertical");
        // Sets the desired velocity

        float y = currentVelocity.y;
        // Stores the gravity of the current velocity so we can re-apply it later

        Vector3 flatVel = currentVelocity.Flattened();
        // The current velocity with no y value, just the horizontal plane

        transformedDesiredVelocity = transform.right * desiredVelocity.x + transform.forward * desiredVelocity.z;
        // Transforms the velocity and desired direction from world space to local space and stores them

        transformedDesiredVelocity = Vector3.ClampMagnitude(transformedDesiredVelocity, 1);
        // Clamps the magnitude to 1 so going diagonal isn't faster

        transformedDesiredVelocity *= speed;
        // Multiplies the desired velocity by speed

        float accel = grounded ? groundAcceleration : airAcceleration;
        // Decides whether to use the ground acceleration or the air acceleration,
        // depending if you are grounded or not

        currentVelocity = Vector3.Lerp(flatVel, transformedDesiredVelocity, Time.deltaTime * accel).WithY(y);
        // Smoothes the current velocity towards the desired velocity, and also adds the gravity back

        bool jump = Input.GetKeyDown(KeyCode.Space);

        if (grounded)
        {
            if (jump) currentVelocity.y = jumpHeight; // If you are grounded and want to jump, jump
            else if (applyDownforce) currentVelocity.y = -downForce; // Otherwise, apply downforce
        }

        currentVelocity.y = Mathf.Clamp(currentVelocity.y - gravity * Time.deltaTime, -maxFallSpeed, 5000);
        // Adds gravity and also clamps the max vertical speed

        controller.Move(currentVelocity * Time.deltaTime);
        // Actually moves the controller

        wasGrounded = grounded; // Not currently used value, but good to have
        actualVelocity = (transform.position - lastPos) / Time.deltaTime; // Calculates actual velocity in the world
        lastPos = transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        if (controller == null) controller = GetComponent<CharacterController>();

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + Vector3.down * (groundedRaycastLength + controller.skinWidth), groundedRaycastSize);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + Vector3.down * (downforceCheckLength + controller.skinWidth), groundedRaycastSize);
        
        // Just draws the ground check rays so you can make sure they intersect the ground
    }
}
