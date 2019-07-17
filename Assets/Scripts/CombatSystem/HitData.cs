using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Данные о попадании
/// </summary>
public class HitData
{
    /// <summary>
    /// Владелец наносимого попадания. пример стрелок
    /// </summary>
    public IPersonController Owner { get; protected set; }

    /// <summary>
    /// Отправитель попадания. пример пуля
    /// </summary>
    public GameObject Sender { get; protected set; }

    /// <summary>
    /// Контруктор
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="owner"></param>
    public HitData(GameObject sender, IPersonController owner)
    {
        Owner = owner;
        Sender = sender;
    }
}
