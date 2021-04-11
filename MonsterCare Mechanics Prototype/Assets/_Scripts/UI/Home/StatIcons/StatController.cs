using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatController : MonoBehaviour
{

    //
    public Image[] IconBars = new Image[5];
    public Sprite[] ElementSprites = new Sprite[5];
    public GameObject ElementStat;
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();

        UpdateIcons();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateIcons();
    }

    void UpdateIcons() 
    {
        IconBars[0].fillAmount = (manager.ActiveMonster.HealthStatus / manager.ActiveMonster.GetMaxHealth);
        IconBars[1].fillAmount = (manager.ActiveMonster.EnergyStatus / manager.ActiveMonster.GetMaxEnergy);
        IconBars[2].fillAmount = (manager.ActiveMonster.HungerStatus / manager.ActiveMonster.GetMaxHunger);
        IconBars[3].fillAmount = (manager.ActiveMonster.SleepStatus / manager.ActiveMonster.GetMaxSleep);
        IconBars[4].fillAmount = manager.ActiveMonster.HappinessStatus / manager.ActiveMonster.GetMaxHappiness;

        switch (manager.ActiveMonster.Element)
        {
            case "Air":
                ElementStat.GetComponent<Image>().sprite = ElementSprites[0];
                ElementStat.transform.localScale = new Vector3(27.5039425f, 27.5039425f, 27.5039425f);
                break;

            case "Earth":
                ElementStat.GetComponent<Image>().sprite = ElementSprites[1];
                ElementStat.transform.localScale = new Vector3(27.5039425f, 27.5039425f, 27.5039425f);
                break;

            case "Fire":
                ElementStat.GetComponent<Image>().sprite = ElementSprites[2];
                ElementStat.transform.localScale = new Vector3(27.5039425f, 27.5039425f, 27.5039425f);
                break;

            case "Water":
                ElementStat.GetComponent<Image>().sprite = ElementSprites[3];
                ElementStat.transform.localScale = new Vector3(27.5039425f, 27.5039425f, 27.5039425f);
                break;

            case "None":
                ElementStat.GetComponent<Image>().sprite = ElementSprites[4];
                ElementStat.transform.localScale = new Vector3(21.6754208f, 21.6754208f, 21.6754208f);
                break;
        }
    }
}
