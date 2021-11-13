using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float force = 100;
    public float explodeTime = 3;

    public float explosionForce = 50;
    public float explosionRadius = 2;
    public float upwardsMult = 1;

    public float damage = 75;

    public string enemyTag = "Enemy";
    public LayerMask explosionLayerMask;

    private static Collider[] colliderPool;
    private const int COLLIDER_POOL_SIZE = 100;

    private bool exploded;

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
        Invoke(nameof(Explode), explodeTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(enemyTag))
    //    {
    //        Explode();
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(enemyTag))
        {
            Explode();
        }
    }

    private void Explode()
    {
        CancelInvoke();

        if (exploded) return;

        exploded = true;

        if (colliderPool == null) colliderPool = new Collider[COLLIDER_POOL_SIZE];

        int colliders = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, colliderPool, explosionLayerMask);
        if (colliders > 0)
        {
            for (int i = 0; i < colliders; i++)
            {
                if (colliderPool[i] != null)
                {
                    if (colliderPool[i].TryGetComponent(out IExplosionDamagable damagable)) damagable.
                            TakeExplosiveDamage(DamageDetails.NoDir(GetActualDamage(damage, transform.position.SqrDistance(colliderPool[i].transform.position)), WeaponType.None));

                    //if (colliderPool[i].TryGetComponent(out Ragdoller ragdoller)) ragdoller.EnableRagdoll();

                    Rigidbody rb = colliderPool[i].attachedRigidbody;

                    if (rb != null)
                    {
                        //colliderPool[i].attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsMult, ForceMode.Impulse);
                        rb.AddForce((transform.position.DirectionTo(rb.position) + new Vector3(0, upwardsMult)) * explosionForce, ForceMode.Impulse);
                        rb.AddTorque(OptimizedRandom.vector01 * 100, ForceMode.Impulse);
                        //Debug.Log(colliderPool[i].transform.name);
                    }
                }
            }
        }
        Destroy(gameObject);
        FX.SpawnVisualEffect(VisualEffect.Explosion, transform.position);
        AudioManager.Play(AudioArray.Explosion, transform.position, null, 50);
    }

    public static float GetActualDamage(float baseDamage, float sqrDistance)
    {
        return baseDamage * Mathf.Clamp((1f / sqrDistance) + 0.5f, 0.2f, 2f);
    }
}
