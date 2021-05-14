using UnityEngine;
using System;

public class GameSaver
{
    //------------------------Save Time in PlayerPrefs---------------------//


    public DateTime Time = DateTime.Now;

    private int NumberOfNotes = 4;

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
    /// Sets the state of the tutorial
    /// </summary>
    /// <param name="state"></param>
    public void IsTutorialDone(bool state)
    {
        if (state)
            PlayerPrefs.SetInt("Tutorial_Done", 1);
        else
            PlayerPrefs.SetInt("Tutorial_Done", 0);
    }


    /// <summary>
    /// returns from the save file if the tutorial is done or not
    /// </summary>
    /// <returns></returns>
    public bool IsTutorialDone()
    {
        int save = PlayerPrefs.GetInt("Tutorial_Done");

        if (save == 1)
            return true;
        return false;
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

        for (int i = 0; i <= NumberOfNotes; i++)
        {
            SaveNote(i, 0);
        }

        Debug.LogWarning("Save was wiped");
    }


    /// <summary>
    /// calculates the difference between now and last time "SaveTime" where used.
    /// </summary>
    /// <returns>Total seconds that have passed.</returns>c
    public double FindTimeDifference()
    {
        DateTime ThisFrame = DateTime.Now;
        DateTime LastTime = LoadTime();

        TimeSpan Subtraction = ThisFrame.Subtract(LastTime);

        return Subtraction.TotalSeconds;
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

        //debug
        //string TimeSaved = TimeTable[0] + ":" + TimeTable[1] + ":" + TimeTable[2] + "   " + TimeTable[3] + "/" + TimeTable[4] + "/" + TimeTable[5];
        //Debug.Log(TimeSaved);
    }


    /// <summary>
    /// Loads the saved floats from the saved time as an array.
    /// </summary>
    public DateTime LoadTime()
    {
        DateTime loadedTime;
        if ((int)PlayerPrefs.GetFloat("SavedTime_Year") != 0)
        {
            loadedTime = new DateTime(
            (int)PlayerPrefs.GetFloat("SavedTime_Year"),
            (int)PlayerPrefs.GetFloat("SavedTime_Month"),
            (int)PlayerPrefs.GetFloat("SavedTime_Day"),
            (int)PlayerPrefs.GetFloat("SavedTime_Hour"),
            (int)PlayerPrefs.GetFloat("SavedTime_Minutes"),
            (int)PlayerPrefs.GetFloat("SavedTime_Seconds")
            );
        }
        else
        {
            loadedTime = new DateTime(0, 0, 0, 0, 0, 0);
        }
        return loadedTime;
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
            PlayerPrefs.SetString(MonsterSaveIndex + "LastEatenElement", yourMonster.Element.ToString());


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
                if (GameObject.FindGameObjectWithTag("Monster") != null)
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

        if (!PlayerPrefs.GetString(MonsterSaveIndex + "LastEatenElement").Equals(""))
            yourMonster.Element = (MonsterElement)Enum.Parse(typeof(MonsterElement), PlayerPrefs.GetString(MonsterSaveIndex + "LastEatenElement"));
        else
            yourMonster.Element = MonsterElement.None;
        
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
        if (indexFound)
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
        foreach (string Name in MonsterIndex)
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


    /// <summary>
    /// Saves an obtained note so it can be viewed in the notes scene. 
    /// </summary>
    /// <param name="NoteNumber">Number of the note saved</param>
    /// <param name="IsObtained">1: obtained, 0: not obtained</param>
    public void SaveNote(int NoteNumber, int IsObtained)
    {
        string NoteIndex = "ObtainedNote_" + NoteNumber;
        switch (IsObtained)
        {
            case 1:
                PlayerPrefs.SetInt(NoteIndex, 1);
                break;

            case 0:
                PlayerPrefs.SetInt(NoteIndex, 0);
                break;

            default:
                Debug.LogError("Is Obtained needs to be between 0-1. (false/true)");
                break;
        }
    }

    /// <summary>
    /// Reads the memory of index NoteNumber. returns 2 if note is out of index range.
    /// </summary>
    /// <param name="NoteNumber">load index note</param>
    public int LoadNote(int NoteNumber)
    {
        if(NoteNumber <= NumberOfNotes)
        {
            string NoteIndex = "ObtainedNote_" + NoteNumber;
            return PlayerPrefs.GetInt(NoteIndex);
        }
        Debug.LogError("Couldnt load note number: " + NoteNumber + ", because it was out of index range.");
        return 2;
    }

    //Saves one new food into the goblin inventory
    //(Unused)
    public void AddGoblinInv(Food ObtainedFood)
    {
        string inv = "Goblin Inventory Load: ";
        Food[] GoblinInv = new Food[2];
        for (int i = 0; i > GoblinInv.Length; i++)
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
                PlayerPrefs.SetString(SaveIndex + "_Element", GoblinInv[i].Element.ToString());
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
    //(Unused)
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
            MonsterElement element;
            if (PlayerPrefs.GetString(SaveIndex + "_Element").Equals(""))
                element = MonsterElement.None;
            else
                element = (MonsterElement)Enum.Parse(typeof(MonsterElement), PlayerPrefs.GetString(SaveIndex + "_Element"));

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
    //(Unused)
    public void ClearGoblinInv()
    {
        string inv = "Goblin Inventory Load: ";
        Food[] empty = new Food[2];
        string GoblinInvIndex = "GoblinInv_Slot";
        for (int i = 0; i < empty.Length; i++)
        {
            string SaveIndex = GoblinInvIndex + i + "_";
            if (empty[i] != null)
            {

                if (empty[i].FoodType != "None")
                {

                    PlayerPrefs.SetInt(SaveIndex + "_Power", empty[i].Power);
                    PlayerPrefs.SetString(SaveIndex + "_Type", empty[i].FoodType);
                    PlayerPrefs.SetString(SaveIndex + "_Element", empty[i].Element.ToString());
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
            
        }
        Debug.Log(inv);
    }

    //Saves the inventory
    /// <summary>
    /// Saves the input food array.
    /// </summary>
    /// <param name="inventory"></param>
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
                PlayerPrefs.SetString(SaveIndex + "_Element", inventory[i].Element.ToString());
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
    /// <summary>
    /// loads an array of the saved food
    /// </summary>
    /// <returns></returns>
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
            MonsterElement element;
            if (PlayerPrefs.GetString(SaveIndex + "_Element").Equals(""))
                element = MonsterElement.None;
            else
                element = (MonsterElement)Enum.Parse(typeof(MonsterElement), PlayerPrefs.GetString(SaveIndex + "_Element"));
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

    /*
    //(unused)
    /// <summary>
    /// get food at index
    /// </summary>
    /// <param name="Index"></param>
    /// <returns></returns>
    public Food LoadFood(int Index)
    {
        string InventorySlot = "InventorySlot_";
        string SaveIndex = InventorySlot + Index;
        int power = PlayerPrefs.GetInt(SaveIndex + "_Power");
        string type = PlayerPrefs.GetString(SaveIndex + "_Type");
        string element = PlayerPrefs.GetString(SaveIndex + "_Element");
        return new Food(type, element, power);
    }
    */

    //(unused)
    /// <summary>
    /// delete food at index 
    /// </summary>
    /// <param name="index"></param>
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