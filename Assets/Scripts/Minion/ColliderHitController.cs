using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// базовый контроллер попаданий
/// </summary>
public class ColliderHitController : MonoBehaviour, IDamagable
{
    protected EnemyController enemyController = null;

    protected delegate void OnHit(IPersonController personController);

    protected OnHit onHitCollider = null;

    /// <summary>
    /// Инициализация контрллера попаданий
    /// </summary>
    /// <param name="_enemyController"></param>
    public virtual void InitColliderHit(EnemyController _enemyController)
    {
        enemyController = _enemyController;
    }

    public void TakeHit(IPersonController personController)
    {       
        onHitCollider(personController);
    }    
}
