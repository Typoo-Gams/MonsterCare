using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealMonster : MonoBehaviour
{
    private Button ThisButton;
    public MonsterManager_Prototype HealThisMonster;
    public int HealBy = 10;

    // Start is called before the first frame update
    void Start()
    {
        ThisButton = gameObject.GetComponent<Button>();
        ThisButton.onClick.AddListener(TaskOnClick);
        HealThisMonster.StartMonster.DebugStatus();
    }

    private void TaskOnClick() 
    {
        HealThisMonster.StartMonster.UpdateHealth(HealThisMonster.StartMonster.HealthStatus + HealBy);
    }
}
