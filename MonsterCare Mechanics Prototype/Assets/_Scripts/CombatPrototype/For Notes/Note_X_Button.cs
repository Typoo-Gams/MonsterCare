using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note_X_Button : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        var note = GameObject.FindGameObjectWithTag("Note");
        Destroy(note);
    }
}
