using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIsleepText : MonoBehaviour
{
    public GameObject ThisMonster;
    Monster monster;

    void Start() 
    {
        monster = ThisMonster.GetComponent<DefaultStarting_MonsterController>().monster;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = string.Format("Sleeping for: {0} hours", ThisMonster.GetComponent<DefaultStarting_MonsterController>().TimeToSleep);
    }
}
