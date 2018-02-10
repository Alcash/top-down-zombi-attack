using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(LevelController))]
public class PlayerController : MonoBehaviour , IPersonController
{

    [Header("Gun")]
    public float m_FireRate = 5;
    public float m_BulletVelocity = 3;
    int curentDamage;

    public GameObject m_BulletPrefab;
    public Transform m_SpawnPoint;

    float secondReload;
    Health m_health;
    LevelController m_levelController;

    Rigidbody m_Rigidbody;
    Camera m_MainCamera;
    // Use this for initialization
    float input_vertictal;
    float input_horizontal;
    Vector3 Look_Direction;
    Vector3 Mouse_Touch;
    bool Attack;


    void Start () {
        m_health = GetComponent<Health>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_MainCamera = Camera.main;
        m_levelController = GetComponent<LevelController>();

        secondReload = 1 / m_FireRate;
        Attack = true;
        curentDamage = m_levelController.DamageOnStart + m_levelController.Level * m_levelController.DamagePerLvl;
        StartCoroutine( RepeatShoot());
        
    }

    public void LevelUp()
    {
        curentDamage = m_levelController.DamageOnStart + m_levelController.Level * m_levelController.DamagePerLvl;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Mouse_Touch = Input.mousePosition;
            
        }

    }

    void Turn()
    {
       
        var dir = LookAtMouse();        
        
        dir.y = 0.0f;
        if (dir.magnitude != 0)
            m_Rigidbody.rotation = Quaternion.LookRotation(dir, Vector3.up);        
    }

    private void FixedUpdate()
    {
        Turn();
    }

    private Vector3 LookAtMouse()
    {
         Vector3 lookPos = new Vector3();
         Ray cameraRay = m_MainCamera.ScreenPointToRay(Input.mousePosition);
         RaycastHit hit;
         if (Physics.Raycast(cameraRay, out hit, LayerMask.GetMask("Ground")))
         {
             lookPos = hit.point;
         }

         Vector3 lookDir = lookPos - transform.position;
         Debug.DrawLine(transform.position, lookPos);
         
         return lookDir;
        
    }

    IEnumerator RepeatShoot()
    {

        while (Attack)
        {

            yield return new WaitForSeconds(secondReload);

            //Instantiate<>(Resources.Load("nameprefab in folder Resources"))
            var bullet = Instantiate(m_BulletPrefab, m_SpawnPoint.position, m_SpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = m_SpawnPoint.forward * m_BulletVelocity;
            bullet.GetComponent<BulletController>().SetDamageValue(curentDamage);
        }
    }

    public void Death()
    {
        GameController.singleton.Gameover();
    }
}
