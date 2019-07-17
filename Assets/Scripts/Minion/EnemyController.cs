using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(LevelController))]
public class EnemyController : MonoBehaviour, IPersonController
{
    public int m_Score = 1;   
    private Rigidbody rigidbodyThis;
    private bool target;
    private Vector3 targetPos;
    private Health m_Heath;
    private LevelController levelController;
    private int movementSpeed;    

    public void Death()
    {
        GameController.singleton.AddScore(m_Score);
        Destroy(gameObject);
    }

    private void Awake ()
    {       
        m_Heath = GetComponent<Health>();
        rigidbodyThis = GetComponent<Rigidbody>();

        var colliders = GetComponentsInChildren<ColliderHitController>();

        foreach (var item in colliders)
        {
            item.InitColliderHit(this);
        }

        levelController = GetComponent<LevelController>();
        movementSpeed =  levelController.MovementAtLevel;

      
    }

    private void AddScore(int value)
    {
    }   

    public void SetTarget(Vector3 _target)
    {
        targetPos = _target;
        target = true;

        var dir = targetPos - transform.position;
        rigidbodyThis.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

        Move();
    }

    private void Move()
    {
        if(target)
            rigidbodyThis.velocity = rigidbodyThis.transform.forward * movementSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CombatSystem.CalculateDamage(other.GetComponent<IPersonController>(), this);
            Destroy(gameObject);
        }
    }

    public Health GetHealth()
    {
        return m_Heath;
    }

    public LevelController GetLevel()
    {
        return levelController;
    }

    internal void OnHeadHit(HitData hitData)
    {
        CombatSystem.CalculateCriticalDamage(this, hitData.Owner);
    }

    internal void OnBodyHit(HitData hitData)
    {
        CombatSystem.CalculateDamage(this, hitData.Owner);         
    }
}
