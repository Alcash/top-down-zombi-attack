using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float m_LifeTime;
    int damage;
	// Use this for initialization
	void Start () {
        
        Destroy(gameObject, m_LifeTime);
        Invoke("EnableCollider", 0.01f);
	}
	
    public void SetDamageValue(int _damage)
    {
        damage = _damage;
    }

	// Update is called once per frame
	void Update () {
		
	}

    void EnableCollider()
    {
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")

        {
            other.GetComponent<Health>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
