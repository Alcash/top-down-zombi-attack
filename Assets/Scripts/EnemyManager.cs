using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour {


    public GameObject m_EnemySoldier;
    EnemyController m_EnemySoldierController;
    public GameObject m_EnemyVeteran;
    EnemyController m_EnemyVeteranController;
    public GameObject m_EnemyTank;
    EnemyController m_EnemyTankController;
   
    [SerializeField]
    Transform PlayerPosition;
    public float m_MaxSecondsBetweenSpawn = 2; 
    public float m_MinSecondsBetweenSpawn = 0.5f;
    public float m_SecondsBetweenSpawnPerWave = 0.1f;
    public float m_SecondsBetweenWave = 10;
    float _SecondsBetweenSpawn;
    int _NumberWave;
    bool spawn;

    public float m_RadiusSpawn = 7;

    // Use this for initialization
    void Start()
    {
        m_EnemySoldierController = m_EnemySoldier.GetComponent<EnemyController>();
        m_EnemyVeteranController = m_EnemyVeteran.GetComponent<EnemyController>();
        m_EnemyTankController = m_EnemyTank.GetComponent<EnemyController>();
        spawn = true;
        _SecondsBetweenSpawn = m_MaxSecondsBetweenSpawn;
        _NumberWave = 0;

        StartCoroutine(RepeatSpawn());
        StartCoroutine(RepeatInscreaseDifficulty());
        
    }
	
    
	// Update is called once per frame
	void Update () {
		
	}

    void Spawn()
    {
        //пока оставим так
        GameObject spawnEnemyPrefab = m_EnemySoldier;
        var rand = Random.value;
        if( rand <= m_EnemyTankController.m_ChanceSpawn)
        {
            spawnEnemyPrefab = m_EnemyTank;
        }
        else if(rand > m_EnemyTankController.m_ChanceSpawn && rand <= m_EnemyVeteranController.m_ChanceSpawn)
        {
            spawnEnemyPrefab = m_EnemyVeteran;
        }
        else if (rand > m_EnemySoldierController.m_ChanceSpawn)
        {
            spawnEnemyPrefab = m_EnemySoldier;
        }

        var randSpawn = Random.value;
        var x = PlayerPosition.position.x + m_RadiusSpawn * Mathf.Cos(randSpawn * Mathf.PI*2);
        var y = PlayerPosition.position.z + m_RadiusSpawn * Mathf.Sin(randSpawn * Mathf.PI*2);

        
       
        var spawnEnemy = Instantiate(spawnEnemyPrefab, new Vector3(x, 0, y), new Quaternion() );

        spawnEnemy.GetComponent<EnemyController>().SetTarget(PlayerPosition.position); ;
        spawnEnemy.GetComponent<LevelController>().LevelUp(_NumberWave);
        
    }

    IEnumerator RepeatSpawn()
    {

        while (spawn)
        {
            Spawn();
            yield return new WaitForSeconds(_SecondsBetweenSpawn);            
            
        }
    }

    IEnumerator RepeatInscreaseDifficulty()
    {

        while (spawn)
        {
            
            yield return new WaitForSeconds(m_SecondsBetweenWave);
            InscreaseDifficulty();
        }
    }

    void InscreaseDifficulty()
    {
        _NumberWave += 1;
        var tempSeconds = m_MaxSecondsBetweenSpawn - _NumberWave * m_SecondsBetweenSpawnPerWave;
        _SecondsBetweenSpawn = Mathf.Min(m_MinSecondsBetweenSpawn, tempSeconds) ;
       
    }
}
