using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolveTrigger_Button : MonoBehaviour
{
    public bool PlaySounds;

    GameManager manager;
    Button ThisButton;
    public int EvolveEnergyCost;

    public ParticleSystem glowing;
    public Animator ShowEvolveButton;
    bool IsSet;
    string currentColor;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        ThisButton = gameObject.GetComponent<Button>();
        ThisButton.onClick.AddListener(EvolutionTrigger);
    }

    private void Update()
    {
        if (manager.ActiveMonster.Element != "None" && manager.ActiveMonster.EnergyStatus > EvolveEnergyCost)
        {
            ShowEvolveButton.SetBool("Active", true);
            ParticleSystem.MainModule settings = glowing.main;
            if (!IsSet || manager.ActiveMonster.Element != currentColor) 
            {
                switch (manager.ActiveMonster.Element)
                {
                    case "Air":
                        currentColor = "Air";
                        settings.startColor = new ParticleSystem.MinMaxGradient(Color.white);
                        break;
                    case "Earth":
                        currentColor = "Earth";
                        settings.startColor = new ParticleSystem.MinMaxGradient(new Color(0.5372549f, 0.8941177f, 0.4392157f));
                        break;
                    case "Fire":
                        currentColor = "Fire";
                        settings.startColor = new ParticleSystem.MinMaxGradient(new Color(0.9647059f, 0.4901961f, 0.345098f));
                        break;
                    case "Water":
                        currentColor = "Water";
                        settings.startColor = new ParticleSystem.MinMaxGradient(new Color(0.6078432f, 0.6705883f, 0.882353f));
                        break;
                }
            }
            IsSet = true;
        }
        else
        {
            ShowEvolveButton.SetBool("Active", false);
            IsSet = false;
        }

        if (manager.ActiveMonster.HealthStatus == 0)
        {
            ShowEvolveButton.SetBool("Active", false);
            IsSet = false;
        }
    }

    void EvolutionTrigger() 
    {
        manager.ActiveMonster.CanEvolveStatus = true;
        manager.ActiveMonster.EnergyStatus -= EvolveEnergyCost;
        FindObjectOfType<SoundManager>().play("ButtonClick");
    }
}
