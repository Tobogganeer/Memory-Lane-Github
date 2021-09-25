using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public RectTransform rectTransform;
    public Color crosshairColour;
    public Color outlineColour;
    public float defaultSize = 50;

    private static float multiplier = 1;
    private float currentSize;
    public float sizeChangeSpeed = 5;
    public float minMultiplier = 0.5f;
    public float maxMultiplier = 5;

    public void ApplySize()
    {
        rectTransform.sizeDelta = new Vector2(defaultSize, defaultSize);
    }

    private void SetSize(float size)
    {
        rectTransform.sizeDelta = new Vector2(size, size);
    }

    private void Update()
    {
        currentSize = Mathf.Lerp(currentSize, defaultSize * Mathf.Clamp(multiplier, minMultiplier, maxMultiplier), Time.deltaTime * sizeChangeSpeed);
        SetSize(currentSize);
    }

    public static void Set(float multiplier)
    {
        Crosshair.multiplier = multiplier;
    }
}
