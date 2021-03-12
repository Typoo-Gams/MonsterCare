using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAbilty : MonoBehaviour
{
    GameManager manager;
    
    public Image monsterIconFill, monsterIcon;
    public float Cooldown;
    

    bool isCooldown = false;

    public int Damage;
    int elementDmg = 2;
    string element;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        monsterIconFill.sprite = manager.MonsterObject.GetComponent<SpriteRenderer>().sprite;
        monsterIcon.sprite = manager.MonsterObject.GetComponent<SpriteRenderer>().sprite;

        monsterIconFill.fillAmount = 0;

        //Checks the last eaten special food
        element = manager.ActiveMonster.Element;
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
                monsterIconFill.fillAmount = 1;
                SpecialAttack();
            }

            if (isCooldown)
            {
                monsterIconFill.fillAmount -= 1 / Cooldown * Time.deltaTime;

                if (monsterIconFill.fillAmount <= 0)
                {
                    monsterIconFill.fillAmount = 0;
                    isCooldown = false;
                }
            }
    }

    public void SpecialAttack()
    {
        if (manager.ActiveMonster.Element == "Water" && manager.EnemyMonster.Element == "Fire")
        {
            manager.EnemyMonster.DealDmg(Damage * elementDmg);
        }

        if (manager.ActiveMonster.Element == "Earth" && manager.EnemyMonster.Element == "Water")
        {
            manager.EnemyMonster.DealDmg(Damage * elementDmg);
        }

        if (manager.ActiveMonster.Element == "Air" && manager.EnemyMonster.Element == "Earth")
        {
            manager.EnemyMonster.DealDmg(Damage * elementDmg);
        }

        if (manager.ActiveMonster.Element == "Fire" && manager.EnemyMonster.Element == "Air")
        {
            manager.EnemyMonster.DealDmg(Damage * elementDmg);
        }

        if(manager.ActiveMonster.Element == "None")
        {
            manager.EnemyMonster.DealDmg(Damage);
        }
    }
}
