using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UnityEngine;

public class CSVLoader
{
    private TextAsset csvFile;
    private char lineSeperator = '\n';
    private char surround = '"';
    char[] fieldSeperator = new char[] { ',' };
    // Start is called before the first frame update

    public void LoadCSV()
    {
        csvFile = Resources.Load<TextAsset>("card_local");
        // Debug.Log("CSV loaded");
    }

    public Dictionary<string, string> GetDictValues(string attributeId)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        string[] lines = csvFile.text.Split(lineSeperator);
        int attributeIndex = -1;
        // the header line
        string[] headers = lines[0].Split(fieldSeperator, System.StringSplitOptions.None);
        // Debug.Log("Headers len: " + headers.Length);
        for (int i = 0; i < headers.Length; i++)
        {
            headers[i] = headers[i].Trim('\"');

            // Debug.Log("Headers: " + i + " " + headers[i]);
            if (headers[i].Contains(attributeId))
            {
                // Debug.Log("Attribute ID: " + attributeId + " i: " + i);
                attributeIndex = i;
                break;
            }
        }

        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = CSVParser.Split(line);

            for (int j = 0; j < fields.Length; j++)
            {
                fields[j] = fields[j].TrimStart(' ', surround);
                fields[j] = fields[j].TrimEnd(surround);
                // Debug.Log("Fields: " + j + " " + fields[j]);
            }

            if (fields.Length > attributeIndex)
            {
                var key = fields[0];
                if (dict.ContainsKey(key))
                {
                    continue;
                }

                var value = fields[attributeIndex];

                dict.Add(key, value);

            }
            // Debug.Log("key: " + fields[0] + " value: " + fields[attributeIndex]);
        }
        return dict;
    }


#if UNITY_EDITOR
    // attributeId: en, zh-s
    public void Add(string key, string value, string attributeId)
    {
        // Append a new line to the file
        // string appended = string.Format("\n\"{0}\", \"{1}\", \"\"", key, value);
        // System.IO.File.AppendAllText("Assets/Resources/card_local.csv", appended);

        if (attributeId == "en")
        {
            string appended = string.Format("\n\"{0}\", \"{1}\", \"\"", key, value);
            System.IO.File.AppendAllText("Assets/Resources/card_local.csv", appended);
        }
        else if (attributeId == "zh-s")
        {
            string appended = string.Format("\n\"{0}\", \"\", \"{1}\"", key, value);
            System.IO.File.AppendAllText("Assets/Resources/card_local.csv", appended);
        }
        

        // Refresh the editor to show the changes
        UnityEditor.AssetDatabase.Refresh();
    }

    public void Add(string key, string value)
    {
        // Append a new line to the file
        string appended = string.Format("\n\"{0}\", \"{1}\", \"\"", key, value);
        System.IO.File.AppendAllText("Assets/Resources/card_local.csv", appended);

        // Refresh the editor to show the changes
        UnityEditor.AssetDatabase.Refresh();
    }

    public void Remove(string key)
    {
        string[] lines = csvFile.text.Split(lineSeperator);
        string[] keys = new string[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            // string[] fields = line.Split(fieldSeperator, System.StringSplitOptions.None);
            keys[i] = line.Split(fieldSeperator, System.StringSplitOptions.None)[0];
        }

        int index = -1;

        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i].Contains(key))
            {
                index = i;
                break;
            }
        }

        if (index > -1)
        {
            // new array with one less element
            string[] newLines = lines.Where(w => w != lines[index]).ToArray();

            string replaced = string.Join(lineSeperator.ToString(), newLines);
            File.WriteAllText("Assets/Resources/card_local.csv", replaced);

        }
    }

    public void Edit(string key, string value, string attributeId)
    {
        Remove(key);
        string temp;
        if (attributeId == "en")
        {
            temp = GetDictValues("zh-s")[key];
            string appended = string.Format("\n\"{0}\", \"{1}\", \"{2}\"", key, value, temp);
            System.IO.File.AppendAllText("Assets/Resources/card_local.csv", appended);
        }
        else
        {
            temp = GetDictValues("en")[key];
            string appended = string.Format("\n\"{0}\", \"{1}\", \"{2}\"", key, temp, value);
            System.IO.File.AppendAllText("Assets/Resources/card_local.csv", appended);
        }

    }

#endif

}
