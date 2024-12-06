using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Drawing;
using System;
using UnityEngine.SocialPlatforms;
using JetBrains.Annotations;
using UnityEngine.UIElements;

public class TextLocalizerEditWindow : EditorWindow
{
    public static Dictionary<int, string> languageIndex = new Dictionary<int, string>
    {
        { 0, "en" },
        { 1, "zh-s" }
    };

    public static int selectedLanguage;
    private string[] languages = new string[] { "English", "Simplified Chinese" };

    [MenuItem("Window/Text Localizer/Edit")]
    public static void Open()
    {
        TextLocalizerEditWindow window = new TextLocalizerEditWindow();
        // TextLocalizerEditWindow window = ScriptableObject.CreateInstance<TextLocalizerEditWindow>();
        window.titleContent = new GUIContent("Text Localizer");
        window.ShowUtility();
        // window.key = key;
        window.key = "dafault";
    }

    public string key;
    public string value;
    public string addLanguage;

    public void OnGUI()
    {
        selectedLanguage = EditorGUILayout.Popup(selectedLanguage, languages);
        addLanguage = languageIndex[selectedLanguage];

        key = EditorGUILayout.TextField("Key: ", key);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Value: ", GUILayout.MaxHeight(50));
        EditorStyles.textArea.wordWrap = true;

        value = EditorGUILayout.TextArea(value, EditorStyles.textArea, GUILayout.Height(100), GUILayout.Width(450));

        if (GUILayout.Button("Add"))
        {
            Debug.Log("ADD Key: " + key + " Value: " + value + " in Language: " + addLanguage);
            if (Localization.GetLocalizedValue(key) != string.Empty)
            {
                Localization.Replace(key, value, addLanguage);
            }
            else
            {
                Localization.Add(key, value);
            }
        }

        minSize = new Vector2(1000, 400);
        maxSize = minSize;

    }

}


public class TextLocalizerSearchWindow : EditorWindow
{
    public static Dictionary<int, string> languageIndex = new Dictionary<int, string>
    {
        { 0, "en" },
        { 1, "zh-s" }
    };

    public static int selectedLanguage;

    [MenuItem("Window/Text Localizer/Search")]
    public static void Open()
    {
        TextLocalizerSearchWindow window = new TextLocalizerSearchWindow();
        // TextLocalizerEditWindow window = ScriptableObject.CreateInstance<TextLocalizerEditWindow>();

        window.titleContent = new GUIContent("Text Localizer Search");

        Rect screenRect = new Rect(Screen.width / 2f, Screen.height / 2f, 10, 10);

        Vector2 windowSize = new Vector2(500, 300);

        window.ShowAsDropDown(screenRect, windowSize);
    }

    public string value;
    public string searchLanguage;
    public Vector2 scrollPos;
    public Dictionary<string, string> dict;
    private string[] languages = new string[] { "English", "Simplified Chinese" };


    private void OnEnable()
    {
        dict = Localization.GetDictForEditor("en");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("Search: ", EditorStyles.boldLabel);
        value = EditorGUILayout.TextField(value);

        selectedLanguage = EditorGUILayout.Popup(selectedLanguage, languages);
        searchLanguage = languageIndex[selectedLanguage];

        // Debug.Log("Selected Language: " + searchLanguage);
        EditorGUILayout.EndHorizontal();

        GetSearchResults();
    }

    private void GetSearchResults()
    {
        if (value == null)
        {
            return;
        }
        dict = Localization.GetDictForEditor(searchLanguage);
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        foreach (KeyValuePair<string, string> element in dict)
        {
            if (element.Key.ToLower().Contains(value.ToLower()) || element.Value.ToLower().Contains(value.ToLower()))
            {
                EditorGUILayout.BeginHorizontal("box");
                Texture closeIcon = (Texture)Resources.Load("close");

                GUIContent content = new GUIContent(closeIcon);
                GUIStyle GUIstyle = new GUIStyle();

                if (GUILayout.Button(content, GUIstyle, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
                {
                    if (EditorUtility.DisplayDialog("Remove Key" + element.Key + "?", "Are you sure you want to delete this key?", "Yes"))
                    {
                        Localization.Remove(element.Key);
                        AssetDatabase.Refresh();
                        Localization.Init();
                        dict = Localization.GetDictForEditor(searchLanguage);
                    }
                }

                EditorGUILayout.TextField(element.Key);
                EditorGUILayout.TextField(element.Value);
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}