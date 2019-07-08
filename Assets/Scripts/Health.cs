using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField]
    int _MaxHealth;

    [SerializeField]
    int _CurrentHealth;
    IPersonController person;

    LevelController _levelController;
    public int MaxHealth
    {
        get
        {
            return _MaxHealth;
        }

        set
        {
            _MaxHealth = value;
        }
    }

    internal void TakeDamage(int damage)
    {
        _CurrentHealth -= damage;
        if (_CurrentHealth < 0)
        {
            _CurrentHealth = 0;
            person.Death();
        }
    }

    // Use this for initialization
    void Awake()
    {
        person = GetComponent<IPersonController>();
       _levelController = GetComponent<LevelController>();
        if(MaxHealth == 0)
            MaxHealth = _levelController.HealthAtLevel;
        if (_CurrentHealth == 0)
            _CurrentHealth = MaxHealth;

    }

    public void LevelUp()
    {       
        MaxHealth =  _levelController.HealthAtLevel;
        _CurrentHealth = MaxHealth;
    }
}
