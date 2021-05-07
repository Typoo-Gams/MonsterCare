using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNotes : MonoBehaviour
{
    public int NoteNumber;
    GameSaver Saver = new GameSaver();

    // Start is called before the first frame update
    void Start()
    {
        Saver.SaveNote(NoteNumber, 1);
    }
}
