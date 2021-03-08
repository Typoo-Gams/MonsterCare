using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAbilty : MonoBehaviour
{
    GameManager manager;
    
    public Image monsterIcon;
    public float Cooldown;
    

    bool isCooldown = false;

    public int Damage;

    // Start is called before the first frame update
    void Start()
    {
        monsterIcon.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        OurMonsterAttack();
    }

    void OurMonsterAttack()
    {
            if (Input.touchCount > 1 && isCooldown == false || Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1) && isCooldown == false)
            {
                isCooldown = true;
                monsterIcon.fillAmount = 1;
                SpecialAttack();
            }

            if (isCooldown)
            {
                monsterIcon.fillAmount -= 1 / Cooldown * Time.deltaTime;

                if (monsterIcon.fillAmount <= 0)
                {
                    monsterIcon.fillAmount = 0;
                    isCooldown = false;
                }
            }
    }

    public void SpecialAttack()
    {
        manager.EnemyMonster.UpdateHealth(manager.EnemyMonster.HealthStatus - Damage);
    }
}
