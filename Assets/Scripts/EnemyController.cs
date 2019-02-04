using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(LevelController))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour, IPersonController
{

    public int m_Score = 1;
    public float m_ChanceSpawn = 0.3f;

    Rigidbody m_Rigidbody;
    bool target;
    Health m_Heath;
    LevelController _levelController;
    int m_MovementSpeed;
    

    public void Death()
    {
        GameController.singleton.AddScore(m_Score);
        Destroy(gameObject);
    }

    // Use this for initialization
    void Awake () {
       // Debug.Log("Awake " + name);
        m_Heath = GetComponent<Health>();
        m_Rigidbody = GetComponent<Rigidbody>();
        //Debug.Log("m_Rigidbody " + m_Rigidbody.name);
        _levelController = GetComponent<LevelController>();
        m_MovementSpeed =  _levelController.MovementOnStart + _levelController.Level * _levelController.MovementPerLvl;
        _levelController.Person = this;

    }
	
    void AddScore(int value)
    {

    }

    public void LevelUp(int value)
    {
        m_MovementSpeed = _levelController.MovementOnStart + _levelController.Level * _levelController.MovementPerLvl;

    }

    public void SetTarget(Vector3 _target)
    {
        var dir = _target - transform.position;
        //Debug.DrawLine(transform.position, _target);
        //Debug.Log("transform.position" + transform.position);
        m_Rigidbody.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        target = true;
        Move();
    }

    void Move()
    {
        if(target)
            m_Rigidbody.velocity = m_Rigidbody.transform.forward * m_MovementSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Health>().Damage(_levelController.DamageOnStart);
            Destroy(gameObject);
        }
    }

}
