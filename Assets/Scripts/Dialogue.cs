// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// using System;
// using System.Linq;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;

// public class Dialogue : MonoBehaviour
// {
//     public TextMeshProUGUI textDisplay;
//     // private LocalizedString[] keys;
//     private float typingSpeed = 0.02f;
//     private int index;
//     public string currentLanguage = "en";
//     // public Button languageSwitchButton;
//     private bool buttonClicked = false;

//     void Start()
//     {
//         // languageSwitchButton.onClick.AddListener(() =>
//         // {
//         //     buttonClicked = true;
//         //     ToggleLanguage();
//         // });

//         // initialize the keys array with the keys from the dictionary
//         // keys = Localization.GetDictForEditor("en").Keys.Select(key => new LocalizedString(key)).ToArray();

//         // lines = lines_en.Values.ToArray();
//         textDisplay.text = string.Empty;
//         // UpdateLines();
//         // StartDialogue();
//     }

//     void Update()
//     {
//         // if (buttonClicked)
//         // {
//         //     Debug.Log("clicked Current Language before: " + currentLanguage);
//         //     Debug.Log("buttonClicked " + buttonClicked);
//         //     buttonClicked = false;
//         //     return;
//         // }

//         // if (Input.GetKeyDown(KeyCode.N) && !buttonClicked)
//         // {
//         //     Debug.Log("not clicled Current Language: " + currentLanguage);

//             // if (textDisplay.text == keys[index].Value)
//             // {
//             //     NextSentence();
//             // }

//             // else
//             // {
//             //     StopAllCoroutines();
//             //     textDisplay.text = keys[index].Value;
//             // }
//         // }
//     }

//     // void ToggleLanguage()
//     // {
//     //     currentLanguage = currentLanguage == "en" ? "zh-s" : "en";
//     //     Localization.SetLanguage(currentLanguage);

//     //     UpdateLines();
//     // }

//     void UpdateLines()
//     {
//         // textDisplay.text = keys[index].Value;
//     }

//     // void StartDialogue()
//     // {
//     //     index = 0;
//     //     StartCoroutine(Type());
//     // }

//     // IEnumerator Type()
//     // {
//     //     textDisplay.text = string.Empty;

//     //     foreach (char letter in textDisplay.text.ToCharArray())
//     //     {
//     //         textDisplay.text += letter;
//     //         yield return new WaitForSeconds(typingSpeed);
//     //     }
//     // }

//     void NextSentence()
//     {
//         // if (index < keys.Length - 1)
//         // {
//         //     index++;
//         //     textDisplay.text = string.Empty;
//         //     StartCoroutine(Type());
//         // }
//         // else
//         // {
//         //     gameObject.SetActive(false);
//         // }
//     }

// }
