using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesReports_Button : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Button>().onClick.AddListener(OpenNotes);
        //gameObject.GetComponent<Button>().onClick.AddListener(OpenReports);
    }

    public void OpenNotes()
    {
        FindObjectOfType<SoundManager>().play("ButtonClick");
    }

    public void OpenReports()
    {
        FindObjectOfType<SoundManager>().play("ButtonClick");
    }
}
