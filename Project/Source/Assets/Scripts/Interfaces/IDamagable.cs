using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(DamageDetails details);
}

public struct DamageDetails
{
    public float amount;
    public Vector3 origin;
    public Vector3 direction;

    public DamageDetails(float amount, Vector3 origin, Vector3 direction)
    {
        this.amount = amount;
        this.origin = origin;
        this.direction = direction;
    }

    public static DamageDetails NoDir(float amount)
    {
        return new DamageDetails(amount, Vector3.zero, Vector3.zero);
    }
}
