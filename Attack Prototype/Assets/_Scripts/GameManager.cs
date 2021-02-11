using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Slider SliderPrefab;
    public DateTime Time = DateTime.Now;

    public int testValue;

    private void Awake()
    {
        LoadTime();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        SaveTime();
    }

    //------------------------Save Time in PlayerPrefs---------------------//


    /// <summary>
    /// Saves current computer time to a save file.
    /// </summary>
    public void SaveTime() 
    {
        string[] TimeIndex =
            {"Hour", "Minutes", "Day", "Month", "Year"};
        float[] TimeTable =
            {Time.Hour, Time.Minute, Time.Day, Time.Month, Time.Year};
        for (int i = 0; i < TimeIndex.Length; i++) 
        {
            string FullIndex = "SavedTime_" + TimeIndex[i];
            PlayerPrefs.SetFloat(FullIndex , TimeTable[i]);
        }
    }


    /// <summary>
    /// Loads the saved floats from the saved time as an array.
    /// </summary>
    public float[] LoadTime() 
    {
        string[] TimeIndex =
            {"Hour", "Minutes", "Day", "Month", "Year"};
        float[] TimeTable = new float[5];
        for( int i = 0; i < TimeIndex.Length; i++) 
        {
            string fullIndex = "SavedTime_" + TimeIndex[i];
            TimeTable[i] = PlayerPrefs.GetFloat(fullIndex);
        }

        //debug
        string TimeSaved = TimeTable[0] + ":" + TimeTable[1] + "   " + TimeTable[2] + "/" + TimeTable[3] + "/" + TimeTable[4];
        Debug.Log(TimeSaved);

        //return
        return TimeTable;
    }


    /// <summary>
    /// Loads spesified value from saved time in playerprefs
    /// </summary>
    /// <param name="Name">Names: "Hour", "Minutes", "Day", "Month", "Year"</param>
    /// <returns></returns>
    public float LoadTime(string Name) 
    {
        string fullIndex = "SavedTime_" + Name;
        return PlayerPrefs.GetFloat(fullIndex);
    }


    /// <summary>
    /// Loads spesified value from saved time in playerprefs
    /// </summary>
    /// <param name="Index">indexes: Hour - 0, Minutes - 1, Day - 2, Month - 3, Year - 4</param>
    /// <returns></returns>
    public float LoadTime(int Index) 
    {
        string[] TimeIndex =
            {"Hour", "Minutes", "Day", "Month", "Year"};
        string fullIndex = "SavedTime_" + TimeIndex[Index];
        return PlayerPrefs.GetFloat(fullIndex);
    }
}

public class Monster
{

    //Monster variables
    private string MonsterName;
    private int MonsterId;
    private int Health;
    private int Hunger;
    private int Sleep;
    private int Happiness;
    private int Playfull;
    private int Toughness;


    //Status Bools
    private bool IsMedicated;
    private bool IsInCombat;
    private bool IsDead;
    private bool IsStarving;
    private bool IsFull;
    private bool IsSleeping;
    private bool IsSleepDeprived;
    private bool IsRested;
    private bool IsHappy;
    private bool IsSad;
    private bool IsOverRested;


    //Max & Min Border readOnly's
    readonly int MaxHealth = 100;
    readonly int MaxHunger = 10;
    readonly int MaxSleep = 24;
    readonly int MaxHappiness = 10;
    static float originX, originY, originZ;

    //Health Bar
    Slider HealthBar;

    //------------------Monster Class Constructor---------------------

    //Constructor for no arguments
    /// <summary>
    /// Create a new monster Object with default values.
    /// </summary>
    public Monster()
    {
        MonsterName = "Default";
        MonsterId = 0;
        Health = MaxHealth;
        Hunger = 0;
        Sleep = 0;
        Happiness = 0;
        Playfull = 0;
        Toughness = 0;
        IsSleepDeprived = false;
        IsMedicated = false;
        UpdateHunger(0);
    }

    /// <summary>
    /// Create a Monster Object instantiated with Name And ID
    /// </summary>
    /// <param name="name">The name of the new monster</param>
    /// <param name="ID">The id of the new monster</param>
    public Monster(string name, int ID)
    {
        MonsterName = name;
        MonsterId = ID;
        Health = MaxHealth;
        Hunger = 0;
        Sleep = 0;
        Happiness = 0;
        Playfull = 0;
        Toughness = 0;
        IsSleepDeprived = false;
        IsMedicated = false;
        UpdateHunger(0);
    }


    //------------------------Properties------------------------


    //------------Combat------------


    //Check Combat Status
    /// <summary>
    /// Returns bool IsInCombat.
    /// </summary>
    public bool CombatStatus
    {
        get => IsInCombat;
    }


    //Updates The monsters Combat Status and health bar visibility
    /// <summary>
    /// Updates the combat state. hides/shows healthbar
    /// </summary>
    /// <param name="state">new combat state</param>
    public void CombatActive(bool state)
    {
        IsInCombat = state;
        if (IsInCombat)
        {
            if (HealthBar == null)
            {
                Debug.LogError("The Monster Must have a health bar to activate combat");
            }
            else
            {
                HealthBar.gameObject.SetActive(true);
            }
        }
        else
        {
            if (HealthBar == null)
            {
                Debug.LogError("The Monster Must have a health bar to activate combat");
            }
            else
            {
                HealthBar.gameObject.SetActive(false);
            }
        }
    }


    //Sets desired healthbar to the monster
    /// <summary>
    /// Assigns a slider as a healthbar.
    /// </summary>
    /// <param name="Slider">Spesified slider for the monster's healthbar</param>
    public void AssignHealthBar(Slider Slider)
    {
        HealthBar = Slider;
        HealthBar.maxValue = MaxHealth;
        HealthBar.minValue = 0;
        HealthBar.value = Health;
    }


    //Deal DMG to monster
    /// <summary>
    /// Deals damage to the monster's health equal to int.
    /// </summary>
    /// <param name="dmg">ammount of damage being dealt</param>
    public void DealDmg(int dmg)
    {
        if (IsInCombat)
        {
            Health -= dmg;
            UpdateHealth(Health);
        }
        else
        {
            Debug.LogError("Monster is not in combat");

        }

    }


    //sets new health and updates health bar
    /// <summary>
    /// Sets a new Health value and updates the healthbar slider
    /// </summary>
    /// <param name="NewHealth">Set new current Health</param>
    public void UpdateHealth(int NewHealth)
    {
        if (NewHealth < MaxHealth && NewHealth > -1)
        {
            Health = NewHealth;
            HealthBar.value = Health;
        }
        else if (NewHealth > MaxHealth)
        {
            Health = MaxHealth;
            HealthBar.value = Health;
        }
        if (Health <= 0)
        {
            IsDead = true;
            Health = 0;
        }
        else if (Health >= 1)
        {
            IsDead = false;
        }
    }


    //------------Shake------------


    //Shakes the monster
    /// <summary>
    /// returns a new vector3 with a shake applied
    /// </summary>
    /// <returns></returns>
    public Vector3 Shake()
    {

        float speed = 75.0f; //how fast it shakes
        float amount = 0.25f; //how much it shakes

        float x = Mathf.Sin(Time.time * speed) * amount;


        return new Vector3(x, originY, originZ);
    }


    //Sets origin position. required to reset position after shake -> would be ideal to use an animation instead
    /// <summary>
    /// Saves the current position as original position.
    /// </summary>
    /// <param name="originalPos">The origin of the monster</param>
    public void SetOriginPos(Transform originalPos)
    {
        originX = originalPos.position.x;
        originY = originalPos.position.y;
        originZ = originalPos.position.z;
    }


    //get original position
    /// <summary>
    /// returns the position set by "SetOriginPos"
    /// </summary>
    /// <returns>position set by "SetOriginPos". if null then returns Vector3(0, 0, 0) </returns>
    public Vector3 GetOriginPos()
    {
        return new Vector3(originX, originY, originZ);
    }


    //----------- Updates------------

    /// <summary>
    /// Updates the hunger value and the statuses if conditions are met.
    /// </summary>
    /// <param name="NewHunger">Set new current hunger</param>
    //Update Hunger
    public void UpdateHunger(int NewHunger)
    {
        Hunger = NewHunger;

        if (Hunger > MaxHunger)
        {
            Hunger = MaxHunger;
            IsFull = true;
        }
        if (Hunger < 0)
        {
            IsStarving = true;
            Hunger = 0;
        }
        if (Hunger > 0)
        {
            IsStarving = false;
        }
        if (Hunger < MaxHunger)
        {
            IsFull = false;
        }
    }


    //Update SleepStatus
    /// <summary>
    /// Updates the sleep value and the statuses if conditions are met.
    /// </summary>
    /// <param name="sleeping">Set new current sleep</param>
    public void UpdateSleep(bool sleeping)
    {
        if (sleeping)
            IsSleeping = true;
        else
            IsSleeping = false;

        if (IsSleeping)
        {
            Sleep++;
            if (Sleep >= MaxSleep)
            {
                if (IsRested)
                    IsOverRested = true;
                IsRested = true;
                Sleep = MaxSleep;
            }
            if (Sleep <= 0)
            {
                Sleep = 0;
                IsSleepDeprived = true;
            }
            if (Sleep > 0)
            {
                IsSleepDeprived = false;
            }
            if (Sleep < MaxSleep)
            {
                IsRested = false;
            }
        }
    }


    //Update Happiness
    /// <summary>
    /// Updates the happiness value and the statuses if conditions are met.
    /// </summary>
    /// <param name="NewHappiness">Set new current happiness</param>
    public void UpdateHappiness(int NewHappiness)
    {
        Happiness = NewHappiness;
        if (Happiness <= 0)
        {
            IsHappy = false;
            Happiness = 0;
        }
        if (Happiness >= MaxHappiness)
        {
            IsHappy = true;
            Happiness = MaxHappiness;
        }
    }


    //------------Get/Set------------


    //Get Name
    /// <summary>
    /// Get the monster's assigned name
    /// </summary>
    public string Name
    {
        get => MonsterName;
    }

    //Get Name
    /// <summary>
    /// Get the monster's assigned ID
    /// </summary>
    public int ID
    {
        get => MonsterId;
    }


    //Print All Statuses
    /// <summary>
    /// Prints out all monster variables in the console.
    /// </summary>
    public void DebugStatus()
    {
        Debug.Log(MonsterName + " Status: \nHealth: " + Health + "\nHunger: " + Hunger + "\nSleepyness: " + Sleep + "\nMedicated: " + IsMedicated);
    }


    //Get health
    /// <summary>
    /// Get Health value.
    /// </summary>
    public int HealthStatus
    {
        get => Health;
    }


    //get Hunger
    /// <summary>
    /// Get Hunger value.
    /// </summary>
    public int HungerStatus
    {
        get => Hunger;
    }


    //Get isStarving
    /// <summary>
    /// Get IsStarving Value.
    /// </summary>
    public bool IStarvingStatus 
    {
        get => IsStarving;
    }


    //Get isStarving
    /// <summary>
    /// Get IsFull Value.
    /// </summary>
    public bool IsFullStatus
    {
        get => IsFull;
    }


    //Set/get Sleep
    /// <summary>
    /// Get/Set Sleep Value.
    /// </summary>
    public int SleepStatus
    {
        get => Sleep;
        set => Sleep = value;
    }


    //Get IsSleeping
    /// <summary>
    /// Get IsSleeping Value.
    /// </summary>
    public bool IsSleepingStatus
    {
        get => IsSleeping;
    }


    //Get IsSleepDeprived
    /// <summary>
    /// Get IsSleepDeprived Value.
    /// </summary>
    public bool IsSleepDeprivedStatus
    {
        get => IsSleepDeprived;
    }


    //Get IsRested
    /// <summary>
    /// Get IsRested Value.
    /// </summary>
    public bool IsRestedStatus
    {
        get => IsRested;
    }


    //Get IsOverRested
    /// <summary>
    /// Get IsOverRested value.
    /// </summary>
    public bool IsOverRestedStatus
    {
        get => IsOverRested;
    }


    //Get MaxSleep
    /// <summary>
    /// Get MaxSleep Value.
    /// </summary>
    public int SleepMaxValue
    {
        get => MaxSleep;
    }


    //Set/get Happiness
    /// <summary>
    /// Get/Set Happiness value.
    /// </summary>
    public int HappinessStatus
    {
        get => Happiness;
        set => Happiness = value;
    }


    //Get IsSad
    /// <summary>
    /// Get IsSad value.
    /// </summary>
    public bool IsSadStatus
    {
        get => IsSad;
    }


    //Get IsHappy
    /// <summary>
    /// Get IsHapyy Value.
    /// </summary>
    public bool IsHappyStatus
    {
        get => IsHappy;
    }


    //Get/Set PLayfullness
    /// <summary>
    /// Get/Set Platfull value.
    /// </summary>
    public int PlayfullStatus
    {
        get => Playfull;
        set => Playfull = value;
    }


    //Get/set Thoughness
    /// <summary>
    /// Get/Set Toughness value.
    /// </summary>
    public int ThoughnessStatus
    {
        get => Toughness;
        set => Toughness = value;
    }


    //Set/get Medicine
    /// <summary>
    /// Get/Set IsMedicated value.
    /// </summary>
    public bool MedicineStatus
    {
        get => IsMedicated;
        set => IsMedicated = value;
    }


    //Get Death Status
    /// <summary>
    /// Get IsDead value.
    /// </summary>
    public bool DeathStatus 
    {
        get => IsDead;
    }
}