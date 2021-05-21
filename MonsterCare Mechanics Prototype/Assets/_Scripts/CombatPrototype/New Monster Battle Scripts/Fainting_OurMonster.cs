using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Fainting_OurMonster : MonoBehaviour
{
    GameManager manager;
    public GameObject black;

    public Text text, timer;
    public GameObject[] UI;

    float count;
    public float whaitTime;
    bool clickeed;
    public float fadeCnt;
    Animator Fade;

    private void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        Fade = GameObject.Find("BlackFade").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.ActiveMonster.DeathStatus == true)
        {
            Fainting();

            float cnt = Mathf.Clamp(count += Time.deltaTime, 0 , whaitTime);
            timer.text = "" + Math.Truncate(whaitTime - cnt);
            if (cnt == whaitTime) 
            {
                timer.text = "Tap to continue.";
                if (Input.GetMouseButtonDown(0))
                {
                    text.gameObject.SetActive(false);
                    clickeed = true;
                    Fade.Play("FadeOut");
                }
                if (clickeed) 
                {
                    fadeCnt += Time.deltaTime;
                    if (fadeCnt > Fade.GetCurrentAnimatorStateInfo(0).length) 
                    {
                        SceneManager.LoadScene("LoadScene");
                        manager.sceneName = "MonsterHome";
                    }
                }
            }
        }
    }

    private IEnumerator Wait()
    {
        if (manager.EnemyMonster != null)
        {
            yield return new WaitForSeconds(1);
            black.SetActive(true);
            text.gameObject.SetActive(true);
            timer.gameObject.SetActive(true);
            //manager.ActiveMonster.GetHealthbar().SetActive(false);
            manager.EnemyMonster.GetHealthbar().SetActive(false);
            manager.Enemy.SetActive(false);
            for (int i = 0; i < UI.Length; i++)
            {
                UI[i].SetActive(false);
            }
        }
    }

    public void Fainting()
    {
        StartCoroutine(Wait());
    }
}
