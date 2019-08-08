using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MainMenuUIController : BaseUIController {

    public Text m_LabelMaxScore;
    public string m_MaxScoreLabel = "Максимальный результат ";

    protected override void Init()
    {
        base.Init();
        var _maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        m_LabelMaxScore.text = m_MaxScoreLabel + _maxScore;
    }

    protected override void Open()
    {
        base.Open();
        Debug.Log(name + " open");
    }
    public void StartGame()
    {
        GameController.singleton.StartGame();
        Close();
    }
    public void Exit()
    {
        Application.Quit();
    }
}
