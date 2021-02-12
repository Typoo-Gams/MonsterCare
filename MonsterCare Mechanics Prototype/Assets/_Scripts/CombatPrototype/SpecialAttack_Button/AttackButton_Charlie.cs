using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton_Charlie : MonoBehaviour
{

    private Button ThisButton;
    public MonsterManager_AttackPrototype AttackThisMonster;
    private int Damage = 40;

    // Start is called before the first frame update
    void Start()
    {
        ThisButton = gameObject.GetComponent<Button>();
        ThisButton.onClick.AddListener(SpecialAttack);
        AttackThisMonster.StartMonster.DebugMonster();
    }

    
    public void SpecialAttack()
    {
        AttackThisMonster.StartMonster.UpdateHealth(AttackThisMonster.StartMonster.HealthStatus - Damage);
    }
}
