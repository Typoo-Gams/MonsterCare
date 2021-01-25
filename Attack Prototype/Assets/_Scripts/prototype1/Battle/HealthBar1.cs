using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar1 : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Enemy playerHealth;

    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        slider = GetComponent<Slider>();
        slider.maxValue = playerHealth.maxHealth;
        slider.value = playerHealth.maxHealth;
    }

    public void SetHealth(int hp)
    {
        slider.value = hp;
    }
}
