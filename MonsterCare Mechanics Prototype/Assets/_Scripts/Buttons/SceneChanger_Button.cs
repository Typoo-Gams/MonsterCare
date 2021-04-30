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
    public bool LoadScene;
    GameManager manager;
    Animator Fade;
    float cnt;
    [HideInInspector]
    public bool clicked;

    // Start is called before the first frame update
    void Start()
    {
        Fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        //add the TaskOnClick function to the button this script is attached to.
        ThisButton = gameObject.GetComponent<Button>();
        ThisButton.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        if (clicked) 
        {
            
            if (!LoadScene)
            {
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
            else
            {
                //whait for the fade to change scene
                cnt += Time.deltaTime;
                if (cnt > Fade.GetCurrentAnimatorStateInfo(0).length) 
                {
                    //If there is no spesificed scene name to load then load the scene with index scene name.
                    if (SceneName == "")
                    {
                        SceneManager.LoadScene(9);
                        manager.sceneNumber = SceneNumber;
                    }
                    else
                    {

                        SceneManager.LoadScene("LoadScene");
                        manager.sceneName = SceneName;
                    }
                }
            }
        }
    }
    // Update is called once per frame
    private void TaskOnClick()
    {
        clicked = true;
        if(LoadScene)
            Fade.Play("FadeOut");
        FindObjectOfType<SoundManager>().play("ButtonClick");

    }
}
