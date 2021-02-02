using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Slider SliderPrefab;
    private Canvas CurrentCanvas;


    // Start is called before the first frame update
    void Start()
    {
        CurrentCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class Monster
{

    //Monster variables
    string MonsterName;
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


    public Monster()
    {
        MonsterName = "Default";
        Health = MaxHealth;
        Hunger = 0;
        Sleep = 0;
        IsSleepDeprived = false;
        IsMedicated = false;
        UpdateHunger(0);
    }


    public Monster(string name)
    {
        MonsterName = name;
        Health = MaxHealth;
        Hunger = 0;
        Sleep = 0;
        IsSleepDeprived = false;
        IsMedicated = false;
        UpdateHunger(0);
    }


    //------------------------Properties------------------------


    //------------Combat------------


    //Check Combat Status
    public bool CombatStatus
    {
        get => IsInCombat;
    }


    //Updates The monsters Combat Status and health bar visibility
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
    public void AssignHealthBar(Slider Slider)
    {
        HealthBar = Slider;
        HealthBar.maxValue = MaxHealth;
        HealthBar.minValue = 0;
        HealthBar.value = Health;
    }


    //Deal DMG to monster
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
    public Vector3 Shake()
    {

        float speed = 75.0f; //how fast it shakes
        float amount = 0.25f; //how much it shakes

        float x = Mathf.Sin(Time.time * speed) * amount;


        return new Vector3(x, originY, originZ);
    }


    //Sets origin position. required to reset position after shake -> would be ideal to use an animation instead
    public void SetOriginPos(Transform originalPos)
    {
        originX = originalPos.position.x;
        originY = originalPos.position.y;
        originZ = originalPos.position.z;
    }


    //get original position
    public Vector3 GetOriginPos()
    {
        return new Vector3(originX, originY, originZ);
    }


    //------------Get/Set------------


    //Set/get health
    public int HealthStatus
    {
        get => Health;
        set => Health = value;
    }


    //get Hunger
    public int HungerStatus
    {
        get => Hunger;
    }


    //Get isStarving
    public bool IStarvingStatus 
    {
        get => IsStarving;
    }


    //Get isStarving
    public bool IsFullStatus
    {
        get => IsFull;
    }


    //Update Hunger
    public void UpdateHunger(int NewHunger) 
    {
        Hunger = NewHunger;
        switch (Hunger) 
        {
            case 10:
                if (Hunger > MaxHunger) 
                    Hunger = MaxHunger;
                IsFull = true;
                break;
            case 0:
                if (Hunger < 0) 
                    Hunger = 0;
                IsStarving = true;
                break;
            default:
                if (Hunger > 0)
                    IsStarving = false;
                if (Hunger < MaxHunger)
                    IsFull = false;
                break;
        }
    }


    //Set/get Sleep
    public int SleepStatus
    {
        get => Sleep;
        set => Sleep = value;
    }


    //Get IsSleeping
    public bool IsSleepingStatus
    {
        get => IsSleeping;
    }


    //Update SleepStatus
    public void UpdateSleep(bool sleeping) 
    {
        if (sleeping)
            IsSleeping = true;
        else
            IsSleeping = false;

        if (IsSleeping)
        {
            Sleep++;
            if (Sleep > 24)
                Sleep = 24;
            switch (Sleep)
            {
                case 24:
                    if(IsRested)
                        IsOverRested = true;
                    IsRested = true;
                    Sleep = MaxSleep;
                    break;
                case 0:
                    IsSleepDeprived = true;
                    Sleep = 0;
                    break;
                case 5:
                    IsSleepDeprived = false;
                    break;
                case 16:
                    IsRested = false;
                    break;
            }
        }
    }


    //Get IsSleepDeprived
    public bool IsSleepDeprivedStatus
    {
        get => IsSleepDeprived;
    }


    //Get IsRested
    public bool IsRestedStatus
    {
        get => IsRested;
    }


    //Get IsOverRested
    public bool IsOverRestedStatus
    {
        get => IsOverRested;
    }


    //Get MaxSleep
    public int SleepMaxBorder
    {
        get => MaxSleep;
    }


    //Set/get Happiness
    public int HappinessStatus
    {
        get => Happiness;
        set => Happiness = value;
    }


    //Get IsSad
    public bool IsSadStatus
    {
        get => IsSad;
    }


    //Get IsHappy
    public bool IsHappyStatus
    {
        get => IsHappy;
    }

    //Update Happiness
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


    //Get/set Thoughness
    public int ThoughnessStatus
    {
        get => Thoughness;
        set => Thoughness = value;
    }


    //Get/Set PLayfullness
    public int PlayfullStatus
    {
        get => Playfull;
        set => Playfull = value;
    }


    //Set/get Medicine
    public bool MedicineStatus
    {
        get => IsMedicated;
        set => IsMedicated = value;
    }


    //Get Death Status
    public bool DeathStatus 
    {
        get => IsDead;
    }


    //Get Name
    public string Name
    {
        get => MonsterName;
    }


    //Print All Statuses
    public void DebugStatus() 
    {
        Debug.Log(MonsterName + " Status: \nHealth: " + Health + "\nHunger: " + Hunger + "\nSleepyness: " + Sleep + "\nMedicated: " + IsMedicated);
    }
}