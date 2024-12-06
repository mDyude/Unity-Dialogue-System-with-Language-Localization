using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

public class TextLocalizerUI : MonoBehaviour
{
    TextMeshProUGUI textField;
    // public string key;

    [SerializeField]

    public LocalizedString localizedString;

    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        // string value = Localization.GetLocalizedValue(key);
        // textField.text = value;
        textField.text = localizedString.Value;
    }
}
