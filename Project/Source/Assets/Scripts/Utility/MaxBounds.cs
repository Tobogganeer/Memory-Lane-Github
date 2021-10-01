using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxBounds : MonoBehaviour
{
    public Vector3 min;
    public Vector3 max;

    public Transform returnPoint;

    public bool logWarnings = true;

    private CharacterController controller;
    private Rigidbody rb;

    public bool hasController;
    public bool hasRb;

    private void Start()
    {
        if (returnPoint == null && logWarnings)
            Debug.LogWarning($"No returnPoint set for {name}.");

        if (hasController) controller = GetComponent<CharacterController>();
        if (hasRb) rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (Time.frameCount % 10 == 0)
        {
            CheckTransform();
        }
    }

    private void CheckTransform()
    {
        if (!transform.position.IsWithinBounds(min, max))
        {
            if (hasController) controller.enabled = false;

            transform.position = returnPoint.position;

            if (hasController) controller.enabled = true;

            if (hasRb) rb.velocity = Vector3.zero;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(Vector3.Lerp(min, max, 0.5f), -(min - max).Abs());
        // Negative so box is inside out
    }
}
