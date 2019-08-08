using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public Text m_LabelMaxScore;
    public string m_MaxScoreLabel = "Максимальный результат ";

	// Use this for initialization
	void Start () {

        var _maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        m_LabelMaxScore.text = m_MaxScoreLabel + _maxScore;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
