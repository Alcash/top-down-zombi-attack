using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour {

    public UnityAction<int> OnLevelChange = null;
    public UnityAction<int> OnExperienceChange = null;

    [Header("Main")]
    [SerializeField]
    int level = 0;
    int ScoreToLvl = 100;
    int score;
    [Header("Gun")]
    [SerializeField]
    int damageOnStart = 5;
    [SerializeField]
    int damagePerLvl = 1;
    float criticalMultiplier = 0.5f;

    [Header("Health")]
    [SerializeField]
    int healthOnStart = 10;
    [SerializeField]
    int healthPerLvl = 1;

    [Header("Movement")]
    [SerializeField]
    int movementOnStart = 2;
    [SerializeField]
    int movementPerLvl = 1;

    public IPersonController Person;
    
    public int DamageAtLevel
    {
        get
        {
            return damageOnStart + Level * damagePerLvl;
        }        
    }

    public int CriticalDamageAtLevel
    {
        get
        {
            return (int)(DamageAtLevel * (1 + criticalMultiplier));
        }
    }

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }
    
    public int MovementAtLevel
    {
        get
        {
            return movementOnStart + level * movementPerLvl; ;
        }       
    }

    public int HealthAtLevel
    {
        get
        {
            return healthOnStart + level * healthPerLvl; ;
        }
    }

    private void Start()
    {
        GameController.OnChangeScore += AddScore;
    }

    public void LevelUp(int _levelIncrease)
    {
        Level += _levelIncrease;
       
        if (OnLevelChange != null)
        {
            OnLevelChange(Level);
        }       
    }

    private void AddScore(int value)
    {
        score += value;
        if(OnExperienceChange != null)
        {
            OnExperienceChange(value);
        }

        if (score >= ScoreToLvl)
        {
            score = score - ScoreToLvl;
            LevelUp(1);
        }
    }

    
}
