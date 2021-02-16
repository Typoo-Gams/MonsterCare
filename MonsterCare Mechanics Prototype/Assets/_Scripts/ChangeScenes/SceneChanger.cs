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
        
        ThisButton.onClick.AddListener(FadeToLevel);
        ThisButton.onClick.AddListener(TaskOnClick);
    }
            private void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator executeAfter(int A)
    {
        LoadScene();
    }     

    // Update is called once per frame
    private void TaskOnClick()
    {
        
        if (SceneName == "")
        {
            animator.SetTrigger("FadeToLevel");
            
            StartCoroutine(executeAfter(0));

        }
        else
        {
            animator.SetTrigger("FadeToLevel");
            SceneManager.LoadScene(SceneName);
            StartCoroutine(executeAfter(0));

        }
    }
                //Fade to level Test
    public void FadeToLevel()
    {
        animator.SetTrigger("FadeOut");
    }
}
