using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool DevBuild;

    //Previously loaded scene
    public int PreviousSecene;
    public bool SleepMemory;

    //for checking which scene it is
    public int sceneNumber;
    public string sceneName;

    //Animator for fade scene transitions
    public Animator Fade;

    //GameSaver for saving info.
    GameSaver Save = new GameSaver();

    //Currently Active Monster
    public Monster ActiveMonster;
    public GameObject MonsterObject;

    //Currently Active Enemy Monster
    public GameObject Enemy;
    public Monster EnemyMonster;

    //slider for stuff
    public GameObject HealthBarPrefab;

    //GameVersion
    [NonSerialized]
    public string GameVersion;

    //Player Inventory
    public Food[] FoodInventory = new Food[5];

    //latest reward
    public Food FoodReward;

    //Remember if this is a new game.
    public bool NewSave;

    //Hide UI
    public bool HideUI;

    //Awake is called when the script instance is being loaded
    private void Awake()
    {
        GameVersion = "14";
        Debug.LogWarning("GameVersion is V." + GameVersion);
        FoodInventory = new Food[]{
            new Food(true),
            new Food(true),
            new Food(true),
            new Food(true),
            new Food(true)
        };
        FoodInventory = Save.LoadFood();
    }


    //Start is called just before any of the Update methods is called the first time
    private void Start()
    {
        //temporary
        FoodInventory[0] = new Food(false, 1);
        FoodInventory[1] = new Food("Air");
        FoodInventory[2] = new Food("Fire");
        FoodInventory[3] = new Food("Earth");
        FoodInventory[4] = new Food("Water");
        Save.SaveFood(FoodInventory);
        //Debug.Log("Manager Start");
        Save.PrintObtainedMonsters();
    }


    private void Update() 
    {
        if (SceneManager.GetActiveScene().name == "MonsterHome")
        {
            //Dev cheats
            if (Input.GetKeyDown(KeyCode.I))
            {
                ActiveMonster.UpdateHealth(100);
                ActiveMonster.HappinessStatus = 100;
                ActiveMonster.UpdateHappiness();
                ActiveMonster.UpdateHunger(100);
                ActiveMonster.SleepStatus = 100;
                ActiveMonster.UpdateSleeping(false);
                ActiveMonster.EnergyStatus = 10;

                FoodInventory[0] = new Food(false);
                FoodInventory[0].Sprite = 12;
                FoodInventory[1] = new Food("Air");
                FoodInventory[2] = new Food("Earth");
                FoodInventory[3] = new Food("Fire");
                FoodInventory[4] = new Food("Water");
                Save.SaveFood(FoodInventory);
                Debug.Log("Cheat I activated");
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                ActiveMonster.UpdateHealth(1);
                ActiveMonster.UpdateHunger(23);
                ActiveMonster.EnergyStatus = 0;
                Save.PrintObtainedMonsters();
                Debug.Log("Cheat O activated");
            }

            if(Input.GetKeyDown(KeyCode.P))
            {
                ActiveMonster.EnergyStatus = 5;
                ActiveMonster.HungerStatus = 50;
            }
        }
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
        if (MonsterObject != null) 
        {
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

        Debug.Log("previous scene: " + PreviousSecene);
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
        SaveGameVersion("");
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
        PlayerPrefs.SetString(MonsterSaveIndex + "LastEatenElement", "None");

        for (int i = 0; i < StatIndex.Length; i++)
        {
            PlayerPrefs.SetFloat(MonsterSaveIndex + StatIndex[i], 0);
        }

        //Sets empty inventory slots
        Food[] cleanInv = 
            { new Food(true),
              new Food(true),
              new Food(true),
              new Food(true),
              new Food(true),
            };
        SaveFood(cleanInv);

        //Resets the obtained monster list 
        SaveObtainedMonster("None", false, true);

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
            PlayerPrefs.SetString(MonsterSaveIndex + "LastEatenElement", yourMonster.Element);
            

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
                if(GameObject.FindGameObjectWithTag("Monster") != null) 
                {
                    Debug.LogWarning("but the monster is still in the scene.");
                }
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
        yourMonster.Element = PlayerPrefs.GetString(MonsterSaveIndex + "LastEatenElement");
        if (yourMonster.Element == "") 
        {
            yourMonster.Element = "None";
        }
        yourMonster.HealthStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[0]);
        yourMonster.HungerStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[1]);
        yourMonster.SleepStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[2]);
        yourMonster.HappinessStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[3]);
        yourMonster.PlayfullStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[4]);
        yourMonster.ToughnessStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[5]);
        yourMonster.EnergyStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[6]);

    }


    /// <summary>
    /// Prints The list of obtainable monsters and if they have been obtained.
    /// </summary>
    public void PrintObtainedMonsters() 
    {
        string PrintLog = "Monsters obtained: \n";
        string[] MonsterIndex =
        {"Child_Gen0", "AirSleepy_Gen1", "BeefMaster_Gen1", "FireSleepy_Gen1", "WaterPlayful_Gen1" };
        string ObtainedSaveIndex = "ObatainedMonster_";
        foreach (string Name in MonsterIndex)
        {
            string fullIndex = ObtainedSaveIndex + Name;
            PrintLog += fullIndex + ": " + PlayerPrefs.GetInt(fullIndex) + "\n";
        }
        Debug.Log(PrintLog);
    }


    /// <summary>
    /// Gets the bool if a monster has been obtained before.
    /// </summary>
    /// <param name="GetObtainedMonster"></param>
    /// <returns></returns>
    public bool MonsterObtainedBefore(string GetObtainedMonster) 
    {
        bool indexFound = false;
        bool IsObtained = false;
        string[] MonsterIndex =
            {"Child_Gen0", "AirSleepy_Gen1", "BeefMaster_Gen1", "FireSleepy_Gen1", "WaterPlayful_Gen1" };
        string ObtainedSaveIndex = "ObatainedMonster_";
        foreach (string Name in MonsterIndex)
        {
            if (GetObtainedMonster == Name)
            {
                indexFound = true;
                break;
            }
        }
        if(indexFound)
        {
            string fullIndex = ObtainedSaveIndex + GetObtainedMonster;
            if (PlayerPrefs.GetInt(fullIndex) == 1)
                IsObtained = true;
            return IsObtained;
        }
        else
            Debug.LogError("Can't find the index: " + GetObtainedMonster + ", try checking the index list.");
        return IsObtained;
    }

    /// <summary>
    /// Saves which monster has been obtained with an index.
    /// </summary>
    /// <param name="MonsterGenName">The Gen name and monster element</param>
    /// <param name="IsObtained">Sets Obtained true/false</param>
    /// <param name="Wipe">Wipes the obtained monsters if true.</param>
    public void SaveObtainedMonster(string MonsterGenName, bool IsObtained, bool Wipe = false) 
    {
        bool indexFound = false;
        int IsObtainedInt;
        if (IsObtained)
            IsObtainedInt = 1;
        else
            IsObtainedInt = 0;
        string[] MonsterIndex =
            {"Child_Gen0", "AirSleepy_Gen1", "BeefMaster_Gen1", "FireSleepy_Gen1", "WaterPlayful_Gen1" };
        string ObtainedSaveIndex = "ObatainedMonster_";
        foreach(string Name in MonsterIndex)
        {
            if (MonsterGenName == Name && !Wipe)
            {
                indexFound = true;
                break;
            }
            if (Wipe) 
            {
                string fullIndex = ObtainedSaveIndex + Name;
                PlayerPrefs.SetInt(fullIndex, 0);
            }
        }
        if (indexFound)
        {
            string fullIndex = ObtainedSaveIndex + MonsterGenName;
            PlayerPrefs.SetInt(fullIndex, IsObtainedInt);
        }
        else if (!Wipe)
            Debug.LogError("Can't find the index: " + MonsterGenName + ", try checking the index list.");
    }

    //Saves one new food into the goblin inventory
    public void AddGoblinInv(Food ObtainedFood)
    {
        string inv = "Goblin Inventory Load: ";
        Food[] GoblinInv = new Food[2];
        for(int i = 0; i > GoblinInv.Length; i++)
        {
            if (GoblinInv[i] == null) 
            {
                GoblinInv[i] = ObtainedFood;
            }
            else 
            {
                Debug.LogError("GoblinInv is full");
            }
        }

        string GoblinInvIndex = "GoblinInv_Slot";
        for (int i = 0; i < GoblinInvIndex.Length; i++)
        {
            string SaveIndex = GoblinInvIndex + i + "_";
            if (GoblinInv[i].FoodType != "None")
            {

                PlayerPrefs.SetInt(SaveIndex + "_Power", GoblinInv[i].Power);
                PlayerPrefs.SetString(SaveIndex + "_Type", GoblinInv[i].FoodType);
                PlayerPrefs.SetString(SaveIndex + "_Element", GoblinInv[i].Element);
                PlayerPrefs.SetInt(SaveIndex + "_SpriteIndex", GoblinInv[i].Sprite);
                inv += "\nInventory Slot " + i + ": Type: " + GoblinInv[i].FoodType + ", Element: " + GoblinInv[i].Element + ", Power: " + GoblinInv[i].Power + ", Sprite: " + GoblinInv[i].Sprite;
            }
            else
            {
                PlayerPrefs.SetInt(SaveIndex + "_Power", 0);
                PlayerPrefs.SetString(SaveIndex + "_Type", "None");
                PlayerPrefs.SetString(SaveIndex + "_Element", "None");
                PlayerPrefs.SetInt(SaveIndex + "_SpritePath", -1);
            }
        }
        Debug.Log(inv);
    }


    //Get Goblin inventory
    public Food[] GetGoblinInv() 
    {
        string GoblinInvIndex = "GoblinInv_Slot";
        Food[] load = new Food[2];
        string inv = "Goblin Inventory Load: ";
        for (int i = 0; i < load.Length; i++)
        {
            string SaveIndex = GoblinInvIndex + i;
            int power = PlayerPrefs.GetInt(SaveIndex + "_Power");
            string type = PlayerPrefs.GetString(SaveIndex + "_Type");
            string element = PlayerPrefs.GetString(SaveIndex + "_Element");
            int SpriteIndex = PlayerPrefs.GetInt(SaveIndex + "_SpriteIndex");
            if (type == "")
                type = "None";
            load[i] = new Food(type, element, power);
            load[i].Sprite = SpriteIndex;
            inv += "\nInventory Slot " + i + ": Type: " + load[i].FoodType + ", Element: " + load[i].Element + ", Power: " + load[i].Power + ", Sprite: " + load[i].Sprite;
        }
        Debug.Log(inv);
        return load;
    }


    //Clear Goblin inventory
    public void ClearGoblinInv()
    {
        string inv = "Goblin Inventory Load: ";
        Food[] empty = new Food[2];
        string GoblinInvIndex = "GoblinInv_Slot";
        for (int i = 0; i < empty.Length; i++)
        {
            string SaveIndex = GoblinInvIndex + i + "_";
            if (empty[i].FoodType != "None")
            {

                PlayerPrefs.SetInt(SaveIndex + "_Power", empty[i].Power);
                PlayerPrefs.SetString(SaveIndex + "_Type", empty[i].FoodType);
                PlayerPrefs.SetString(SaveIndex + "_Element", empty[i].Element);
                PlayerPrefs.SetInt(SaveIndex + "_SpriteIndex", empty[i].Sprite);
                inv += "\nInventory Slot " + i + ": Type: " + empty[i].FoodType + ", Element: " + empty[i].Element + ", Power: " + empty[i].Power + ", Sprite: " + empty[i].Sprite;
            }
            else
            {
                PlayerPrefs.SetInt(SaveIndex + "_Power", 0);
                PlayerPrefs.SetString(SaveIndex + "_Type", "None");
                PlayerPrefs.SetString(SaveIndex + "_Element", "None");
                PlayerPrefs.SetInt(SaveIndex + "_SpritePath", -1);
            }
        }
        Debug.Log(inv);
    }

    //Saves the inventory
    public void SaveFood(Food[] inventory) 
    {
        string inv = "Inventory Save: ";

        string InventorySlot = "InventorySlot_";
        for (int i = 0; i < inventory.Length; i++)
        {
            string SaveIndex = InventorySlot + i;
            if (inventory[i].FoodType != "None")
            {
                
                PlayerPrefs.SetInt(SaveIndex + "_Power", inventory[i].Power);
                PlayerPrefs.SetString(SaveIndex + "_Type", inventory[i].FoodType);
                PlayerPrefs.SetString(SaveIndex + "_Element", inventory[i].Element);
                PlayerPrefs.SetInt(SaveIndex + "_SpriteIndex", inventory[i].Sprite);
                inv += "\nInventory Slot " + i + ": Type: " + inventory[i].FoodType + ", Element: " + inventory[i].Element + ", Power: " + inventory[i].Power + ", Sprite: " + inventory[i].Sprite;
            }
            else 
            {
                PlayerPrefs.SetInt(SaveIndex + "_Power", 0);
                PlayerPrefs.SetString(SaveIndex + "_Type", "None");
                PlayerPrefs.SetString(SaveIndex + "_Element", "None");
                PlayerPrefs.SetInt(SaveIndex + "_SpritePath", -1);
            }
        }
        Debug.Log(inv);
    }


    //Loads the Food Inventory
    public Food[] LoadFood() 
    {
        string InventorySlot = "InventorySlot_";
        Food[] load = new Food[5];
        string inv = "Inventory Load: ";
        for (int i = 0; i < load.Length; i++) 
        {
            string SaveIndex = InventorySlot + i;
            int power = PlayerPrefs.GetInt(SaveIndex + "_Power");
            string type = PlayerPrefs.GetString(SaveIndex + "_Type");
            string element = PlayerPrefs.GetString(SaveIndex + "_Element");
            int SpriteIndex = PlayerPrefs.GetInt(SaveIndex + "_SpriteIndex");
            if (type == "") 
                type = "None";
            load[i] = new Food(type, element, power);
            load[i].Sprite = SpriteIndex;
            inv += "\nInventory Slot " + i + ": Type: " + load[i].FoodType + ", Element: " + load[i].Element + ", Power: " + load[i].Power + ", Sprite: " + load[i].Sprite;
        }
        Debug.Log(inv);
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

    public void DeleteFood(int index) 
    {
        PlayerPrefs.SetInt("InventorySlot_" + index + "_Power", 0);
        PlayerPrefs.SetString("InventorySlot_" + index + "_Type", "None");
        PlayerPrefs.SetString("InventorySlot_" + index + "_Element", "None");
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