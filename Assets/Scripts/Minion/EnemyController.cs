using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(LevelController))]
public class EnemyController : MonoBehaviour, IPersonController, IIdentitable
{
    public int m_Score = 1;   
    private Rigidbody rigidbodyThis;
    private bool target;
    private Vector3 targetPos;
    private Health health;
    private LevelController levelController;
    private float targetMovementSpeed;
    private float currentPercentSpeed;
    private Animator animator;

    private string myId = "Minion";

    string IIdentitable.Id => myId;

    GameObject IIdentitable.GetGameObject => gameObject;

    private IIdentitable myIdentity;

    private List<IIdentitable> identitables;

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
        animator = GetComponentInChildren<Animator>();
        var colliders = GetComponentsInChildren<ColliderHitController>();

        foreach (var item in colliders)
        {
            item.InitColliderHit(this);
        }

        levelController = GetComponent<LevelController>();
        targetMovementSpeed =  levelController.MovementAtLevel;

        levelController.OnLevelChange += LevelUp;

        myIdentity = GetComponent<IIdentitable>();
    }

    private void OnEnable()
    {
        UnitsDataBase.AddNewUnit(gameObject);

        myId = myId + UnitsDataBase.GetMyId(gameObject);

        identitables = new List<IIdentitable>();
        MonoBehaviour[] sceneObjects = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < sceneObjects.Length; i++)
        {
            MonoBehaviour currentObj = sceneObjects[i];
            IIdentitable currentComponent = currentObj.GetComponent<IIdentitable>();

            if (currentComponent != null)
            {
                identitables.Add(currentComponent);
            }
        }


        foreach (var item in identitables)
        {
            Debug.Log(item.GetGameObject);
            if (IFFSystem.IsFoe(myIdentity, item))
            {
                SetTarget(item.GetGameObject.transform);
                break;
            }
        }
    }

    public void LevelUp(int level)
    {
        targetMovementSpeed = levelController.MovementAtLevel;
        health.LevelUp();
    }

    private void FixedUpdate()
    {
        Move(Time.fixedDeltaTime);
        
    }

    private void AddScore(int value)
    {
    }   

    public void SetTarget(Transform _target)
    {
        targetPos = _target.position;
        target = true;

        var dir = targetPos - transform.position;
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);       
    }

    private void Move(float delta)
    {
        if (target)
        {           
            currentPercentSpeed = Mathf.Clamp(currentPercentSpeed + delta, 0, 1);
            transform.Translate(Vector3.forward * currentPercentSpeed * targetMovementSpeed * delta , Space.Self);
            animator.SetFloat("Forward", currentPercentSpeed);
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

    private void OnDestroy()
    {
        UnitsDataBase.RemoveUnit(gameObject);
    }
}
