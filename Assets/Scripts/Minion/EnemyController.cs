﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(LevelController))]
public class EnemyController : MonoBehaviour, IPersonController
{
    public int m_Score = 1;   
    private Rigidbody rigidbodyThis;
    private bool target;
    private Vector3 targetPos;
    private Health health;
    private LevelController levelController;
    private float movementSpeed;    

    public void Death()
    {
        GameController.singleton.AddScore(m_Score);
        Destroy(gameObject);
    }

    private void Awake ()
    {       
        health = GetComponent<Health>();
        health.LevelUp();
        rigidbodyThis = GetComponent<Rigidbody>();

        var colliders = GetComponentsInChildren<ColliderHitController>();

        foreach (var item in colliders)
        {
            item.InitColliderHit(this);
        }

        levelController = GetComponent<LevelController>();
        movementSpeed =  levelController.MovementAtLevel;

        levelController.OnLevelChange += LevelUp;


    }

    public void LevelUp(int level)
    {
        movementSpeed = levelController.MovementAtLevel;
        health.LevelUp();
    }

    private void Update()
    {
        Move(Time.deltaTime);
        
    }

    private void AddScore(int value)
    {
    }   

    public void SetTarget(Vector3 _target)
    {
        targetPos = _target;
        target = true;

        var dir = targetPos - transform.position;
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);       
    }

    private void Move(float delta)
    {
        if (target)
        {
            transform.Translate(Vector3.forward * movementSpeed * delta , Space.Self);
        }
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
        return health;
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
