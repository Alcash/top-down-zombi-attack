using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade
{
    public class EnemyManager : MonoBehaviour
    {
       
        private SpawnData_SO[] enemyDataList = null;

        private float[] chancesSpawn = null;

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
            enemyDataList = Resources.LoadAll<SpawnData_SO>("MinionDatas/");
            chancesSpawn = new float[enemyDataList.Length + 1];
           
            chancesSpawn[0] = 0;


            for (int i = 0; i < enemyDataList.Length; i++)
            {
                chancesSpawn[i + 1] = chancesSpawn[i] + enemyDataList[i].SpawnChance;
            }

            _SecondsBetweenSpawn = m_MaxSecondsBetweenSpawn;
            _NumberWave = 0;
        }

        public void EnableSpawn(bool value)
        {
            spawn = value;
            StartCoroutine(RepeatSpawn());
            StartCoroutine(RepeatInscreaseDifficulty());
        }

        void Spawn()
        {
            GameObject spawnEnemyPrefab = GetRandomEnemy();

            var randSpawn = Random.value;
            var x = PlayerPosition.position.x + m_RadiusSpawn * Mathf.Cos(randSpawn * Mathf.PI * 2);
            var y = PlayerPosition.position.z + m_RadiusSpawn * Mathf.Sin(randSpawn * Mathf.PI * 2);

            var spawnEnemy = Instantiate(spawnEnemyPrefab, new Vector3(x, 0, y), new Quaternion());

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

        private GameObject GetRandomEnemy()
        {
            SpawnData_SO spawnEnemyData = enemyDataList[0];
            float rand = Random.value;          
            for (int i = 1; chancesSpawn[i] < rand; i++)
            {
                spawnEnemyData = enemyDataList[i];
            }

            return spawnEnemyData.SpawnEnemy;
        }

        void InscreaseDifficulty()
        {
            _NumberWave += 1;
            var tempSeconds = m_MaxSecondsBetweenSpawn - _NumberWave * m_SecondsBetweenSpawnPerWave;
            Debug.Log("tempSeconds " + tempSeconds);
            _SecondsBetweenSpawn = Mathf.Max(m_MinSecondsBetweenSpawn, Random.Range(m_MinSecondsBetweenSpawn, tempSeconds));
        }
    }
}