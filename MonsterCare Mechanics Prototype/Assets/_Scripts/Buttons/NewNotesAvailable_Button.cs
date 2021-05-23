using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewNotesAvailable_Button : MonoBehaviour
{
    Animator NewNote;
    GameSaver Saver = new GameSaver();

    // Start is called before the first frame update
    void Start()
    {
        NewNote = GetComponent<Animator>();
        for (int i = 1; i <= Saver.NumberOfNotes ; i++)
        {
            //inconsistant methods i know... its just like this. cant be bothered to fix it.
            if (!Saver.GetSeenNote(i) && Saver.LoadNote(i) == 1)
            {
                NewNote.SetBool("NewNote", true);
                break;
            }
        }
    }
}
