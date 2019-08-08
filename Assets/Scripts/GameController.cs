using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameController : MonoBehaviour {

    public static UnityAction<int> OnChangeScore = null;
    public static GameController singleton;
    public GameObject m_PlayerGameObject;

    [HideInInspector]
    public LevelController playerLevel;

    public GameObject m_GameoverPanel;  
    
    public int m_Score = 0;

    public  GameUIController gameUIController;
	
	private void Start () {

        if(singleton == null )
            singleton = this;
        playerLevel = m_PlayerGameObject.GetComponent<LevelController>();
        Time.timeScale = 1;
        m_GameoverPanel.SetActive(false);
        StartCoroutine( LocalizationManager.instance.LoadLocalizedText());
    }
	
    public Vector3 PlayerPosition
    {
       get {
            Debug.Log("m_PlayerGameObject.transform.position" + m_PlayerGameObject.transform.position);
            return m_PlayerGameObject.transform.position;
        }
    }	

    public void Gameover()
    {
        Time.timeScale = 0;
        m_GameoverPanel.SetActive(true);
    }

    public void AddScore(int score)
    {
        m_Score += score;
        if (OnChangeScore != null)
        {
            OnChangeScore(score);
        }   
    }
}
