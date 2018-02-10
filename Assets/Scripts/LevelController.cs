using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    [Header("Main")]
    [SerializeField]
    int m_Level = 0;

    [Header("Gun")]
    [SerializeField]
    int m_DamageOnStart = 5;
    [SerializeField]
    int m_DamagePerLvl = 1;
    

    [Header("Health")]
    [SerializeField]
    int m_HealthOnStart = 10;
    [SerializeField]
    int m_HealthPerLvl = 1;

    [Header("Movement")]
    [SerializeField]
    int m_MovementOnStart = 2;
    [SerializeField]
    int m_MovementPerLvl = 1;

    public int DamageOnStart
    {
        get
        {
            return m_DamageOnStart;
        }

        set
        {
            m_DamageOnStart = value;
        }
    }

    public int DamagePerLvl
    {
        get
        {
            return m_DamagePerLvl;
        }

        set
        {
            m_DamagePerLvl = value;
        }
    }

    public int Level
    {
        get
        {
            return m_Level;
        }

        set
        {
            m_Level = value;
        }
    }

    public int HealthOnStart
    {
        get
        {
            return m_HealthOnStart;
        }

        set
        {
            m_HealthOnStart = value;
        }
    }

    public int HealthPerLvl
    {
        get
        {
            return m_HealthPerLvl;
        }

        set
        {
            m_HealthPerLvl = value;
        }
    }

    public int MovementOnStart
    {
        get
        {
            return m_MovementOnStart;
        }

        set
        {
            m_MovementOnStart = value;
        }
    }

    public int MovementPerLvl
    {
        get
        {
            return m_MovementPerLvl;
        }

        set
        {
            m_MovementPerLvl = value;
        }
    }

    public void LevelUp()
    {

    }

    public void LevelUp(int _levelIncrease)
    {
        Level += _levelIncrease;
        SendMessage("LevelUp");
    }

    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
