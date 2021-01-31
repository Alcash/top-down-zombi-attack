using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade
{
    /// <summary>
    /// Абстрактный менеджер противников
    /// </summary>
    public abstract class AbstractEnemyManager : MonoBehaviour
    {
        /// <summary>
        /// Инициализация менеджера
        /// </summary>
        /// <param name="playerPosition"></param>
        /// <returns></returns>
        public abstract AbstractEnemyManager Init(Transform playerPosition);

        /// <summary>
        /// Переключение работы менеджера спавнера
        /// </summary>
        /// <param name="value"></param>
        public abstract void EnableSpawn(bool value);
    }
}
