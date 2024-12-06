using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomPropertyDrawer(typeof(LocalizedString))]
public class LocalizedStringDrawer : PropertyDrawer
{
    bool dropdown;
    float height;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (dropdown)
        {
            return height + 20;
        }
        else
        {
            return 20;
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        position.width -= 34;
        position.height = 16;

        Rect valueRect = new Rect(position);
        valueRect.x += 14;
        valueRect.width -= 14;

        Rect foldButtonRect = new Rect(position);
        foldButtonRect.width = 14;

        dropdown = EditorGUI.Foldout(foldButtonRect, dropdown, "");

        position.x += 14;
        position.width -= 14;

        SerializedProperty key = property.FindPropertyRelative("key");
        key.stringValue = EditorGUI.TextField(position, key.stringValue);

        position.x += position.width + 2;
        position.width = 15;
        position.height = 15;

        Texture searchIcon = (Texture)Resources.Load("search");
        GUIContent searchContent = new GUIContent(searchIcon);
        GUIStyle GUIstyle = new GUIStyle();

        if (GUI.Button(position, searchContent, GUIstyle))
        {
            TextLocalizerSearchWindow.Open();
        }

        position.x += position.width + 2;

        Texture saveIcon = (Texture)Resources.Load("save");
        GUIContent storeContent = new GUIContent(saveIcon);

        if (GUI.Button(position, storeContent, GUIstyle))
        {
            // TextLocalizerEditWindow.Open(key.stringValue);
            TextLocalizerEditWindow.Open();

        }

        if (dropdown)
        {
            var value = Localization.GetLocalizedValue(key.stringValue);
            GUIStyle style = GUI.skin.box;
            height = style.CalcHeight(new GUIContent(value), valueRect.width);

            valueRect.height = height;
            valueRect.y += 20;
            EditorGUI.LabelField(valueRect, value, EditorStyles.wordWrappedLabel);
        }

        EditorGUI.EndProperty();


    }
}