using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Primary : MonoBehaviour
{
    public bool hasCoolDown;
    public bool canUse;
    public float cooldown;

    public float criticalStrikeChance = 0;
    public float criticalStrikeDamage = 1;

    public int critChance = 11;

    public float criticalStrikeCheck( float Damage)
    {
        float tempExtraModifier;
        if (Random.Range(1, critChance) <= criticalStrikeChance * 10)
        {
            tempExtraModifier = criticalStrikeDamage;
        }
        else
            tempExtraModifier = 0;

        return Damage += Damage * tempExtraModifier;


        
    }
}
