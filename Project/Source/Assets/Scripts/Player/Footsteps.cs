using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public Transform footSource;

    private void OnEnable()
    {
        WeaponSway.OnFootstep += WeaponSway_OnFootstep;
        PlayerMovement.OnLand += PlayerMovement_OnLand;
    }

    private void OnDisable()
    {
        WeaponSway.OnFootstep -= WeaponSway_OnFootstep;
        PlayerMovement.OnLand -= PlayerMovement_OnLand;
    }

    private void WeaponSway_OnFootstep(Foot foot, float magnitude)
    {
        AudioManager.Play(GetSound(foot), footSource.position, footSource);
    }

    private void PlayerMovement_OnLand(float airtime)
    {
        if (airtime > 0.3f) AudioManager.Play(AudioArray.Jump, footSource.position, footSource, 10, 0.75f);
    }

    private AudioArray GetSound(Foot foot)
    {
        switch (foot)
        {
            case Foot.Left:
                return AudioArray.LeftFoot;
            case Foot.Right:
                return AudioArray.RightFoot;
        }

        return AudioArray.Null;
    }
}
