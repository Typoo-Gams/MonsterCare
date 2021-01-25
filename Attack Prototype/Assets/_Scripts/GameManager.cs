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
    
    //Monster variable statuses
    string MonsterName;
    private int Health; 
    private int Hunger;
    private int Sleep;
    private bool IsSleepDeprived;
    private bool IsMedicated;
    private bool IsInComat;
    private bool IsDead;

    //Max & Min Border readOnly's
    readonly int MaxHealth = 100;
    readonly int MaxHunger = 10;
    readonly int MaxSleep = 24;
    static float originX, originY, originZ;

    //Health Bar
    Slider HealthBar;

    //------------------Monster Class Constructor---------------------


    public Monster(string name) 
    {
        MonsterName = name;
        Health = MaxHealth;
        Hunger = 0;
        Sleep = 0;
        IsSleepDeprived = false;
        IsMedicated = false;
    }


    //------------------------Properties------------------------


    //------------Combat------------


    //Check Combat Status
    public bool CombatStatus
    {
        get => IsInComat;
    }


    //Updates The monsters Combat Status and health bar visibility
    public void CombatActive(bool state)
    {
        IsInComat = state;
        if (IsInComat)
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
        if (IsInComat)
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
        else if(NewHealth > MaxHealth)
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


    //Set/get Hunger
    public int HungerStatus
    {
        get => Hunger;
        set => Hunger = value;
    }


    //Set/get Sleep
    public int SleepStatus
    {
        get => Sleep;
        set => Sleep = value;
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


    //Print All Statuses
    public void DebugStatus() 
    {
        Debug.Log(MonsterName + " Status: \nHealth: " + Health + "\nHunger: " + Hunger + "\nSleepyness: " + Sleep + "\nMedicated: " + IsMedicated);
    }
}