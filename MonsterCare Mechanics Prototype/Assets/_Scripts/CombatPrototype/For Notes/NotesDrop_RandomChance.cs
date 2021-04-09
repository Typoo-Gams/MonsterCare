using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesDrop_RandomChance : MonoBehaviour
{
    GameManager manager;

    private bool isCreated;

    //This chooses the dropchance for the different generations
    const float dropChance = 1f / 5f;
    const float dropChance1 = 1f / 8f;
    //const float dropChance2 = 0f / 0f;

    char[] seperator = { '_' };

    public Image AbilityIcon1;
    public Image AbilityIcon2;

    //List of the notes, can add more in the inspector
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
            //Checks if the enemy has died to know if its gonna create a note
            if (manager.EnemyMonster.DeathStatus && isCreated == false)
            {
                AbilityIcon1.enabled = false;
                AbilityIcon2.enabled = false;

                isCreated = true;
                EnemyHasDied();
            }
        }
        
    }

    //Spawns the notes
    public void EnemyHasDied()
    {
        if (isCreated == true)
        {
            //checks if it has evolved and then changes the dropchance accordingly
            switch (manager.MonsterObject.name.Split(seperator)[1])
            {
                case "Gen0":
                    if (Random.Range(0f, 1f) <= dropChance)
                    {
                        int prefabIndex = UnityEngine.Random.Range(0, 4);
                        Instantiate(prefabList[prefabIndex]);
                        FindObjectOfType<SoundManager>().play("ObtainReport");
                    }
                    break;

                case "Gen1":
                    if (Random.Range(0f, 1f) <= dropChance)
                    {
                        int prefabIndex = UnityEngine.Random.Range(0, 4);
                        Instantiate(prefabList[prefabIndex]);
                        FindObjectOfType<SoundManager>().play("ObtainReport");
                    }
                    break;
            }
        }
    }
}
