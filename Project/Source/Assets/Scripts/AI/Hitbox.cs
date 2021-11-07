using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour, IBulletDamagable, IExplosionDamagable
{
    public HitboxRegion region;
    public Transform damageReceiver;
    private IDamagable damagable;

    private void Start()
    {
        damagable = damageReceiver.GetComponent<IDamagable>();
    }

    public float GetBulletDamageMultiplier()
    {
        return GetBulletDamageMultiplier(region);
    }

    public static float GetBulletDamageMultiplier(HitboxRegion region)
    {
        switch (region)
        {
            case HitboxRegion.Chest:
                return 1.0f;
            case HitboxRegion.Abdomen:
                return 1.25f;
            case HitboxRegion.Arms:
                return 0.65f;
            case HitboxRegion.Legs:
                return 0.45f;
            case HitboxRegion.Head:
                return 3.5f;
        }

        return 1.0f;
    }

    public float GetExplosiveDamageMultiplier()
    {
        return GetExplosiveDamageMultiplier(region);
    }

    public static float GetExplosiveDamageMultiplier(HitboxRegion region)
    {
        switch (region)
        {
            case HitboxRegion.Chest:
                return 0.4f;
            case HitboxRegion.Abdomen:
                return 0.15f;
            case HitboxRegion.Arms:
                return 0.05f;
            case HitboxRegion.Legs:
                return 0.05f;
            case HitboxRegion.Head:
                return 0.7f;
        }

        // Explosion will likely hit all body parts
        // 0.7 + 0.4 + 0.15 + (0.05 * 10)
        // A lot

        return 0.3f;
    }


    public void TakeBulletDamage(float amount)
    {
        damagable.TakeDamage(GetBulletDamageMultiplier() * amount);
    }

    public void TakeExplosiveDamage(float amount)
    {
        damagable.TakeDamage(GetExplosiveDamageMultiplier() * amount);
    }
}
