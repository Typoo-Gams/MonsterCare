using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dmg_BodyParts : MonoBehaviour
{
    public MonsterManager_AttackPrototype AttackingBack;
    bool currentlyAttacking;
    public List<GameObject> colliders = new List<GameObject>();

    float Torso = 1f;
    float Head = 2f;
    float Arms = 2f;
    float Legs = 1f;

    private void Start()
    {
        currentlyAttacking = AttackingBack.StartMonster.CombatStatus;
    }

    private void Update()
    {
        Attacking();
    }

    private void Attacking()
    {
        if(currentlyAttacking == true)
        {
            if(Input.touchCount > 0)
            {
                if (gameObject.CompareTag("Head"))
                {
                    AttackingBack.StartMonster.HealthStatus += Head;
                    Debug.Log("Working");
                }
                
            }
            
        }
        
    }
}
