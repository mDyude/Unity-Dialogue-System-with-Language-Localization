using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class NodeParser : MonoBehaviour
{
    public DialogueGraph graph;
    Coroutine _parser;
    // public Text speaker;
    public TextMeshProUGUI dialogue;
    public Image image;
    bool initialized = false;
    public Button languageSwitchButton;
    public string currentLanguage = "en";
    public GameObject selection1;
    public GameObject selection2;
    public GameObject selection3;
    public GameObject selection4;
    public GameObject selection5;
    int decisionCount;
    GameObject[] selections;
    LocalizedString[] decisionLocStrings;
    int maxResponses = 5;
    bool dialogueEneded = false;

    // Start is called before the first frame update
    void Start()
    {
        languageSwitchButton.onClick.AddListener(() =>
        {
            ToggleLanguage();
        });

        selections = new GameObject[] { selection1, selection2, selection3, selection4, selection5 };
        selection1.GetComponent<Button>().onClick.AddListener(() =>
        {
            NextNode("decision1");
        });
        selection2.GetComponent<Button>().onClick.AddListener(() =>
        {
            NextNode("decision2");
        });
        selection3.GetComponent<Button>().onClick.AddListener(() =>
        {
            NextNode("decision3");
        });
        selection4.GetComponent<Button>().onClick.AddListener(() =>
        {
            NextNode("decision4");
        });
        selection5.GetComponent<Button>().onClick.AddListener(() =>
        {
            NextNode("decision5");
        });
        Debug.Log("selection: " + selections[0]);
    }

    void Update()
    {
        if (!initialized)
        {
            if (Localization.isInit)
            {
                foreach (BaseNode b in graph.nodes)
                {
                    Debug.Log("NP: get localized string for b" + b.GetLocalizedString());
                    Debug.Log("NP: get localized string value for b" + b.GetLocalizedString().Value);

                    if (b.GetLocalizedString().key == "start")
                    {
                        Debug.Log("NP: Start Node Found");
                        graph.currentNode = b;
                        break;
                    }
                }
                initialized = true;
                _parser = StartCoroutine(ParseNode());
            }
        }
    }

    IEnumerator ParseNode()
    {
        BaseNode b = graph.currentNode;
        LocalizedString data = b.GetLocalizedString();
        Debug.Log("NP: data: " + data);
        Debug.Log("NP: data key: " + data.key);

        if (b.GetSprite() != null)
        {
            image.sprite = b.GetSprite();
        }

        CheckForDecisions(b);

        if (data.key == "start")
        {
            NextNode("exit");
        }

        if (data.key.Length >= 9)
        {
            if (data.key[..9] == "dialogue_")
            {
                // speaker.text = dataParts[1].Value;
                dialogue.text = data.Value;
                Debug.Log("NP: dialogue node text: " + dialogue.text);

                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.N));
                if (decisionCount == 0)
                {
                    NextNode("exit");
                }
            }
        }
        else
        {
            Debug.Log("NP: data key length is less than 9 " + data.key);
        }
    }

    public void NextNode(string fieldName)
    {
        Debug.Log("NP NextNode fieldName: " + fieldName);
        if (_parser != null)
        {
            Debug.Log("NP NextNode: _parser != null");
            StopCoroutine(_parser);
            _parser = null;
        }

        foreach (NodePort p in graph.currentNode.Ports)
        {
            if (p.fieldName == fieldName)
            {
                Debug.Log("NP NextNode p.fieldName: " + p.fieldName);
                if (p.Connection != null)
                {
                    graph.currentNode = p.Connection.node as BaseNode;
                }
                else
                {
                    dialogueEneded = true;
                }
                break;
            }
        }

        _parser = StartCoroutine(ParseNode());
    }

    void ToggleLanguage()
    {
        currentLanguage = currentLanguage == "en" ? "zh-s" : "en";
        Localization.SetLanguage(currentLanguage);

        UpdateLines();
        UpdateSelectionsText();
    }

    void UpdateLines()
    {
        dialogue.text = graph.currentNode.GetLocalizedString().Value;
    }

    void UpdateSelectionsText()
    {
        if (dialogueEneded)
        {
            selection3.GetComponentInChildren<TextMeshProUGUI>().text = "Dialogue Ended";
        }
        else if (decisionCount == 0)
        {
            selection3.GetComponentInChildren<TextMeshProUGUI>().text = "Press N to continue";
        }
        else
        {
            for (int i = 0; i < decisionCount; i++)
            {
                selections[i].GetComponentInChildren<TextMeshProUGUI>().text = graph.currentNode.GetResponseStrings(i + 1).Value;
            }
        }
    }

    void CheckForDecisions(BaseNode node)
    {
        foreach (GameObject s in selections)
        {
            s.SetActive(false);
        }

        decisionCount = 0;

        for (int i = 0; i < maxResponses; i++)
        {
            if (node.HasResponse(i + 1))
            {
                Debug.Log("NP: decision found: " + i);
                selections[i].SetActive(true);
                // selections[i].GetComponent<Button>().onClick.AddListener(() =>
                // {
                //     NextNode("decision" + i);
                // });
                decisionCount++;
            }
        }

        Debug.Log("NP: decisionCount: " + decisionCount);

        if (decisionCount > 0)
        {
            UpdateSelectionsText();
        }
    }
}
