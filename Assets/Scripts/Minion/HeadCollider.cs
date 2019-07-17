using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// контроллер попаданий в голову
/// </summary>
public class HeadCollider : ColliderHitController, IDamagable
{
    public override void InitColliderHit(EnemyController _enemyController)
    {
        base.InitColliderHit(_enemyController);
        onHitCollider = enemyController.OnHeadHit;
    }   
}
