using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    GameManager manager;
    

    public Text text;
    float count;
    public bool isLoaded;

    private void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (isLoaded == true)
        {
            text.gameObject.SetActive(true);

            if (count > 2)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //If there is no spesificed scene name to load then load the scene with index scene name.
                    if (manager.sceneName == "")
                    {
                        SceneManager.LoadScene(manager.sceneNumber);
                        manager.sceneNumber = 0;
                    }
                    else
                    {
                        SceneManager.LoadScene(manager.sceneName);
                        manager.sceneName = "";
                    }
                }
            }
        }
        
    }

}