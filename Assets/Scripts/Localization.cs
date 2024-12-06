using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localization
{
    public enum Language
    {
        English,
        Simplified_Chinese
    }

    public static Language currentLanguage = Language.English;

    private static Dictionary<string, string> localizedEN;
    private static Dictionary<string, string> localizedZHS;

    public static bool isInit;

    public static CSVLoader csvLoader;


    public static void Init()
    {
        csvLoader = new CSVLoader();
        csvLoader.LoadCSV();
        Debug.Log("CSV loaded");
        UpdateDict();

        isInit = true;
    }

    public static void UpdateDict()
    {
        localizedEN = csvLoader.GetDictValues("en");
        localizedZHS = csvLoader.GetDictValues("zh-s");
    }

    public static Dictionary<string, string> GetDictForEditor(string attributeId)
    {
        if (!isInit)
        {
            Init();
        }

        if (attributeId == "en")
        {
            return localizedEN;
        }
        else if (attributeId == "zh-s")
        {
            return localizedZHS;
        }

        return null;

    }

    public static string GetLocalizedValue(string key)
    {
        if (!isInit)
        {
            Init();
        }
        Debug.Log("GetLocalizedValue key: " + key);

        string value = key;
        bool keyFound = false;

        switch (currentLanguage)
        {
            case Language.English:
                keyFound = localizedEN.TryGetValue(key, out value);
                break;
            case Language.Simplified_Chinese:
                keyFound = localizedZHS.TryGetValue(key, out value);
                break;
        }

        if (!keyFound)
        {
            Debug.LogWarning($"Key '{key}' not found in {currentLanguage} dictionary.");
            return string.Empty;
        }

        return value;
    }

    public static void Add(string key, string value)
    {
        if (value.Contains("\""))
        {
            value.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Add(key, value);
        csvLoader.LoadCSV();

        UpdateDict();
    }

    public static void Add(string key, string value, string attributeId)
    {
        if (value.Contains("\""))
        {
            value.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Add(key, value, attributeId);
        csvLoader.LoadCSV();

        UpdateDict();
    }


    public static void Replace(string key, string value, string attributeId)
    {
        if (value.Contains("\""))
        {
            value.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Edit(key, value, attributeId);
        csvLoader.LoadCSV();

        UpdateDict();
    }

    public static void Remove(string key)
    {
        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Remove(key);
        csvLoader.LoadCSV();

        UpdateDict();
    }

    public static void SetLanguage(string language)
    {
        if (language == "en")
        {
            currentLanguage = Language.English;
        }
        else if (language == "zh-s")
        {
            currentLanguage = Language.Simplified_Chinese;
        }
    }

    public static Language GetLanguage()
    {
        return currentLanguage;
    }
}
