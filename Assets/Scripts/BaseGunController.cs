using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGunController : MonoBehaviour
{
    [Header("Gun")]
    [SerializeField]
    private float fireRate = 5;
    private float delayFire = 1;
    private bool canShot = false;
    private bool autoShoot = false;
    [SerializeField]
    private GameObject m_BulletPrefab;
    [SerializeField]
    private Transform m_SpawnPoint;
    [SerializeField]
    private IPersonController owner = null;

    public bool AutoShoot{
        get { return autoShoot; }
        set { autoShoot = value; }
    }

    public void Init(IPersonController _owner)
    {
        owner = _owner;
    }

    private void FixedUpdate()
    {
        if (delayFire > 0)
        {
            delayFire -= 1 / fireRate;
        }
        else
        {
            canShot = true;     
            if(autoShoot)
            {
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        if (canShot)
        {
            canShot = false;
            delayFire = 1;            
            var bullet = Instantiate(m_BulletPrefab, m_SpawnPoint.position, m_SpawnPoint.rotation);
            bullet.GetComponent<BulletController>().Init(owner);
        }
    }
}
