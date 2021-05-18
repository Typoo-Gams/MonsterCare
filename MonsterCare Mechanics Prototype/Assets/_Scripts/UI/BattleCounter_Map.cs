using UnityEngine;
using UnityEngine.UI;
using System;

public class BattleCounter_Map : MonoBehaviour
{
    GameManager manager;
    Text myText;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        myText = GetComponent<Text>();    
    }

    // Update is called once per frame
    void Update()
    {
        double battles = Math.Truncate(manager.ActiveMonster.EnergyStatus / 100); 
        myText.text = "Remaining battles: " + battles;
    }
}
