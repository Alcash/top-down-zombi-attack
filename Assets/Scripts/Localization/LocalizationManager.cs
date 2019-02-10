using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using System;

public class LocalizationManager : MonoBehaviour {

    public static LocalizationManager instance;
    public LanguageType languageType;

    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingTextString = "Localized key not found";

    private List<String> LanguageDictinary = new List<string>() { "", AppSettings.LocalizationEn, AppSettings.LocalizationRu };

    public UnityAction OnNewLanguage;
   

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }        
    }
    public IEnumerator LoadLocalizedText(string fileName = "")
    {


        if (languageType == LanguageType.NotSelect)
        {

            if (Application.systemLanguage == SystemLanguage.Russian)
            {

                PlayerPrefs.SetInt(AppSettings.LanguageSetting, (int)LanguageType.Russian);


            }
            //Otherwise, if the system is English, output the message in the console
            else//(Application.systemLanguage == SystemLanguage.English)
            {
                PlayerPrefs.SetInt(AppSettings.LanguageSetting, (int)LanguageType.English);
            }

        }

        else
        {
            PlayerPrefs.SetInt(AppSettings.LanguageSetting, (int)languageType);
            
        }

        fileName = GetLanguage();



        localizedText = new Dictionary<string, string>();
       
        TextAsset Text = UnityEngine.Resources.Load(fileName) as TextAsset;
        if(Text != null)
        {
            string dataAsJson = Text.text;
            
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries. Filename " + fileName);
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }

        if (instance == null)
        {
            Debug.LogError("instance == null");
        }

        isReady = true;

        if (OnNewLanguage != null)
        {
            Debug.LogError("OnNewLanguage");
            OnNewLanguage();
        }

        yield return null;
    }

    public string GetLanguage()
    {
        int indexLanguange = (int)PlayerPrefs.GetInt(AppSettings.LanguageSetting, 0);
        return LanguageDictinary[indexLanguange];
    }

    public LanguageType GetLanguageEnum()
    {
        return (LanguageType)PlayerPrefs.GetInt(AppSettings.LanguageSetting);
    }

    public void SetLanguage(LanguageType value)
    {
        PlayerPrefs.SetInt(AppSettings.LanguageSetting,(int)value);

        StartCoroutine(LoadLocalizedText());
    }

    public enum LanguageType
    {
        NotSelect,
        English,
        Russian
    }

    public string GetLocalizedValue(string key)
    {
        if(key == null || key == "")
        {
            key = missingTextString;
        }
        string result = key;

        if (localizedText == null)
        {
            Debug.LogError("localizedText not init");
        }
        else if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result;

    }

    public bool GetIsReady()
    {
        return isReady;
    }

    public static void SaveNewLocalizedText(string fileName, string Key, string value)
    {
        List<LocalizationItem>  localizedText = new List<LocalizationItem>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
       
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add( new LocalizationItem( loadedData.items[i].key, loadedData.items[i].value));
            }
           
            localizedText.Add(new LocalizationItem(Key, value));
            loadedData.items = localizedText.ToArray();
            Debug.Log(localizedText.ToArray());
            string jsonString = JsonUtility.ToJson(loadedData);
            File.WriteAllText(filePath, jsonString, System.Text.Encoding.UTF8);
            
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }        

       
    }

    
}
