using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    private void Awake()
    {
        instance = this;
    }

    public TMP_Text ammoCounter;

    public static void SetAmmoCounterText(string text)
    {
        instance.ammoCounter.text = text;
    }

    public static void SetAmmoCounterText(int lhs, int rhs, int extra)
    {
        instance.ammoCounter.text = lhs + " / " + rhs + " - " + extra;
    }
}
