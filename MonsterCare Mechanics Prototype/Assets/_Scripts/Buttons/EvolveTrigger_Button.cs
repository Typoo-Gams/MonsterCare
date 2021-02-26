using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolveTrigger_Button : MonoBehaviour
{
    GameManager manager;
    Button ThisButton;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        ThisButton = gameObject.GetComponent<Button>();
        ThisButton.onClick.AddListener(EvolutionTrigger);
    }

    void EvolutionTrigger() 
    {
        manager.ActiveMonster.CanEvolveStatus = true;
    }
}
