using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatController : MonoBehaviour
{
    

    public Sprite[] HealthIcons = new Sprite[3];
    public Sprite[] EnergyIcons = new Sprite[3];
    public Sprite[] HungerIcons = new Sprite[3];
    public Sprite[] SleepIcons = new Sprite[3];
    public Sprite[] HappyIcons = new Sprite[3];

    public List<Sprite[]> IconSprites;

    public Image[] Icons = new Image[5];
    public Image[] IconBars = new Image[5];
    public Image[] Glow = new Image [5];
    public Image ElementalGlow;
    public Sprite[] ElementSprites = new Sprite[5];
    public GameObject ElementStat;
    GameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        IconSprites = new List<Sprite[]>(){ HealthIcons, EnergyIcons, HungerIcons, SleepIcons, HappyIcons };
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

        IconBars[0].fillAmount = manager.ActiveMonster.HealthStatus / manager.ActiveMonster.GetMaxHealth;
        IconBars[1].fillAmount = manager.ActiveMonster.EnergyStatus / manager.ActiveMonster.GetMaxEnergy;
        IconBars[2].fillAmount = manager.ActiveMonster.HungerStatus / manager.ActiveMonster.GetMaxHunger;
        IconBars[3].fillAmount = manager.ActiveMonster.SleepStatus / manager.ActiveMonster.GetMaxSleep;
        IconBars[4].fillAmount = manager.ActiveMonster.HappinessStatus / manager.ActiveMonster.GetMaxHappiness;

        for (int i = 0; i < Glow.Length; i++) 
        {
            Glow[i].color = Color32.Lerp(Color.red, Color.green, IconBars[i].fillAmount);
        }

        for (int i = 0; i < Icons.Length; i++) 
        {
            if (IconBars[i].fillAmount < 0.3f)
                Icons[i].sprite = IconSprites[i][2];
            if (IconBars[i].fillAmount < 0.6f && IconBars[i].fillAmount > 0.3f)
                Icons[i].sprite = IconSprites[i][1];
            if (IconBars[i].fillAmount > 0.6f)
                Icons[i].sprite = IconSprites[i][0];
        }


        switch (manager.ActiveMonster.Element)
        {
            case "Air":
                ElementStat.GetComponent<Image>().sprite = ElementSprites[0];
                ElementStat.transform.localScale = new Vector3(27.5039425f, 27.5039425f, 27.5039425f);
                ElementalGlow.GetComponent<Image>().color = Color.white;
                ElementStat.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;

            case "Earth":
                ElementStat.GetComponent<Image>().sprite = ElementSprites[1];
                ElementStat.transform.localScale = new Vector3(27.5039425f, 27.5039425f, 27.5039425f);
                ElementalGlow.GetComponent<Image>().color = new Color(0.5372549f, 0.8941177f, 0.4392157f, 1f);
                ElementStat.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;

            case "Fire":
                ElementStat.GetComponent<Image>().sprite = ElementSprites[2];
                ElementStat.transform.localScale = new Vector3(27.5039425f, 27.5039425f, 27.5039425f);
                ElementalGlow.GetComponent<Image>().color = new Color(0.9647059f, 0.4901961f, 0.345098f, 1f);
                ElementStat.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;

            case "Water":
                ElementStat.GetComponent<Image>().sprite = ElementSprites[3];
                ElementStat.transform.localScale = new Vector3(27.5039425f, 27.5039425f, 27.5039425f);
                ElementalGlow.GetComponent<Image>().color = new Color(0.6078432f, 0.6705883f, 0.882353f, 1f);
                ElementStat.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;

            case "None":
                ElementStat.GetComponent<Image>().sprite = null;
                ElementStat.transform.localScale = new Vector3(21.6754208f, 21.6754208f, 21.6754208f);
                ElementalGlow.GetComponent<Image>().color = new Color(0.9647059f, 0.4901961f, 0.345098f, 0f);
                ElementStat.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                break;
        }
    }
}
