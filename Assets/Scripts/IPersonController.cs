using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPersonController  {

    void Death();

    Health GetHealth();

    LevelController GetLevel();
}
