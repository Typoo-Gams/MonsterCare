using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private Button ThisButton;
    public int SceneNumber = 0;
    public string SceneName = "";

    //Testing Fading between scenes
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        ThisButton = gameObject.GetComponent<Button>();
        ThisButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    private void TaskOnClick() 
    {
        if (SceneName == "")
        {
           
            SceneManager.LoadScene(SceneNumber);
            animator.SetTrigger("FadeToLevel");
        }
        else 
        {
           
            SceneManager.LoadScene(SceneName);
            animator.SetTrigger("FadeToLevel");
        }
    }
                //Fade to level Test
    public void FadeToLevel(int levelindex)
    {
        animator.SetTrigger("FadeOut");
    }
}
