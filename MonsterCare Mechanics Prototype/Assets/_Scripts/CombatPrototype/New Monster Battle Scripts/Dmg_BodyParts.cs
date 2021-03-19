using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dmg_BodyParts : MonoBehaviour
{
    GameManager manager;

    bool currentlyAttacking;
    public int DmgModifier;
    float Counter;
    bool tapped;

    private void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        currentlyAttacking = manager.EnemyMonster.CombatStatus;
        manager.EnemyMonster.SetOriginPos(transform);
    }
    private void Update()
    {
        if (tapped)
        {
            Counter += Time.deltaTime;
        }

        if(Counter > 0.35f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            Counter = 0;
            tapped = false;
        }
    }

    private void OnMouseDown()
    {
        tapped = true;

        if (currentlyAttacking == true)
        {
            manager.EnemyMonster.DealDmg(1 * DmgModifier);
            FindObjectOfType<SoundManager>().play("SwordSwing");
        }
        
        if(manager.ActiveMonster.HealthStatus == 0)
        {
            currentlyAttacking = false;
        }

        if(DmgModifier == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.92f, 0.016f, 1);
        }
        if(DmgModifier == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0, 1);
        }
        if(DmgModifier == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
    }
}
