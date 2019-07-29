using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class GameController : MonoBehaviour {

    public static UnityAction<int> OnChangeScore = null;
    public static GameController singleton;
    public GameObject m_PlayerGameObject;  

    public int m_Score = 0;  
	
	private void Start () {

        if(singleton == null )
            singleton = this;
       
        Time.timeScale = 1;
       
        StartCoroutine( LocalizationManager.instance.LoadLocalizedText());

        UIManager.OpenWindow(typeof(MainMenuUIController).ToString());
    }

    internal void StartGame()
    {
        throw new NotImplementedException();
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
        UIManager.OpenWindow(typeof(GameoverUIController).ToString());
        

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
