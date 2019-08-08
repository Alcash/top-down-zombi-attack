using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{   
    [SerializeField]
    private GameObject CanvasGame;

    public static UnityAction<string> OpenWindow = null;

    private BaseUIController[] uiControllers;

    // Start is called before the first frame update
    void Awake()
    {
        OpenWindow += ShowWindow;
             
        uiControllers = CanvasGame.GetComponentsInChildren<BaseUIController>();
    }
  

    public void ShowWindow(string baseUIController)
    {        
        foreach (var item in uiControllers)
        {            
            if(baseUIController == item.GetType().ToString())
            { 
                item.Open(true);
            }
        }       
    }
}
