using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnimTest : MonoBehaviour
{
    public Animator anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            anim.Play("Debug");
            Debug.Log("Played Debug");
        }
    }
}
