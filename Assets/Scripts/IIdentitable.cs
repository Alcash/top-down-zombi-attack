using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Интерфейс уникальности
/// </summary>
public interface IIdentitable
{
    string Id
    {
        get;
    }

    GameObject GetGameObject
    {
        get;
    }
}
