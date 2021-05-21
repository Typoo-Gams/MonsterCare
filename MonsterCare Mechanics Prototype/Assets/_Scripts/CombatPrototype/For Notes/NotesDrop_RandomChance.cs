using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesDrop_RandomChance : MonoBehaviour
{
    GameManager manager;
    GameSaver Saver = new GameSaver();

    private bool isCreated;
    public bool EnemyDead;

    //This chooses the dropchance for the different generations
    //const float dropChance0 = 1f / 3f;
    //const float dropChance1 = 1f / 5f;

    //Note Goups
    List<NoteGroup> Groups = new List<NoteGroup>();
    [SerializeField] public NoteGroup group0; // Notes 1-4
    [SerializeField] public NoteGroup group1; // Notes 5-8
    public int GroupDropIndex = 0;
    List<int> DroppableNotes = new List<int>();

    public Image AbilityIcon1;
    public Image AbilityIcon2;

    int DeathAnimIndex;
    public float animCnt;
    public GameObject DeathAnimsObject;
    Animator CurrentDeath;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        isCreated = false;
        group0.DropChance = 1;
        Groups.Add(group0);
        //Groups.Add(group1);

        int noteIndex = 1;
        for (int i = 0; i < Groups.Count; i++)
        {
            int count = 0;
            for (int j = noteIndex; j < Groups[i].Notes.Length+1; j++)
            {
                if (Saver.LoadNote(j) == 1)
                {
                    count++;
                    Debug.Log("Note" + j);
                }
                if (count == Groups[i].Notes.Length)
                {
                    GroupDropIndex++;
                    Debug.LogWarning("Note group " + i + " has been collected");
                }
            }
            noteIndex += Groups[i].Notes.Length;
        }

        for (int i = Groups[GroupDropIndex].NoteIndexStart; i < Groups[GroupDropIndex].Notes.Length; i++)
        {
            if (Saver.LoadNote(i) == 1)
                DroppableNotes.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.EnemyMonster != null)
        {
            //Checks if the enemy has died to know if its gonna create a note
            if (manager.EnemyMonster.DeathStatus && isCreated == false)
            {
                DeathAnimIndex = GetComponent<Toughness_Modifer>().ActiveMonster;

                AbilityIcon1.enabled = false;
                AbilityIcon2.enabled = false;

                isCreated = true;
            }
        }
        if (isCreated && !EnemyDead)
        {
            EnemyHasDied();
        }
    }

    //Makes a random and drops the note if succeeded
    public void EnemyHasDied()
    {
        if (isCreated == true)
        {
            if (!EnemyDead) 
            {
                DeathAnimsObject.transform.GetChild(DeathAnimIndex).gameObject.SetActive(true);
                CurrentDeath = DeathAnimsObject.transform.GetChild(DeathAnimIndex).GetComponent<Animator>();
            }
            animCnt += Time.deltaTime;
            manager.EnemyMonster.CombatActive(false);
            manager.Enemy.SetActive(false);

            if (CurrentDeath.GetCurrentAnimatorStateInfo(0).length < animCnt)
            {
                //Debug.LogError("enemy has now died");
                float random = Random.Range(0f, 1f);
                EnemyDead = true;
                //Debug.LogError((random <= Groups[GroupDropIndex].DropChance) + ": " + random + " <= " + Groups[GroupDropIndex].DropChance); 


                //drops a random note that hasnt been found yet. only drops notes from a certain range of predetermined groups of notes.
                if (random <= Groups[GroupDropIndex].DropChance)
                {
                    int prefabIndex;

                    prefabIndex = Random.Range(0, DroppableNotes.Count - 1);

                    if (GroupDropIndex < Groups.Count)
                    {
                        GameObject spawn = Instantiate(Groups[GroupDropIndex].Notes[prefabIndex]);
                        spawn.transform.SetParent(gameObject.transform, false);
                        spawn.transform.SetAsLastSibling();
                    }
                    else
                        Debug.Log("All Current notes have been obtained");
                    //PlaySound
                    FindObjectOfType<SoundManager>().play("ObtainReport");
                    Debug.Log("Group0 Note");
                }
                
            }
        }
    }
}
