using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct LocalizedString
{
    public string key;

    public LocalizedString(string key)
    {
        this.key = key;
    }

    public string Value
    {
        get
        {
            return Localization.GetLocalizedValue(key);
        }
    }

    public static implicit operator LocalizedString(string key)
    {
        return new LocalizedString(key);
    } 


}
