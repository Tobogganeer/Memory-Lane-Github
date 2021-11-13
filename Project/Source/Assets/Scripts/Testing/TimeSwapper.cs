using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSwapper : MonoBehaviour
{
    public float slow = 0.25f;
    public float normal = 1f;

    private bool slowing;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma)) slowing = !slowing;

        float target = slowing ? slow : normal;
        Time.timeScale = Mathf.Lerp(Time.timeScale, target, Time.unscaledDeltaTime * 5);
    }
}
