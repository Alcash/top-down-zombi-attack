using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade
{
    /// <summary>
    /// Простой менеджер противников
    /// </summary>
    public class EnemyManager : AbstractEnemyManager
    {
       
        private SpawnData_SO[] enemyDataList = null;

        private float[] chancesSpawn = null;
               
        Transform playerTransform;
        public float m_MaxSecondsBetweenSpawn = 2;
        public float m_MinSecondsBetweenSpawn = 1f;
        public float m_SecondsBetweenSpawnPerWave = 0.1f;
        public float m_SecondsBetweenWave = 10;
        float _SecondsBetweenSpawn;
        int _NumberWave;
        bool spawn;

        public float m_RadiusSpawn = 10;

        // Use this for initialization
        public override AbstractEnemyManager Init(Transform playerPosition)
        {
            playerTransform = playerPosition;
            enemyDataList = Resources.LoadAll<SpawnData_SO>("MinionDatas/");
            chancesSpawn = new float[enemyDataList.Length + 1];           
            chancesSpawn[0] = 0;
            for (int i = 0; i < enemyDataList.Length; i++)
            {
                chancesSpawn[i + 1] = chancesSpawn[i] + enemyDataList[i].SpawnChance;
            }

            _SecondsBetweenSpawn = m_MaxSecondsBetweenSpawn;
            _NumberWave = 0;

            gameObject.name = "EnemyManager";
            return this;
        }

        public override void EnableSpawn(bool value)
        {
            spawn = value;
            StartCoroutine(RepeatSpawn());
            StartCoroutine(RepeatInscreaseDifficulty());
        }

        void Spawn()
        {
            GameObject spawnEnemyPrefab = GetRandomEnemy();

            var randSpawn = Random.value;
            var x = playerTransform.position.x + m_RadiusSpawn * Mathf.Cos(randSpawn * Mathf.PI * 2);
            var y = playerTransform.position.z + m_RadiusSpawn * Mathf.Sin(randSpawn * Mathf.PI * 2);

            var spawnEnemy = Instantiate(spawnEnemyPrefab, new Vector3(x, 0, y), new Quaternion());

            spawnEnemy.GetComponent<EnemyController>().SetTarget(playerTransform.position); ;
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