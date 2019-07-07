using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField]
    int _MaxHealth;

    [SerializeField]
    int _CurrentHealth;
    IPersonController m_Person;

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

    internal void Damage(int damage)
    {
        _CurrentHealth -= damage;
        if (_CurrentHealth < 0)
        {
            _CurrentHealth = 0;
            m_Person.Death();
        }
    }

    // Use this for initialization
    void Awake()
    {
        m_Person = GetComponent<IPersonController>();
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
