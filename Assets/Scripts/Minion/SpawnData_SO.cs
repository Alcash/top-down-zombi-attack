using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SO для выставления шансы появления монстра
/// </summary>
[CreateAssetMenu(fileName = "New Minion Data", menuName = "minionData", order = 51)]
public class SpawnData_SO : ScriptableObject
{
    [SerializeField]
    private float spawnChance = 1;
    [SerializeField]
    private GameObject spawnEnemy = null;

    public float SpawnChance
    {
        get
        {
           return spawnChance;
        }
    }

    public GameObject SpawnEnemy
    {
        get
        {
            return spawnEnemy;
        }
    }
}
