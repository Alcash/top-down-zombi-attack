using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameoverUIController : BaseUIController {

    public Text m_GameoverText;
    public Text m_ResultText;
    

    public Button m_ExitButton;
    public Button m_RestartButton;

   
    string _GameoverLabel = "Game.UI.GameoverLabel";    
    string _ResultLabel = "Game.UI.ResultLabel";    
    string _BestResultLabel = "Game.UI.BestResultLabel";    
    string _NewBestResultLabel = "Game.UI.NewBestResultLabel";    
    string _PreBestResultLabel = "Game.UI.PreBestResultLabel";
    // Use this for initialization
    void Start () {
        m_RestartButton.onClick.AddListener(Restart);
        m_ExitButton.onClick.AddListener(Exit);

    }

    protected override void Open()
    {     
         m_GameoverText.text = LocalizationManager.instance.GetLocalizedValue(_GameoverLabel);

        var _BestResult = PlayerPrefs.GetInt("MaxScore", 0);
        if (_BestResult >= GameController.singleton.m_Score)
        {
            m_ResultText.text = LocalizationManager.instance.GetLocalizedValue(_ResultLabel) + GameController.singleton.m_Score.ToString();
            m_ResultText.text += "\n" + LocalizationManager.instance.GetLocalizedValue(_BestResultLabel) + _BestResult;
        }
        else
        {
            PlayerPrefs.SetInt("MaxScore", GameController.singleton.m_Score);
            m_ResultText.text = LocalizationManager.instance.GetLocalizedValue(_NewBestResultLabel) + GameController.singleton.m_Score.ToString();
            m_ResultText.text += "\n" + LocalizationManager.instance.GetLocalizedValue(_PreBestResultLabel) + _BestResult;
        }
    }

    private void OnEnable()
    {
       
        
    }

    void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
