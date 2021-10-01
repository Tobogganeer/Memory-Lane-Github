using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public static Weapons instance;

    [SerializeField]
    private WeaponProfile[] weaponProfiles;
    private static Dictionary<WeaponType, WeaponProfile> profiles = new Dictionary<WeaponType, WeaponProfile>();

    private void Awake()
    {
        if (instance == null) instance = this;

        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

        profiles.Clear();

        foreach (WeaponProfile profile in weaponProfiles)
        {
            profiles.Add(profile.Type, profile);
        }
    }

    public static WeaponProfile GetProfile(WeaponType type)
    {
        return profiles[type];
    }
}
