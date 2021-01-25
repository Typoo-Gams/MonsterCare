using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private Button ThisButton;
    public int SceneNumber;
    public string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        ThisButton = gameObject.GetComponent<Button>();
        ThisButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    private void TaskOnClick() 
    {
        if (SceneName == null)
        {
            SceneManager.LoadScene(SceneNumber);
        }
        else 
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
