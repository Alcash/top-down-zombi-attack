using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер попадания пули
/// </summary>
public class BulletController : MonoBehaviour {

    [SerializeField]
    private float lifeTime = 5;
    private IPersonController owner  = null;
    private int damage;
    [SerializeField]
    private float bulletVelocity = 3;


    void Start () {
        
        Destroy(gameObject, lifeTime);
        Invoke("EnableCollider", 0.2f);
        GetComponent<Rigidbody>().velocity = transform.forward * bulletVelocity;
       
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
        IDamagable damagable = other.GetComponent<IDamagable>();
        if(damagable != null)
            damagable.TakeHit(new HitData(gameObject, owner));

       Destroy(gameObject);        
    }

    public IPersonController GetOwner()
    {
        return owner;
    }
}
