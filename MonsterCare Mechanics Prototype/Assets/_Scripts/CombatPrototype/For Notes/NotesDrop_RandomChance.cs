using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesDrop_RandomChance : MonoBehaviour
{
    GameManager manager;

    private bool isCreated;
    const float dropChance = 1f / 5f;
    public Image AbilityIcon1;
    public Image AbilityIcon2;

    public List<GameObject> prefabList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        isCreated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.EnemyMonster != null)
        {
            if (manager.EnemyMonster.DeathStatus && isCreated == false)
            {
                AbilityIcon1.enabled = false;
                AbilityIcon2.enabled = false;

                isCreated = true;
                EnemyHasDied();
            }
        }
        
    }

    public void EnemyHasDied()
    {
        if (isCreated == true)
        {
            if (Random.Range(0f, 1f) <= dropChance)
            {
                int prefabIndex = UnityEngine.Random.Range(0, 4);
                Instantiate(prefabList[prefabIndex]);
            }
        }
    }
}
