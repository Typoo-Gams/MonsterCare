using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dmg_BodyParts : MonoBehaviour
{
    GameManager manager;
    GameObject Overlay;

    bool currentlyAttacking;
    public float DmgModifier;
    float Counter;
    bool tapped;

    private void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        currentlyAttacking = manager.EnemyMonster.CombatStatus;
        manager.EnemyMonster.SetOriginPos(transform);

        Overlay = gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject;

        Debug.Log(Overlay);
    }
        

    private void Update()
    {
        if (tapped)
        {
            Counter += Time.deltaTime;
        }

        if(Counter > 0.35f)
        {
            Overlay.GetComponent<Image>().color = new Color(1, 1, 1, 0);
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
            Overlay.GetComponent<Image>().color = new Color(1, 0.92f, 0.016f, 0.5f);
        }
        if(DmgModifier == 2)
        {
            Overlay.GetComponent<Image>().color = new Color(1, 0.5f, 0, 0.5f);
        }
        if(DmgModifier == 3)
        {
            Overlay.GetComponent<Image>().color = new Color(1, 0, 0, 0.5f);
        }
    }
}
