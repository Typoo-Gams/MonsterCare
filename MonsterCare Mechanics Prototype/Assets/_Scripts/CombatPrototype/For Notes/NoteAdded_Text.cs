using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteAdded_Text : MonoBehaviour
{
    public GameObject[] notes;

    public Text addedText;
    private float timeAppear = 2f;
    private float timeDisappear;

    private  void EnableText()
    {
        addedText.enabled = true;
        timeDisappear = Time.time + timeAppear;
    }

    //checks every frame if the timer has expired and the text should then disappear
    void Update()
    {
        if (addedText.enabled && (Time.time >= timeDisappear))
        {
            addedText.enabled = false;
        }
    }
}
