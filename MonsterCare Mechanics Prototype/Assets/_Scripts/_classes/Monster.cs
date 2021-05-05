using UnityEngine;
using System;

/// <summary>
/// Type Of Monster. Decides what bonuses it has
/// </summary>
public enum MonsterType
{
    Playfull,
    Fighter,
    Sleepy,
    Hungry,
    Basic
}

public enum MonsterElement
{
    None,
    Air,
    Fire,
    Earth,
    Water
}

public class Monster
{

    //UpdateSpeed
    readonly float UpdateSpeed = 0.1f;

    //Monster variables
    private string MonsterName;
    private string EvolvedFrom;
    private float Health;
    private float HealthRegen;
    private float Hunger;
    private float Sleep;
    private float Happiness;
    private float Playfull;
    private float Toughness;
    private float Energy;
    private bool CanEvolve;
    private MonsterType _PersonalityType;
    private MonsterElement elementEaten;

    //Other
    private GameObject MonsterReport;
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
    private bool Evolved;

    //potentials
    /*
     * IsSick
     * IsPoisoned
     * 
     * */


    //Max & Min Values
    private float MaxHealth = 100;
    private float MaxHunger = 100;
    private float MaxSleep = 100;
    private float MaxHappiness = 100;
    private float MaxEnergy = 10;
    private float EvolveCost = 10;


    //Monster Degradation Modifiers
    private float HungerDegration = 0.083f; //1% every _ min
    private float SleepDegration = 0.083f; //1% every _ min
    private float HappinessDegration = 0.083f;

//-----------------Personality Degradation Values----------------
/**/

    //Fighter
    readonly float Fighter_MaxHealth = 200;
    readonly float Fighter_HungerDegration = 0.093f;

    //Hungry
    readonly float Hungry_MaxHunger = 200;
    readonly float Hungry_HungerDegration = 0.073f;
    //more passive health regen?

    //Sleepy
    readonly float Sleepy_MaxSleep = 200;
    readonly float Sleepy_MaxEnergy = 20;
    //needs more energy to evolve

    //Playfull
    readonly float Playfull_MaxHappiness = 200;
    readonly float Playfull_HappinessDegration = 0.073f;

/**/

    //Health Bar
    GameObject HealthBar;


    //------------------Monster Class Constructor---------------------


    //Constructor for no arguments
    /// <summary>
    /// Create a new monster Object with default values. (testing purposes)
    /// </summary>
    public Monster()
    {
        MonsterName = "Default";
        elementEaten = MonsterElement.None;
        Health = MaxHealth;
        HealthRegen = 0.01f;
        Energy = 10;
        Hunger = 100;
        Sleep = 100;
        Happiness = 100;
        Playfull = 0;
        Toughness = 1;
        IsSleepDeprived = false;
        IsMedicated = false;
        UpdateHunger(Hunger);
        PreviousEvolution = "";
    }

    /// <summary>
    /// Constructor for friendly monsters
    /// </summary>
    /// <param name="name">Monsters name (currently not visable to player).</param>
    /// <param name="prefabLocation">Where the monster is being loaded from.</param>
    /// <param name="type">Type of monster. changes its behavior.</param>
    /// <param name="IsEvolution">If this monster just evolved.</param>
    public Monster(string name, string prefabLocation, MonsterType type = MonsterType.Basic, bool IsEvolution = false)
    {
        _PersonalityType = type;

        switch (_PersonalityType)
        {
            case MonsterType.Basic:
                //no changes
                break;

            case MonsterType.Fighter:
                MaxHealth = Fighter_MaxHealth;
                HungerDegration = Fighter_HungerDegration;
                break;

            case MonsterType.Hungry:
                MaxHunger = Hungry_MaxHunger;
                HungerDegration = Hungry_HungerDegration;
                break;

            case MonsterType.Playfull:
                MaxHappiness = Playfull_MaxHappiness;
                HappinessDegration = Playfull_HappinessDegration;
                break;

            case MonsterType.Sleepy:
                MaxSleep = Sleepy_MaxSleep;
                MaxEnergy = Sleepy_MaxEnergy;
                break;
        }

        Evolved = IsEvolution;
        loadLocation = prefabLocation;
        MonsterName = name;
        elementEaten = MonsterElement.None;
        Health = MaxHealth;
        HealthRegen = 0.01f;
        Energy = 10;
        Hunger = 100;
        Sleep = 100;
        Happiness = 100;
        Playfull = 0;
        Toughness = 1;
        IsSleepDeprived = false;
        IsMedicated = false;
        UpdateHunger(Hunger);
        PreviousEvolution = "";
    }


    /// <summary>
    /// Create a Monster Object instantiated with Name
    /// </summary>
    /// <param name="name">The name of the new monster</param>
    public Monster(string name)
    {
        MonsterName = name;
        elementEaten = MonsterElement.None;
        Health = MaxHealth;
        HealthRegen = 0.01f;
        Energy = 10;
        Hunger = 100;
        Sleep = 100;
        Happiness = 100;
        Playfull = 0;
        Toughness = 1;
        IsSleepDeprived = false;
        IsMedicated = false;
        UpdateHunger(Hunger);
        PreviousEvolution = "";
    }

    //------------------------Properties------------------------


    /// <summary>
    /// Sets the monster report that shows when the player evolves a new monster
    /// </summary>
    /// <param name="report">report prefab</param>
    public void SetReport(GameObject report)
    {
        MonsterReport = report;
    }



    /// <summary>
    /// Gets the monster report that shows when the player evolves a new monster
    /// </summary>
    /// <returns>report prefab</returns>
    public GameObject GetReport()
    {
        return MonsterReport;
    }


    //Print All Statuses
    /// <summary>
    /// Prints out all monster variables in the console.
    /// </summary>
    public void DebugMonster()
    {
        Debug.Log(MonsterName + " Status: \nHealth: " + Health + "\nEnergy: " + Energy + "\nHunger: " + Hunger + "\nHappiness: " + HappinessStatus + "\nStarving: " + IsStarving + "\nFull: " + IsFull + "\nSleepyness: " + Sleep + "\nSleep Deprived: " + IsSleepDeprived + "\nRested: " + IsRested + "\nOver Rested: " + IsOverRested + "\nIsDead: " + IsDead + "\nCanEvolve: " + CanEvolveStatus);
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
                //Debug.LogError("The Monster Must have a health bar to activate combat");
            }
            else
            {
                HealthBar.SetActive(true);
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
                HealthBar.SetActive(false);
            }
        }
    }


    //Sets desired healthbar to the monster
    /// <summary>
    /// Assigns a healthbar prefab as the mosnters healthbar.
    /// </summary>
    /// <param name="Slider">Spesified slider for the monster's healthbar</param>
    public void AssignHealthBar(GameObject GivenHealthBar)
    {
        HealthBar = GivenHealthBar;
        HealthBar.tag = "UsedHealthbar";
        HealthBar.GetComponent<HealthBarController>().ThisMonster = this;
    }


    //Deal DMG to monster
    /// <summary>
    /// Deals damage to the monster's health equal to int.
    /// </summary>
    /// <param name="dmg">ammount of damage being dealt</param>
    public void DealDmg(float dmg)
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
        if (NewHealth <= MaxHealth && NewHealth > -1)
        {
            Health = NewHealth;
        }
        else if (NewHealth > MaxHealth)
        {
            Health = MaxHealth;
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

    //----------- Updates------------

    //Updates all the stat changes when the game opens after being closed.
    /// <summary>
    /// Updates every stat based on how many seconds have passed (Basically time travel)
    /// </summary>
    /// <param name="TimeInSec">Time in seconds</param>
    ///<param name="WasSleeping">If the monster was sleeping when game wakes up</param>
    public void AtGameWakeUp(double TimeInSec)
    {
        if (TimeInSec > 0)
        {
            for (int j = 0; j < TimeInSec; j++)
            {
                UpdateHunger(Hunger - HungerDegration);
                UpdateSleeping(IsSleeping);
                UpdateHappiness();
            }
        }
        else
        {
            Debug.LogError("AtGameWakeUp: input was outside of bounds: " + TimeInSec);
        }
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

        if (IsStarving)
        {
            UpdateHealth(HealthStatus - HealthRegen);
        }

        if (Hunger > 50)
            UpdateHealth(HealthStatus + HealthRegen);
    }


    //Degrade Hunger
    /// <summary>
    /// Degrades hunger and updates the statuses if conditions are met.
    /// </summary>
    public void DegradeHunger()
    {
        Hunger -= HungerDegration;
        UpdateHunger(Hunger);
    }


    //Update SleepStatus
    /// <summary>
    /// Updates the sleep value by the degration value and the statuses if conditions are met.
    /// </summary>
    /// <param name="sleeping">New sleep state, Updates IsSleeping</param>
    /// <param name="modifier">Changes how much sleep is added</param>
    public void UpdateSleeping(bool sleeping, float modifier = 1)
    {
        float SleepHungry = 0.95f;
        float SleepMedium = 1;
        float SleepFull = 1.05f;

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
                Energy += gainedSleep * SleepHungry;
                Debug.Log(gainedSleep * SleepHungry);
            }
            else if (Hunger > 75)
            {
                Energy += gainedSleep * SleepFull;
                Debug.Log(gainedSleep * SleepFull);
            }
            else if (Hunger > 25 && Hunger < 75)
            {
                Energy += gainedSleep * SleepMedium;
                Debug.Log(gainedSleep * SleepMedium);
            }
            if (Energy > MaxEnergy)
                Energy = MaxEnergy;

            if (_PersonalityType.Equals(MonsterType.Sleepy))
            {
                Happiness += HappinessDegration;
            }

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
    public void UpdateHappiness()
    {

        if (!(IsSleeping && _PersonalityType.Equals(MonsterType.Sleepy))) 
        {
            Happiness -= HappinessDegration;
            Debug.LogWarning("Happiness degredationdasjdskaj");
        }


        if (Happiness > 75)
        {
            HungerDegration = 0.083f * 0.9f;
            SleepDegration = 0.083f * 0.9f;
        }
        if (Happiness < 75 && Happiness > 25)
        {
            HungerDegration = 0.083f;
            SleepDegration = 0.083f;
        }
        if (Happiness < 25)
        {
            HungerDegration = 0.083f * 1.1f;
            SleepDegration = 0.083f * 1.1f;
        }
    }


    /// <summary>
    /// Monster gains an ammount of happiness
    /// </summary>
    /// <param name="Ammount"></param>
    public void AddHappiness(float Ammount)
    {
        Happiness += Ammount;
    }


    //------------Get/Set------------


    //Get loadLocation
    public string PrefabLocation
    {
        get => loadLocation;
    }

    /// <summary>
    /// Set get the previous evolution
    /// </summary>
    public string PreviousEvolution
    {
        get => EvolvedFrom;
        set => EvolvedFrom = value;
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
    /// Get the healthbar GameObject
    /// </summary>
    /// <returns>Health bar slider</returns>
    public GameObject GetHealthbar()
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
    public float GetMaxSleep
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
        set => IsDead = value;
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

    //Used for enemy toughness
    public float ToughnessModifier
    {
        get => Toughness;
        set => Toughness = value;
    }

    /// <summary>
    /// Set/Get monster's current element
    /// </summary>
    public MonsterElement Element 
    {
        get => elementEaten;
        set => elementEaten = value;
    }

    public float GetMaxHealth 
    {
        get => MaxHealth;
    }

    public float GetMaxHunger
    {
        get => MaxHunger;
    }

    public float GetMaxEnergy
    {
        get => MaxEnergy;
    }

    public float GetMaxHappiness
    {
        get => MaxHappiness;
    }

    public float GetEvolveCost
    {
        get => EvolveCost;
    }

    public MonsterType Personality
    {
        get => _PersonalityType;
    }

}