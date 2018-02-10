using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PanelGameoverController : MonoBehaviour {

    public Text m_GameoverText;
    public Text m_ResultText;
    

    public Button m_ExitButton;
    public Button m_RestartButton;

    [SerializeField]
    string _GameoverLabel = "Ты умер, ахаха! ";
    [SerializeField]
    string _ResultLabel = "ты смог набрать очков: ";

    [SerializeField]
    string _BestResultLabel = "Лучший результат: ";

    [SerializeField]
    string _NewBestResultLabel = "Ты стал лучше чем прежде, твой результат: ";

    [SerializeField]
    string _PreBestResultLabel = "Предыдущий результат: ";
    // Use this for initialization
    void Start () {
        m_RestartButton.onClick.AddListener(Restart);
        m_ExitButton.onClick.AddListener(Exit);

    }
		

    private void OnEnable()
    {
        m_GameoverText.text = _GameoverLabel;

        var _BestResult = PlayerPrefs.GetInt("MaxScore", 0);
        if (_BestResult >= GameController.singleton.m_Score)
        {
            m_ResultText.text = _ResultLabel + GameController.singleton.m_Score.ToString();
            m_ResultText.text += "\n" + _BestResultLabel + _BestResult;
        }
        else
        {
            PlayerPrefs.SetInt("MaxScore", GameController.singleton.m_Score);
            m_ResultText.text = _NewBestResultLabel + GameController.singleton.m_Score.ToString();
            m_ResultText.text += "\n" + _PreBestResultLabel + _BestResult;
        }
        
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
