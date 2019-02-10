using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour {
    public string key;
    Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        SetText();

        LocalizationManager.instance.OnNewLanguage += SetText;
    }

    void SetText()
    {

        if (LocalizationManager.instance == null)
        {
            Debug.LogError("LocalizationManager.instance == null");
        }
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
    }

    private void OnDestroy()
    {
        LocalizationManager.instance.OnNewLanguage -= SetText;
    }


}
