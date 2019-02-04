using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    public Text m_ScoreText;
    public Text m_PlayerLevelText;
    [SerializeField]
    string _ScoreLabel = "Очки ";
    [SerializeField]
    string _LevelLabel = "Уровень ";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void UpdateScore()
    {
        m_ScoreText.text = _ScoreLabel + GameController.singleton.m_Score;
        m_PlayerLevelText.text = _LevelLabel + GameController.singleton.playerLevel.Level;
    }
}
