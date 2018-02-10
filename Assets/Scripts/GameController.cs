using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController singleton;
    public GameObject m_PlayerGameObject;
    public GameObject m_GameoverPanel;
    public Text m_ScoreText;
   

    [SerializeField]
    string _ScoreLabel = "Очки ";
    

    public int m_Score = 0;
	// Use this for initialization
	void Start () {

        if(singleton == null )
            singleton = this;

        Time.timeScale = 1;
        m_GameoverPanel.SetActive(false);
    }
	
    public Vector3 PlayerPosition
    {
       get {
            Debug.Log("m_PlayerGameObject.transform.position" + m_PlayerGameObject.transform.position);
            return m_PlayerGameObject.transform.position;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void Gameover()
    {
        Time.timeScale = 0;
        m_GameoverPanel.SetActive(true);

    }

    public void AddScore(int score)
    {
        m_Score += score;
        m_ScoreText.text = _ScoreLabel + m_Score.ToString();
    }
}
