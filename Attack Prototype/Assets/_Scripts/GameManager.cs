using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    //Monster Status variables
    string MonsterName;
    private int Health; 
    private int Hunger;
    private int Sleep;
    private bool IsSleepDeprived;
    private bool IsMedicated;
    private bool IsInComat;

    //Max & Min Border readOnly's
    readonly int MaxHealth = 100;
    readonly int MaxHunger = 10;
    readonly int MaxSleep = 24;
    static float originX, originY, originZ;

    //Health Bar
    Slider HealthBar;

    //Monster Class Constructor
    public Monster(string name) 
    {
        MonsterName = name;
        Health = MaxHealth;
        Hunger = 0;
        Sleep = 0;
        IsSleepDeprived = false;
        IsMedicated = false;
    }

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
    
    public Vector3 Shake() 
    {

        float speed = 75.0f; //how fast it shakes
        float amount = 0.25f; //how much it shakes

        float x = Mathf.Sin(Time.time * speed) * amount;
        

        return new Vector3(x, originY, originZ);
    }

    public void SetOriginPos(Transform originalPos) 
    {
        originX = originalPos.position.x;
        originY = originalPos.position.y;
        originZ = originalPos.position.z;
    }

    public Vector3 resetPosition() 
    {
        return new Vector3(originX, originY, originZ);
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

    //Update Health
    public void UpdateHealth(int NewHealth) 
    {
        if (NewHealth < MaxHealth && NewHealth > -1)
        {
            Health = NewHealth;
            HealthBar.value = Health;
        }
        else 
        {
            Debug.LogError("Cant Assign Health Outside of peremeters: " + 0 + "-" + MaxHealth);
        }
    }

    //Check Combat Status
    public bool CombatStatus
    {
        get => IsInComat;
    }

    //Set Health
    public int HealthStatus
    {
        get => Health;
        set => Health = value;
    }

    //Set Hunger
    public int HungerStatus
    {
        get => Hunger;
        set => Hunger = value;
    }

    //Set Sleep
    public int SleepStatus
    {
        get => Sleep;
        set => Sleep = value;
    }

    //Set Medicine
    public bool MedicineStatus
    {
        get => IsMedicated;
        set => IsMedicated = value;
    }

    //Print All Statuses
    public void DebugStatus() 
    {
        Debug.Log(MonsterName + " Status: \nHealth: " + Health + "\nHunger: " + Hunger + "\nSleepyness: " + Sleep + "\nMedicated: " + IsMedicated);
    }
}