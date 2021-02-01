using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Система опознавания свой чужой friend or foe system
/// </summary>
public class IFFSystem : MonoBehaviour
{
     public static bool IsFoe(IIdentitable you, IIdentitable he)
    {
        bool result = false;

        result = you.Id != "MainPlayer" && he.Id == "MainPlayer";
        result |= you.Id == "MainPlayer" && he.Id != "MainPlayer";

        return result;
    }
}
