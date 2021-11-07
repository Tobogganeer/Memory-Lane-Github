using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    public static FX instance;
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

        InitDicts();
    }

    private void InitDicts()
    {
        vfx.Clear();
        foreach (InspectorVisualEffect effect in i_vfx)
            vfx.Add(effect.type, effect.prefab);
    }

    public InspectorVisualEffect[] i_vfx;
    private static Dictionary<VisualEffect, GameObject> vfx = new Dictionary<VisualEffect, GameObject>();


    public GameObject tracerPrefab;

    public static void SpawnTracer(Vector3 spawnOrigin, Vector3 endPoint)
    {
        Instantiate(instance.tracerPrefab, spawnOrigin, Quaternion.LookRotation(spawnOrigin.DirectionTo(endPoint), Vector3.up), instance.transform).GetComponent<Tracer>().Init(spawnOrigin, endPoint);
    }

    public static void SpawnTracer(Vector3 spawnOrigin, Vector3 dir, ref BallisticsResult result)
    {
        if (result.empty)
        {
            SpawnTracer(spawnOrigin, spawnOrigin + dir * 200);
        }
        else
        {
            PenetrationData lastPen = result.lastPenetration;
            if (lastPen.exited)
                SpawnTracer(spawnOrigin, spawnOrigin + dir * 200);
            else
                SpawnTracer(spawnOrigin, lastPen.entryPoint);
        }
    }

    public static void SpawnVisualEffect(VisualEffect type, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (!vfx.TryGetValue(type, out GameObject effect))
        {
            Debug.LogWarning("Tried to spawn " + type + ", but there is no effect assigned for that!");
            return;
        }

        Instantiate(effect, position, rotation, parent);
    }

    public static void SpawnVisualEffect(VisualEffect type, Vector3 position, Transform parent = null)
    {
        SpawnVisualEffect(type, position, Quaternion.identity, parent);
    }
}

[System.Serializable]
public class InspectorVisualEffect
{
    public VisualEffect type;
    public GameObject prefab;
}
