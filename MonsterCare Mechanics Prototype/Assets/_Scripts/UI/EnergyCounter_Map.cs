using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnergyCounter_Map : MonoBehaviour
{
    GameManager manager;
    public Image fill, glow, icon;
    public Sprite[] IconSprites;
    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        fill.fillAmount = manager.ActiveMonster.EnergyStatus / manager.ActiveMonster.GetMaxEnergy;
        glow.color = Color32.Lerp(Color.red, Color.green, fill.fillAmount);

        if (fill.fillAmount < 0.3f)
        {
            icon.sprite = IconSprites[2];
            myAnim.SetBool("IsLow", true);
        }
        if (fill.fillAmount < 0.6f && fill.fillAmount > 0.3f)
        {
            icon.sprite = IconSprites[1];
            myAnim.SetBool("IsLow", false);
        }
        if (fill.fillAmount > 0.6f)
        {
            icon.sprite = IconSprites[0];
            myAnim.SetBool("IsLow", false);
        }
    }
}
