using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// контроллер попаданий в тело
/// </summary>
public class BodyCollider : ColliderHitController, IDamagable
{
    public override void InitColliderHit(EnemyController _enemyController)
    {
        base.InitColliderHit(_enemyController);
        onHitCollider = enemyController.OnBodyHit;
    }
}
