using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SmokeParticle_Box : MonoBehaviour
{
    public GameObject smokeParticles;
    public Button[] myButton;
    public Text[] myText;
    public int SceneNumber = 0;
    public string SceneName = "";
    GameManager manager;
    float fadeAnimCnt = 0;
    float openAnimCnt = 0;
    Animator Fade;
    Animator Open;
    int tapCounter;
    bool canTap = true;

    private void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        Fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();
        Open = GetComponent<Animator>();
    }

    private void Update()
    {
        if (tapCounter > 2)
        {
            openAnimCnt += Time.deltaTime;
            Open.SetBool("Tapped", true);
            canTap = false;

            foreach (Button found in myButton)
            {
                found.gameObject.SetActive(false);
            }

            foreach (Text found in myText)
            {
                found.gameObject.SetActive(false);
            }

            if (openAnimCnt > Open.GetCurrentAnimatorStateInfo(0).length)
            {
                fadeAnimCnt += Time.deltaTime;
                Fade.Play("FadeOut");

                if (fadeAnimCnt > Fade.GetCurrentAnimatorStateInfo(0).length)
                {
                    if (SceneName == "")
                    {
                        SceneManager.LoadScene(SceneNumber);
                    }
                    else
                    {

                        SceneManager.LoadScene("LoadScene");
                        manager.sceneName = "MonsterHome";
                    }
                }
            }            
        }
    }

    private void OnMouseDown()
    {
        if (canTap)
        {
            FindObjectOfType<SoundManager>().play("BoxTapp");
            Smoke();
            tapCounter++;
        }
    }

    private void Smoke()
    {
        GameObject smokeParticlesPrefab = Instantiate(smokeParticles);
        smokeParticlesPrefab.transform.position = gameObject.transform.position;
        smokeParticlesPrefab.transform.SetParent(gameObject.transform, true);
    }
}
