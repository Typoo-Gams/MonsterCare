using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Previously loaded scene
    public int PreviousSecene;
    //GameSaver for saving info.
    GameSaver Save = new GameSaver();
    //Currently Active Monster
    public Monster ActiveMonster;
    public GameObject MonsterObject;
    //Currently Active Enemy Monster
    public GameObject Enemy;
    //slider for stuff
    public Slider SliderPrefab;


    //Called When this is destroyed.
    private void OnDestroy()
    {
        if (SceneManager.GetActiveScene().name == "MonsterHome_MainPrototype")
            Save.SaveTime();
    }


    //Called when a new scene is loaded.
    private void OnLevelWasLoaded(int level)
    {
        //Canvas canvas =  GameObject.Find("Canvas").GetComponent<Canvas>();
        //canvas.worldCamera = Camera.main;
        Debug.Log("previous scene: "+PreviousSecene);
        if (SceneManager.GetActiveScene().name == "MonsterHome_MainPrototype") 
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


    //-------------Properties--------------


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


    /// <summary>
    /// Save the floats and name of a monster
    /// </summary>
    /// <param name="yourMonster">Input a monster to be saved</param>
    public void SaveMonster(Monster yourMonster) 
    {
        string[] StatIndex = 
            {"Health", "Hunger", "Sleep", "Happiness", "Playfull", "Toughness"};
        float[] Stats =
            {yourMonster.HealthStatus, yourMonster.HungerStatus, yourMonster.SleepStatus, yourMonster.HappinessStatus, yourMonster.PlayfullStatus, yourMonster.ToughnessStatus};

        string MonsterSaveIndex = "SavedMonster_";

        PlayerPrefs.SetString(MonsterSaveIndex + "MonsterName", yourMonster.Name);
        PlayerPrefs.SetString(MonsterSaveIndex + "PrefabLocation", yourMonster.PrefabLocation);

        for (int i = 0; i < StatIndex.Length; i++)
        {
            PlayerPrefs.SetFloat(MonsterSaveIndex + StatIndex[i], Stats[i]);
        }
    }


    /// <summary>
    /// Loads the saved monster into the spesified monster
    /// </summary>
    /// <param name="yourMonster">Input a monster to load the stats into</param>
    public void LoadMonster(Monster yourMonster)
    {
        string[] StatIndex =
            {"Health", "Hunger", "Sleep", "Happiness", "Playfull", "Toughness"};

        string MonsterSaveIndex = "SavedMonster_";

        yourMonster.Name = PlayerPrefs.GetString(MonsterSaveIndex + "MonsterName");
        yourMonster.HealthStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[0]);
        yourMonster.HungerStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[1]);
        yourMonster.SleepStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[2]);
        yourMonster.HappinessStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[3]);
        yourMonster.PlayfullStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[4]);
        yourMonster.ToughnessStatus = PlayerPrefs.GetFloat(MonsterSaveIndex + StatIndex[5]);
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


public class Monster
{

    //Monster variables
    private string MonsterName;
    private float Health;
    private float Hunger;
    private float Sleep;
    private float Happiness;
    private float Playfull;
    private float Toughness;
    private float Energy;
    private bool CanEvolve;
    private string loadLocation;

    //Combat
    private float AbilityDmg = 10;

    //Status effect Bools
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

    //potentials
    /*
     * IsSick
     * IsPoisoned
     * 
     * */


    //Max & Min Values (Read Only for now)
    readonly float MaxHealth = 100;
    readonly float MaxHunger = 100;
    readonly float MaxSleep = 100;
    readonly float MaxHappiness = 100;


    //Degration modifiers
    readonly float HungerDegration = 0.0083f; //1% every _ min
    readonly float SleepDegration = 0.0083f; //1% every _ min
    readonly float PlayfullDegration = 0f; //1% every _ min
    readonly float ToughnessDegration = 0f; //1% every _ min


    //needed for shake (temporary?)
    float originX, originY, originZ;


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
    /// <param name="prefabLocation">The resource location to load this monster's prefab</param>
    public Monster(string name, string prefabLocation)
    {
        loadLocation = prefabLocation;
        MonsterName = name;
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
    public Monster(string name)
    {
        MonsterName = name;
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


    //Print All Statuses
    /// <summary>
    /// Prints out all monster variables in the console.
    /// </summary>
    public void DebugMonster()
    {
        Debug.Log(MonsterName + " Status: \nHealth: " + Health + "\nEnergy: " + Energy + "\nHunger: " + Hunger + "\nStarving: " + IsStarving + "\nFull: " + IsFull + "\nSleepyness: " + Sleep + "\nSleep Deprived: " + IsSleepDeprived + "\nRested: " + IsRested + "\nOver Rested: " + IsOverRested);
    }


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
        HealthBar.tag = "UsedHealthbar";
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
    public void UpdateHealth(float NewHealth)
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


    //------------Shake------------ (Temporary?)


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

    //Updates all the stat changes when the game opens after being closed.
    /// <summary>
    /// Updates every stat based on how many seconds have passed (Basically time travel)
    /// </summary>
    /// <param name="TimeInSec">Time in seconds</param>
    ///<param name="WasSleeping">If the monster was sleeping when game wakes up</param>
    public void AtGameWakeUp(float TimeInSec) 
    {
        float[] statuses = { Hunger, Sleep };
        float[] degrade = {HungerDegration, SleepDegration };

        for (int i = 0; i < statuses.Length; i++)
        {
            statuses[i] -= degrade[i] * TimeInSec;
            if (statuses[i] < 0)
                statuses[i] = 0;
        }
        UpdateHunger(statuses[0]);
        Sleep = statuses[1];
        UpdateSleeping(IsSleeping);
        Debug.Log("calculated");
    }


    //Update Hunger
    /// <summary>
    /// Updates the hunger to desired value and the statuses if conditions are met.
    /// </summary>
    /// <param name="NewHunger">Set new current hunger</param>
    public void UpdateHunger(float NewHunger)
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


    //Degrade Hunger
    /// <summary>
    /// Degrades hunger and updates the statuses if conditions are met.
    /// </summary>
    public void DegradeHunger()
    {
        Hunger -= HungerDegration;   

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
    /// Updates the sleep value by the degration value and the statuses if conditions are met.
    /// </summary>
    /// <param name="sleeping">New sleep state, Updates IsSleeping</param>
    /// <param name="modifier">Changes how much sleep is added</param>
    public void UpdateSleeping(bool sleeping, float modifier = 2)
    {
        if (sleeping)
            IsSleeping = true;
        else
            IsSleeping = false;

        if (IsSleeping)
        {
            //Adds sleep
            float gainedSleep = SleepDegration * modifier;
            Sleep += gainedSleep;
            if (Hunger < 25) 
            {
                Energy += gainedSleep * 0.5f;
            }
            if (Hunger > 75)
            {
                Energy += gainedSleep * 1.5f;
            }
            if (Hunger > 25 && Hunger < 75)
            {
                Energy += gainedSleep * 1;
            }
            if (Energy > 10)
                Energy = 10;

        }
        else 
        {
            if (modifier != 2)
                Sleep -= SleepDegration * modifier;
            else
                Sleep -= SleepDegration;
            if (Sleep < 0)
                Sleep = 0;
        }

        //Statusupdates
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


    //Update Happiness
    /// <summary>
    /// Updates the happiness value and the statuses if conditions are met.
    /// </summary>
    /// <param name="NewHappiness">Set new current happiness</param>
    public void UpdateHappiness(float NewHappiness)
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


    //Get loadLocation
    public string PrefabLocation 
    {
        get => loadLocation;
    }


    //Get/set Name
    /// <summary>
    /// Get the monster's assigned name
    /// </summary>
    public string Name
    {
        get => MonsterName;
        set => MonsterName = value;
    }


    //Gets the slider used for the healthbar
    /// <summary>
    /// Get the healthbar slider
    /// </summary>
    /// <returns>Health bar slider</returns>
    public Slider GetHealthbar() 
    {
        return HealthBar;
    }


    //Get/set health
    /// <summary>
    /// Get Health value.
    /// </summary>
    public float HealthStatus
    {
        get => Health;
        set => Health = value;
    }


    //get Hunger
    /// <summary>
    /// Get Hunger value.
    /// </summary>
    public float HungerStatus
    {
        get => Hunger;
        set => Hunger = value;
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


    //Get/set Sleep
    /// <summary>
    /// Get/Set Sleep Value.
    /// </summary>
    public float SleepStatus
    {
        get => Sleep;
        set => Sleep = value;
    }


    //Get/Set IsSleeping
    /// <summary>
    /// Get IsSleeping Value.
    /// </summary>
    public bool IsSleepingStatus
    {
        get => IsSleeping;
        set => IsSleeping = value;
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
    public float SleepMaxValue
    {
        get => MaxSleep;
    }


    //Set/get Happiness
    /// <summary>
    /// Get/Set Happiness value.
    /// </summary>
    public float HappinessStatus
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
    public float PlayfullStatus
    {
        get => Playfull;
        set => Playfull = value;
    }


    //Get/set Thoughness
    /// <summary>
    /// Get/Set Toughness value.
    /// </summary>
    public float ToughnessStatus
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

    //Get/Set Energy Status
    /// <summary>
    /// Get the Energy value.
    /// </summary>
    public float EnergyStatus 
    {
        get => Energy;
        set => Energy = value;
    }


    //Get/Set CanEvolve Status
    /// <summary>
    /// Get/Set the CanEvolve bool that triggers evolution when true.
    /// </summary>
    public bool CanEvolveStatus 
    {
        get => CanEvolve;
        set => CanEvolve = value;
    }
}