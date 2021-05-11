using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dmg_BodyParts : MonoBehaviour
{
    GameManager manager;
    GameObject Overlay;

    bool currentlyAttacking;
    public WeaknessType DmgModifier;
    public float[] DMG_values = {1.8f, 1.3f, 0.8f };
    int dmg_index;
    float Counter;
    bool tapped;

    public enum WeaknessType {
        Weak,
        Moderate,
        Strong
    }

    private void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        currentlyAttacking = manager.EnemyMonster.CombatStatus;
        //idk why its done like this.
        Overlay = gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject;

        Debug.Log(Overlay.GetComponent<Image>().color);

        switch (DmgModifier)
        {
            case WeaknessType.Strong:
                dmg_index = 0;
                break;

            case WeaknessType.Moderate:
                dmg_index = 1;
                break;

            case WeaknessType.Weak:
                dmg_index = 2;
                break;
        }

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
            manager.EnemyMonster.DealDmg(1 * DMG_values[dmg_index]);
            FindObjectOfType<SoundManager>().play("SwordSwing");
        }
        
        if(manager.ActiveMonster.HealthStatus == 0)
        {
            currentlyAttacking = false;
        }


        switch (DmgModifier)
        {
            case WeaknessType.Strong:
                Overlay.GetComponent<Image>().color = new Color(1, 0.92f, 0.016f, 0.5f);
                break;

            case WeaknessType.Moderate:
                Overlay.GetComponent<Image>().color = new Color(1, 0.5f, 0, 0.5f);
                break;

            case WeaknessType.Weak:
                Overlay.GetComponent<Image>().color = new Color(1, 0, 0, 0.5f);
                break;
        }
    }
}
