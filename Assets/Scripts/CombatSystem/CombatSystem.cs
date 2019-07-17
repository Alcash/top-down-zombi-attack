using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public static void CalculateDamage(IPersonController reciever, IPersonController transmiter)
    {      
        reciever.GetHealth().TakeDamage(transmiter.GetLevel().DamageAtLevel);
    }

    internal static void CalculateCriticalDamage(IPersonController reciever, IPersonController transmiter)
    {
        reciever.GetHealth().TakeDamage(transmiter.GetLevel().CriticalDamageAtLevel);
    }
}
