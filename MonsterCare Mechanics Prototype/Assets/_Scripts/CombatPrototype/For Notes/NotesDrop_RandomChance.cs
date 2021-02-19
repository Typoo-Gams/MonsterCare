using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesDrop_RandomChance : MonoBehaviour
{
    public MonsterManager_AttackPrototype IsItDead;
    public bool isCreated;
    float currentHealth;
    const float dropChance = 1f / 5f;
    public Image image1;
    public Image image2;

    List<GameObject> prefabList = new List<GameObject>();
    public GameObject note1;
    public GameObject note2;
    public GameObject note3;
    public GameObject note4;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = IsItDead.StartMonster.HealthStatus;
        isCreated = false;

        prefabList.Add(note1);
        prefabList.Add(note2);
        prefabList.Add(note3);
        prefabList.Add(note4);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsItDead.StartMonster.DeathStatus && isCreated == false)
        {
            image1.enabled = false;
            image2.enabled = false;

            isCreated = true;
            EnemyHasDied();
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
