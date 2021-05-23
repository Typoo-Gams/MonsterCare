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
    Animator Fade;

    private void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        Fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();
        Fade.Play("FadeIn");
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (isLoaded == true)
        {
            if (count > 16)
            {
                text.gameObject.SetActive(true);
                //If there is no spesificed scene name to load then load the scene with index scene name.
                if (manager.sceneName == "")
                {
                    SceneManager.LoadScene(manager.sceneNumber);
                    manager.sceneNumber = 0;
                    Fade.Play("FadeIn");
                }
                else
                {
                    SceneManager.LoadScene(manager.sceneName);
                    manager.sceneName = "";
                    Fade.Play("FadeIn");
                }
            }
        }
    }
}
