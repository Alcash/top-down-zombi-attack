using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// базовый контроллер попаданий
/// </summary>
public class ColliderHitController : MonoBehaviour, IDamagable
{
    protected EnemyController enemyController = null;

    protected delegate void OnHit(HitData hitData);

    protected OnHit onHitCollider = null;

    /// <summary>
    /// Инициализация контрллера попаданий
    /// </summary>
    /// <param name="_enemyController"></param>
    public virtual void InitColliderHit(EnemyController _enemyController)
    {
        enemyController = _enemyController;
    }

    /// <summary>
    /// Получение попадания от personController
    /// </summary>
    /// <param name="personController"></param>
    public void TakeHit(HitData hitData)
    {       
        onHitCollider(hitData);
    }    
}
