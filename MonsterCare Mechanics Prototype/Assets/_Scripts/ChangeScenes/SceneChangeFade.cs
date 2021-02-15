using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeFade : MonoBehaviour
{
    public Animator animator;
    private int LevelToLoad;
    // Update is called once per frame
    void Update()
    {
               
    }

    public void FadeToLevel(int levelindex)
    {
        LevelToLoad = levelindex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene("LevelToLoad");
    }
}
