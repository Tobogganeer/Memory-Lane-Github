using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnimation : MonoBehaviour
{
    public Transform animatedBone;
    public Vector3 offset = new Vector3(-90, 0, 0);

    private void Update()
    {
        Quaternion offsetRot = GetRotWithOffset(animatedBone.localRotation, offset);
        Vector3 eulers = offsetRot.eulerAngles;

        eulers.y = eulers.z;
        eulers.z = 0;

        //Vector3 correctedEulers = new Vector3(correctedEulers)
        transform.localRotation = Quaternion.Euler(eulers);
    }

    public Quaternion GetRotWithOffset(Quaternion quaternion, Vector3 offset)
    {
        quaternion *= Quaternion.Euler(Vector3.up * offset.y);
        quaternion *= Quaternion.Euler(Vector3.right * offset.x);
        quaternion *= Quaternion.Euler(Vector3.forward * offset.z);
        return quaternion;
        //return Quaternion.Euler(quaternion.eulerAngles + offset);
    }
}
