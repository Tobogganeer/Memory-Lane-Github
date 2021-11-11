using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform target;

    public CopyMode position;
    public CopyMode rotation;
    public UpdateLoop loop;

    public Vector3 posOffset;
    public Vector3 rotOffset;

    private void Update()
    {
        if (loop == UpdateLoop.Update) CopyTransforms();
    }

    private void FixedUpdate()
    {
        if (loop == UpdateLoop.FixedUpdate) CopyTransforms();
    }

    private void LateUpdate()
    {
        if (loop == UpdateLoop.LateUpdate) CopyTransforms();
    }

    public void CopyTransforms()
    {
        if (target == null) return;

        if (position == CopyMode.Global) transform.position = target.position + posOffset;
        else if (position == CopyMode.Local) transform.localPosition = target.localPosition + posOffset;

        if (rotation == CopyMode.Global) transform.rotation = GetRotWithOffset(target.rotation, rotOffset);
        else if (rotation == CopyMode.Local) transform.localRotation = GetRotWithOffset(target.localRotation, rotOffset);
    }

    public Quaternion GetRotWithOffset(Quaternion quaternion, Vector3 offset)
    {
        quaternion *= Quaternion.Euler(Vector3.up * offset.y);
        quaternion *= Quaternion.Euler(Vector3.right * offset.x);
        quaternion *= Quaternion.Euler(Vector3.right * offset.z);
        return quaternion;
        //return Quaternion.Euler(quaternion.eulerAngles + offset);
    }

    public enum CopyMode
    {
        None,
        Local,
        Global
    }

    public enum UpdateLoop
    {
        Update,
        LateUpdate,
        FixedUpdate
    }
}
