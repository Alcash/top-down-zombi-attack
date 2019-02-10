using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    public Text m_ScoreText;
    public Text m_PlayerLevelText;    
    string _ScoreLabel = "Game.UI.ScoreLabel";    
    string _LevelLabel = "Game.UI.LevelLabel";

    // Use this for initialization
    void Start () {
        UpdateScore();

    }	
	
    internal void UpdateScore()
    {
        m_ScoreText.text = LocalizationManager.instance.GetLocalizedValue(_ScoreLabel) + GameController.singleton.m_Score;
        m_PlayerLevelText.text = LocalizationManager.instance.GetLocalizedValue(_LevelLabel) + GameController.singleton.playerLevel.Level;
    }
}
