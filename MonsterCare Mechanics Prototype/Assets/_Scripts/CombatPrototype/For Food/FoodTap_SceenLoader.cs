using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FoodTap_SceenLoader : MonoBehaviour
{
    GameManager manager;
    public int spriteIndex;
    float cnt;
    bool clicked;
    Animator Fade;
    float ClickDelayCnt;
    bool CanBeClicked;

    void Start() 
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        Fade = GameObject.Find("BlackFade").GetComponent<Animator>();
        foreach(Food missingSprite in manager.FoodInventory) 
        {
            if (missingSprite.Sprite == -1) 
            {
                missingSprite.Sprite = spriteIndex;
            }
        }
    }

    private void Update()
    {
        if (clicked)
        {
            cnt += Time.deltaTime;

            if (cnt > Fade.GetCurrentAnimatorStateInfo(0).length)
            {
                SceneManager.LoadScene("LoadScene");
                manager.sceneName = "MonsterHome";
            }
        }

        ClickDelayCnt += Time.deltaTime;
        if (ClickDelayCnt > 1)
            CanBeClicked = true;
    }

    public void OnMouseDown()
    {
        if (GameObject.FindGameObjectWithTag("Note") == null && CanBeClicked) 
        {
            Fade.Play("FadeOut");
            clicked = true;
            gameObject.GetComponent<Image>().enabled = false;
        }
        Debug.Log("Food Pickup");
    }
}
