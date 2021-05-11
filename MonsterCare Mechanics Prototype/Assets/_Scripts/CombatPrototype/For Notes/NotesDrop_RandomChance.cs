using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesDrop_RandomChance : MonoBehaviour
{
    GameManager manager;
    GameSaver Saver = new GameSaver();

    private bool isCreated;

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

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
        isCreated = false;
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
                    Debug.LogError("Note" + j);
                }
                if (count == Groups[i].Notes.Length)
                {
                    GroupDropIndex++;
                    Debug.LogError("Note group " + i + " has been collected");
                }
            }
            noteIndex += Groups[i].Notes.Length;
        }

        for (int i = Groups[GroupDropIndex].NoteIndexStart; i < Groups[GroupDropIndex].Notes.Length; i++)
        {
            if (Saver.LoadNote(i) == 1)
                DroppableNotes.Add(i);
            else
                DroppableNotes.Add(0);
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
                AbilityIcon1.enabled = false;
                AbilityIcon2.enabled = false;

                isCreated = true;
                EnemyHasDied();
            }
        }
    }

    //Makes a random and drops the note if succeeded
    public void EnemyHasDied()
    {
        if (isCreated == true)
        {
            float random = Random.Range(0f, 1f);

            //checks if it has evolved and then changes the dropchance accordingly
            if (random <= Groups[GroupDropIndex].DropChance)
            {

                int prefabIndex;

                do
                    prefabIndex = Random.Range(0, group0.Notes.Length -1);
                while (DroppableNotes[prefabIndex] == 0);

                if (GroupDropIndex < Groups.Count)
                    Instantiate(Groups[GroupDropIndex].Notes[prefabIndex]);
                else
                    Debug.Log("All Current notes have been obtained");
                //PlaySound
                FindObjectOfType<SoundManager>().play("ObtainReport");
                Debug.Log("Group0 Note");
            }
        }
    }
}
