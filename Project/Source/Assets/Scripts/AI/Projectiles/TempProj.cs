using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempProj : EnemyProjectile
{
    public Rigidbody rb;
    public float startingForce = 35;
    public float damage = 15;

    private bool destroyed;

    public override void Init(Vector3 targetPos, Vector3 origin)
    {
        rb.AddForce(transform.position.DirectionTo(targetPos) * startingForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (destroyed) return;

        if (collision.transform.CompareTag("Player"))
        {
            Player.TakeDamage(damage);
        }
        else if (collision.transform.TryGetComponent(out IBulletDamagable damagable))
        {
            damagable.TakeBulletDamage(DamageDetails.NoDir(damage));
        }

        Destroy(gameObject);
        destroyed = true;
    }
}
