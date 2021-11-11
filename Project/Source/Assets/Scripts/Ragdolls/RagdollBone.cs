using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollBone : MonoBehaviour
{
    private Rigidbody rb;
    //private new Collider collider;

    private Vector3 velocity;
    private Vector3 angularVelocity;

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        //velocity = (lastPosition - transform.position) / Time.deltaTime;
        velocity = (transform.position - lastPosition) / Time.deltaTime;

        //Quaternion q = transform.rotation * Quaternion.Inverse(lastRotation);
        Quaternion q = lastRotation * Quaternion.Inverse(transform.rotation);
        Vector3 axis;
        float angle;
        q.ToAngleAxis(out angle, out axis);

        angularVelocity = (axis * angle) / Time.deltaTime;

        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    public void Enable()
    {
        rb.isKinematic = false;
        //collider.enabled = true;

        rb.velocity = velocity * Random.Range(0.5f, 2.0f);
        rb.angularVelocity = angularVelocity * Random.Range(0.5f, 2.0f);
    }

    public void Disable()
    {
        rb.isKinematic = true;
        //collider.enabled = false;
    }

    public void SetState(bool active)
    {
        if (active)
            Enable();
        else
            Disable();
    }

    public void AddForce(Vector3 force, ForceMode forceMode)
    {
        rb.AddForce(force, forceMode);
    }
}