using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Arcade
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(LevelController))]
    public class PlayerController : MonoBehaviour, IPersonController
    {       
        [SerializeField]
        private ParticleSystem m_ParticleSystem = null;
        [SerializeField]
        private BaseGunController gunController = null;
        private Health health = null;
        private LevelController levelController = null;
        private Animator animator = null;
        private Rigidbody m_Rigidbody = null;
        private Camera m_MainCamera = null;
        
        private float input_vertictal = 0;
        private float input_horizontal = 0;
        private Vector3 Look_Direction = Vector3.zero;
        private Vector3 Mouse_Touch = Vector3.zero;
        private bool Attack = false;

        private void Start()
        {
            health = GetComponent<Health>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_MainCamera = Camera.main;
            levelController = GetComponent<LevelController>();

            levelController.OnLevelChange += LevelUp;
            levelController.OnLevelChange += ShowLevelUp;
            m_ParticleSystem.gameObject.SetActive(false);           
            Attack = true;
            gunController.Init(this);
            gunController.AutoShoot = Attack;


            health.OnHit += ShowCurrentHealth;
        }

        private void OnDisable()
        {
            levelController.OnLevelChange -= LevelUp;
            levelController.OnLevelChange -= ShowLevelUp;
            health.OnHit -= ShowCurrentHealth;
        }

        private void ShowLevelUp(int level)
        {
            Debug.Log("ShowLevelUp " + level);
            GameUIController.ShowLevelUP(level);
        }

        private void ShowCurrentHealth(int currentHealth)
        {
            GameUIController.OnUpdateHealth((float)currentHealth / health.MaxHealth);
        }

        public void LevelUp(int value)
        {           
            StartCoroutine(LevelUpParticle());
        }

        private IEnumerator LevelUpParticle()
        {
            m_ParticleSystem.gameObject.SetActive(true);           
            yield return new WaitForSeconds(3);          
            m_ParticleSystem.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Mouse_Touch = Input.mousePosition;
            }
        }

        private void Turn()
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

        public void Death()
        {
            GameController.singleton.Gameover();
        }

        public Health GetHealth()
        {
            return health;
        }

        public LevelController GetLevel()
        {
            return levelController;
        }
    }
}
