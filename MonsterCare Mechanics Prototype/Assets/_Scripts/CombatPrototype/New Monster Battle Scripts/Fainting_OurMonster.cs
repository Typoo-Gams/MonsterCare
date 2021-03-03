using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fainting_OurMonster : MonoBehaviour
{
    GameManager manager;

    public GameObject blackOutSquare;

    // Update is called once per frame
    void Update()
    {
        if(manager.ActiveMonster.DeathStatus == true)
        {
            StartCoroutine(FadeBlackOutSquare());
        }
        else
        {
            StartCoroutine(FadeBlackOutSquare(false));
        }
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 3)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while(blackOutSquare.GetComponent<SpriteRenderer>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                yield return null;
                SceneManager.LoadScene("Home");
            }
        }
        else
        {
            while(blackOutSquare.GetComponent<SpriteRenderer>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<SpriteRenderer>().color = objectColor;
                yield return null;
            }
        }
    }
}
