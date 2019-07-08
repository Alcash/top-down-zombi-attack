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
    private Rigidbody rigidbodyThis;
    private bool target;
    private Health m_Heath;
    private LevelController levelController;
    private int movementSpeed;
    

    public void Death()
    {
        GameController.singleton.AddScore(m_Score);
        Destroy(gameObject);
    }

    // Use this for initialization
    void Awake () {
       // Debug.Log("Awake " + name);
        m_Heath = GetComponent<Health>();
        rigidbodyThis = GetComponent<Rigidbody>();
        //Debug.Log("m_Rigidbody " + m_Rigidbody.name);
        levelController = GetComponent<LevelController>();
        movementSpeed =  levelController.MovementAtLevel;    
    }
	
    void AddScore(int value)
    {

    }   

    public void SetTarget(Vector3 _target)
    {
        var dir = _target - transform.position;
        //Debug.DrawLine(transform.position, _target);
        //Debug.Log("transform.position" + transform.position);
        rigidbodyThis.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        target = true;
        Move();
    }

    void Move()
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
}
