using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteAdded_Text : MonoBehaviour
{
    public GameObject[] notes;

    public GameObject addedNote;
    private float timeAppear = 2f;
    private float timeDisappear;

    private void Start()
    {
        addedNote.SetActive(true);
        timeDisappear = Time.time + timeAppear;
    }

    //checks every frame if the timer has expired and the text should then disappear
    void Update()
    {
        
    }
}
