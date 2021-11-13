using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumExtensions
{
    public static string GetName(this WeaponType weapon)
    {
        return weapon switch
        {
            WeaponType.FNAL => "FNAL",
            WeaponType.GR3_N => "GR3-N",
            WeaponType.Molkor => "Molkor",
            WeaponType.Nateva => "Nateva",
            WeaponType.P3K => "P3K",
            WeaponType.XRM => "XRM",
            _ => throw new System.NotImplementedException()
        };
    }
}
