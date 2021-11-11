using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRB : MonoBehaviour
{
    public Rigidbody rb;
    public float maxSpeed = 5;
    public float maxAcceleration = 3;

    private Vector3 desiredVelocity;
    private Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();

        Vector2 input;
        input.x = Inputs.Horizontal;
        input.y = Inputs.Vertical;
        input = Vector2.ClampMagnitude(input, 1f);

        desiredVelocity = new Vector3(input.x, 0f, input.y) * maxSpeed;
    }

    void FixedUpdate()
    {
        velocity = rb.velocity;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z =
            Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        rb.velocity = velocity;
    }
}
