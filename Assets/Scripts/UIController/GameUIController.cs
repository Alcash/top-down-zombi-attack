using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    public Text m_ScoreText;
    private int scoreTotal = 0;
    public Text m_PlayerLevelText;    
    string _ScoreLabel = "Game.UI.ScoreLabel";    
    string _LevelLabel = "Game.UI.LevelLabel";

    // Use this for initialization
    void Start ()
    {
        GameController.OnChangeScore += UpdateScore;
    }	
	
    private void UpdateScore(int score)
    {
        scoreTotal += score;
        m_ScoreText.text = LocalizationManager.instance.GetLocalizedValue(_ScoreLabel) + scoreTotal;
       
    }

    private void UpdateLevel()
    {
        m_PlayerLevelText.text = LocalizationManager.instance.GetLocalizedValue(_LevelLabel) + GameController.singleton.playerLevel.Level;
    }
}
