using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    [SerializeField]
    private float lifeTime;
    private IPersonController owner  = null;
    private int damage;
    [SerializeField]
    private float bulletVelocity = 3;


    void Start () {
        
        Destroy(gameObject, lifeTime);
        Invoke("EnableCollider", 0.1f);
        GetComponent<Rigidbody>().velocity = Vector3.forward * bulletVelocity;
    }
	
    public void Init(IPersonController _owner)
    {
        owner = _owner;
        damage = owner.GetLevel().DamageAtLevel;
    }

    void EnableCollider()
    {
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            CombatSystem.CalculateDamage(other.GetComponent<IPersonController>(), owner);            
            Destroy(gameObject);
        }
    }
}
