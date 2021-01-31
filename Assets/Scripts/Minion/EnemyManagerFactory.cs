using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade
{
    /// <summary>
    /// Фабрика менеджеров противников
    /// </summary>
    public class EnemyManagerFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform playerPosition;

        private List<AbstractEnemyManager> factories = new List<AbstractEnemyManager>();
            
        private void Start()
        {
            GameObject factory = new GameObject();

            AbstractEnemyManager enemyFactory = factory.AddComponent<EnemyManager>();
            enemyFactory.Init(playerPosition);

            factories.Add(enemyFactory);            
        }

        /// <summary>
        /// Включить или выключить спавнеры
        /// </summary>
        /// <param name="balue"></param>
        public void EnableProcess(bool balue)
        {
            factories.ForEach(x => x.EnableSpawn(balue));
        }

    }
}
