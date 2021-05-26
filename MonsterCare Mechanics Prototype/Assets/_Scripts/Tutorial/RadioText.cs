using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioText : MonoBehaviour
{
    string[] Text =
        {
        "Hello there this is Dr.Steele. I’m here to help you get started.",
        "Important information can be found in the notes tab.",
        "Also, My journal during my stay was stolen by monsters.",
        "Please retrieve them when you're out hunting.",
        "They will show in the notes and reports tab when you find them."
    };
    char[] _chars;
    int _CharIndex, _textIndex = 0;
    float counter;
    public bool skip;

    Text radioText;
    GameObject HideRadio;

    GameSaver Saver = new GameSaver();
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        manager.HideUI = true;
        radioText = GetComponentInChildren<Text>();
        radioText.text = "";
        _chars = Text[0].ToCharArray();
        foreach (GameObject found in GameObject.FindGameObjectsWithTag("Tutorial"))
        {
            if (found.name.Equals("radio"))
                HideRadio = found;
        }
        
        HideRadio.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //adds characters to the text if there is still more to add
        if (_CharIndex < _chars.Length)
        {
            if (Input.GetMouseButtonDown(0))
                skip = true;
            //delay
            counter += Time.deltaTime;
            if (counter > 0.075f)
            {
                radioText.text += _chars[_CharIndex];
                _CharIndex++;
                counter = 0;
            }
            if (skip)
            {
                radioText.text = Text[_textIndex];
                _CharIndex = _chars.Length;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                _textIndex++;
                if (_textIndex == Text.Length)
                {
                    HideRadio.SetActive(true);
                    Saver.IsTutorialDone(2);
                    foreach (GameObject found in GameObject.FindGameObjectsWithTag("Tutorial"))
                    {
                        if (found.name.Equals("NotesButton"))
                        {
                            found.GetComponent<Button>().interactable = true;
                        }
                    }
                    Destroy(gameObject);
                    //manager.HideUI = false;
                }
                else
                {
                    _chars = Text[_textIndex].ToCharArray();
                    _CharIndex = 0;
                    radioText.text = "";
                    skip = false;
                }
            }
        }
    }
}
