using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    public static UnityAction<int> ShowLevelUP = null;

    public Text m_ScoreText;
    private int scoreTotal = 0;
    public Text m_PlayerLevelText;
    public GameObject m_LvlUpText;
    private string _ScoreLabel = "Game.UI.ScoreLabel";
    private string _LevelLabel = "Game.UI.LevelLabel";
    
    private void Start ()
    {
        GameController.OnChangeScore += UpdateScore;
        ShowLevelUP += UpdateLevel;
    }

    private void OnDestroy()
    {
        GameController.OnChangeScore -= UpdateScore;
        ShowLevelUP -= UpdateLevel;
    }

    private void UpdateScore(int score)
    {
        scoreTotal += score;
        m_ScoreText.text = LocalizationManager.instance.GetLocalizedValue(_ScoreLabel) + scoreTotal;
       
    }

    private void UpdateLevel(int level)
    {
        m_PlayerLevelText.text = LocalizationManager.instance.GetLocalizedValue(_LevelLabel) + level;
        StartCoroutine(LevelUpParticle());
    }

    private IEnumerator LevelUpParticle()
    {        
        m_LvlUpText.SetActive(true);
        yield return new WaitForSeconds(3);
        m_LvlUpText.SetActive(false);        
    }
}
