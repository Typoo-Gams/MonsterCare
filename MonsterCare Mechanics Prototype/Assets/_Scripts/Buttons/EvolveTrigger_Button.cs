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
            ThisButton.interactable = true;
        else
            ThisButton.interactable = false;

        if (manager.ActiveMonster.HealthStatus == 0)
            ThisButton.interactable = false;
    }

    void EvolutionTrigger() 
    {
        manager.ActiveMonster.CanEvolveStatus = true;
        manager.ActiveMonster.EnergyStatus -= EvolveEnergyCost;
        FindObjectOfType<SoundManager>().play("ButtonClick");
    }
}
