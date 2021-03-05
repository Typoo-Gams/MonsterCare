using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //Previously loaded scene
    public int PreviousSecene;
    public bool SleepMemory;
    //GameSaver for saving info.
    GameSaver Save = new GameSaver();
    //Currently Active Monster
    public Monster ActiveMonster;
    public GameObject MonsterObject;
    //Currently Active Enemy Monster
    public GameObject Enemy;
    public Monster EnemyMonster;
    //slider for stuff
    public Slider SliderPrefab;
    public Slider GreenSliderPrefab;

    //GameVersion
    [NonSerialized]
    public string GameVersion;

    //Player Inventory
    public Food[] FoodInventory;
    //latest reward
    public Food FoodReward;

    //Awake is called when the script instance is being loaded
    private void Awake()
    {
        GameVersion = "8.2.4";
        FoodInventory = Save.LoadFood();
    }


    //Start is called just before any of the Update methods is called the first time
    private void Start()
    {
        FoodInventory = new Food[5];
        FoodInventory[0] = new Food();
        FoodInventory[1] = new Food("Earth");
        Save.SaveFood(FoodInventory);
        Debug.LogWarning("ItemType: " + FoodInventory[0].GetType() + "\npath: " + PlayerPrefs.GetString("InventorySlot_0_Path"));
    }


    //When the application is killed then save
    private void OnApplicationQuit()
    {
        try
        {
            Save.SaveTime();
            Save.SaveMonster(ActiveMonster);
            Save.SaveFood(FoodInventory);
            Save.SaveGameVersion(GameVersion);
        }
        catch
        {
            Debug.LogError("Something went wrong when saving");
        }
    }
    

    //When the application is paused then save.
    private void OnApplicationPause(bool focus)
    {
        try
        {
            Save.SaveTime();
            Save.SaveMonster(ActiveMonster);
            Save.SaveFood(FoodInventory);
            Save.SaveGameVersion(GameVersion);
            Debug.Log("Saved with pause");
        }
        catch
        {
            Debug.LogError("Something went wrong when saving");
        }
    }
    

    //Called when a new scene is loaded.
    private void OnLevelWasLoaded(int level)
    {
        //Canvas canvas =  GameObject.Find("Canvas").GetComponent<Canvas>();
        //canvas.worldCamera = Camera.main;
        Debug.Log("previous scene: "+PreviousSecene);
        if (SceneManager.GetActiveScene().name == "MonsterHome") 
        {
            //change to renderer so that stats can change while in other scenes?
            MonsterObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            //change to renderer so that stats can change while in other scenes?
            MonsterObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    //Set active monster so the gamemanager knows the monsters stats.
    /// <summary>
    /// Set the active monster
    /// </summary>
    /// <param name="yourMonster">Your monster</param>
    public void GetMonster(Monster yourMonster) 
    {
        ActiveMonster = yourMonster;
    }


    //Set the gameobject for the active monster so the gamemanager has acess to it.
    /// <summary>
    /// set the gameobject for the active monster
    /// </summary>
    /// <param name="yourMonster">your monster gameobject</param>
    public void GetMonster(GameObject yourMonster)
    {
        MonsterObject = yourMonster;
    }
}


public class GameSaver 
{
    //------------------------Save Time in PlayerPrefs---------------------//


    public DateTime Time = DateTime.Now;


    //Sets the savefile version number.
    /// <summary>
    /// Saves the game version to the game file. 
    /// </summary>
    /// <param name="version">version of the game</param>
    public void SaveGameVersion(string version) 
    {
        PlayerPrefs.SetString("MonsterCare_Version_", version);
    }


    //returns the version. used to check if a savefile has the same game version
    /// <summary>
    /// returns the game version as a string.
    /// </summary>
    /// <returns>version number</returns>
    public string LoadgameVersion() 
    {
        return PlayerPrefs.GetString("MonsterCare_Version_");
    }


    /// <summary>
    /// Wipes all the save file data
    /// </summary>
    public void WipeSave() 
    {
        SaveGameVersion("None");
        string[] TimeIndex =
            {"Hour", "Minutes", "Seconds", "Day", "Month", "Year"};
        for (int i = 0; i < TimeIndex.Length; i++)
        {
            string FullIndex = "SavedTime_" + TimeIndex[i];
            PlayerPrefs.SetFloat(FullIndex, 0);
        }
        string[] StatIndex =
            {"Health", "Hunger", "Sleep", "Happiness", "Playfull", "Toughness", "Energy"};
        string MonsterSaveIndex = "SavedMonster_";

        PlayerPrefs.SetString(MonsterSaveIndex + "MonsterName", "None");
        PlayerPrefs.SetString(MonsterSaveIndex + "PrefabLocation", "None");

        for (int i = 0; i < StatIndex.Length; i++)
        {
            PlayerPrefs.SetFloat(MonsterSaveIndex + StatIndex[i], 0);
        }
        Debug.LogWarning("Save was wiped");
    }


    /// <summary>
    /// calculates the difference between now and last time "SaveTime" where used.
    /// </summary>
    /// <returns>Total seconds that have passed.</returns>c
    public float FindTimeDifference()
    {
        DateTime ThisFrame = DateTime.Now;
        float[] LastTime = LoadTime();
        float[] currentTime = { ThisFrame.Hour, ThisFrame.Minute, ThisFrame.Second, ThisFrame.Day, ThisFrame.Month, ThisFrame.Year };
        float[] TimeDifference = new float[currentTime.Length];
        float[] conversions = { 3600, 60, 1, 86400, 2629800, 31557600 };
        float DifferenceInSeconds = 0;

        //Calculating the difference from saved time to now in seconds
        for (int i = 0; i < currentTime.Length; i++)
        {
            TimeDifference[i] = LastTime[i] - currentTime[i];
            //Turns the difference positive if negative. if there is anything wrong this is where it is. time calculated wrongly.
            if (TimeDifference[i] < 0)
                TimeDifference[i] = TimeDifference[i] * -1;
            if (TimeDifference[i] > 0)
                DifferenceInSeconds += TimeDifference[i] * conversions[i]; //converts to secs.
        }
        return DifferenceInSeconds;
    }


    /// <summary>
    /// Saves current computer time to a save file.
    /// </summary>
    public void SaveTime()
    {

        string[] TimeIndex =
            {"Hour", "Minutes", "Seconds", "Day", "Month", "Year"};
        float[] TimeTable =
            {Time.Hour, Time.Minute, Time.Second, Time.Day, Time.Month, Time.Year};
        for (int i = 0; i < TimeIndex.Length; i++)
        {
            string FullIndex = "SavedTime_" + TimeIndex[i];
            PlayerPrefs.SetFloat(FullIndex, TimeTable[i]);
        }
    }


    /// <summary>
    /// Loads the saved floats from the saved time as an array.
    /// </summary>
    public float[] LoadTime()
    {
        string[] TimeIndex =
            {"Hour", "Minutes", "Seconds", "Day", "Month", "Year"};
        float[] TimeTable = new float[TimeIndex.Length];
        for (int i = 0; i < TimeIndex.Length; i++)
        {
            string fullIndex = "SavedTime_" + TimeIndex[i];
            TimeTable[i] = PlayerPrefs.GetFloat(fullIndex);
        }

        //debug
        //string TimeSaved = TimeTable[0] + ":" + TimeTable[1] + ":" + TimeTable[2] + "   " + TimeTable[3] + "/" + TimeTable[4] + "/" + TimeTable[5];
        //Debug.Log(TimeSaved);

        //return
        return TimeTable;
    }


    /// <summary>
    /// Loads spesified value from saved time in playerprefs
    /// </summary>
    /// <param name="Name">Names: "Hour", "Minutes", "Seconds", "Day", "Month", "Year"</param>
    /// <returns></returns>
    public float LoadTime(string Name)
    {
        string fullIndex = "SavedTime_" + Name;
        return PlayerPrefs.GetFloat(fullIndex);
    }


    /// <summary>
    /// Loads spesified value from saved time in playerprefs
    /// </summary>
    /// <param name="Index">indexes: Hour - 0, Minutes - 1, Seconds - 2, Day - 3, Month - 4, Year - 5</param>
    /// <returns></returns>
    public float LoadTime(int Index)
    {
        string[] TimeIndex =
            {"Hour", "Minutes", "Seconds", "Day", "Month", "Year"};
        string fullIndex = "SavedTime_" + TimeIndex[Index];
        return PlayerPrefs.GetFloat(fullIndex);
    }


    //saves the inputed monster
    /// <summary>
    /// Save the floats and name of a monster
    /// </summary>
    /// <param name="yourMonster">Input a monster to be saved</param>
    public void SaveMonster(Monster yourMonster) 
    {
        try 
        {
            string[] StatIndex =
            {"Health", "Hunger", "Sleep", "Happiness", "Playfull", "Toughness", "Energy"};
            float[] Stats =
                {yourMonster.HealthStatus, yourMonster.HungerStatus, yourMonster.SleepStatus, yourMonster.HappinessStatus, yourMonster.PlayfullStatus, yourMonster.ToughnessStatus, yourMonster.EnergyStatus};

            string MonsterSaveIndex = "SavedMonster_";

            PlayerPrefs.SetString(MonsterSaveIndex + "MonsterName", yourMonster.Name);
            PlayerPrefs.SetString(MonsterSaveIndex + "PrefabLocation", yourMonster.PrefabLocation);

            for (int i = 0; i < StatIndex.Length; i++)
            {
                PlayerPrefs.SetFloat(MonsterSaveIndex + StatIndex[i], Stats[i]);
            }
            Debug.Log("Monster Was Saved");
        } 
        catch 
        {
            if (yourMonster == null) 
            {
                Debug.LogWarning("The input monster was empty and couldnt save.");
            }
        }
        
    }

    //Loads the monster stats from the savefile
    /// <summary>
    /// Loads the saved monster into the spesified monster
    /// </summary>
    /// <param name="yourMonster">Input a monster to load the stats into</param>
    public void LoadMonster(Monster yourMonster)
    {
        string[] StatIndex =
            {"Health", "Hunger", "Sleep", "Happiness", "Playfull", "Toughness", "Energy"};

        string MonsterSaveIndex = "SavedMonster_";

        yourMonster.Name = PlayerPrefs.GetString(MonsterSaveIndex + "MonsterName");
        yourMonster.HealthStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[0]);
        yourMonster.HungerStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[1]);
        yourMonster.SleepStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[2]);
        yourMonster.HappinessStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[3]);
        yourMonster.PlayfullStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[4]);
        yourMonster.ToughnessStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[5]);
        yourMonster.EnergyStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[6]);
    }


    //Saves the inventory
    public void SaveFood(Food[] inventory) 

    {
        string InventorySlot = "InventorySlot_";
        for (int i = 0; i < inventory.Length; i++) 
        {
            if (inventory[i] != null) 
            {
                string SaveIndex = InventorySlot + i;
                PlayerPrefs.SetInt(SaveIndex + "_Power", inventory[i].Power);
                PlayerPrefs.SetString(SaveIndex + "_Type", inventory[i].FoodType);
                PlayerPrefs.SetString(SaveIndex + "_Element", inventory[i].Element);
            }
        }
    }


    //Loads the Food Inventory
    public Food[] LoadFood() 
    {
        string InventorySlot = "InventorySlot_";
        Food[] load = new Food[5];
        for (int i = 0; i < load.Length; i++) 
        {
            string SaveIndex = InventorySlot + i;
            int power = PlayerPrefs.GetInt(SaveIndex + "_Power");
            string type = PlayerPrefs.GetString(SaveIndex + "_Type");
            string element = PlayerPrefs.GetString(SaveIndex + "_Element");
            load[i] = new Food(type, element, power);
        }
        return load;
    }

    public Food LoadFood(int Index) 
    {
        string InventorySlot = "InventorySlot_";
        string SaveIndex = InventorySlot + Index;
        int power = PlayerPrefs.GetInt(SaveIndex + "_Power");
        string type = PlayerPrefs.GetString(SaveIndex + "_Type");
        string element = PlayerPrefs.GetString(SaveIndex + "_Element");
        return new Food(type, element, power);
    }

    //Loads the saved monster's prefab location.
    /// <summary>
    /// Gets the prefab location for the currently saved monster
    /// </summary>
    public string GetMonsterPrefab() 
    {
        return PlayerPrefs.GetString("SavedMonster_" + "PrefabLocation");
    }
}