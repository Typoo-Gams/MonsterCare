using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger_Button : MonoBehaviour
{


    private Button ThisButton;
    public int SceneNumber = 0;
    public string SceneName = "";

    // Start is called before the first frame update
    void Start()
    {
        //add the TaskOnClick function to the button this script is attached to.
        ThisButton = gameObject.GetComponent<Button>();
        ThisButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    private void TaskOnClick()
    {
        FindObjectOfType<SoundManager>().play("ButtonClick");
        //If there is no spesificed scene name to load then load the scene with index scene name.
        if (SceneName == "")
        {
            SceneManager.LoadScene(SceneNumber);
        }
        else
        {

            SceneManager.LoadScene(SceneName);
        }

    }
}
