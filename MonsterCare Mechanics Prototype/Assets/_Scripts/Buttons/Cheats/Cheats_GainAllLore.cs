using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheats_GainAllLore : MonoBehaviour
{
    GameSaver Save = new GameSaver();
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        Save.SaveNote(1, 1);
        Save.SaveNote(2, 1);
        Save.SaveNote(3, 1);
        Save.SaveNote(4, 1);
        Save.SaveObtainedMonster("Child_Gen0", true);
        Save.SaveObtainedMonster("AirSleepy_Gen1", true);
        Save.SaveObtainedMonster("BeefMaster_Gen1", true);
        Save.SaveObtainedMonster("FireSleepy_Gen1", true);
        Save.SaveObtainedMonster("WaterPlayful_Gen1", true);
        //"Child_Gen0", "AirSleepy_Gen1", "BeefMaster_Gen1", "FireSleepy_Gen1", "WaterPlayful_Gen1"
    }
}
