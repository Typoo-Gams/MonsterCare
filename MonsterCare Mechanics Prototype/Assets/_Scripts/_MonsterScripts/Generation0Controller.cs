using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation0Controller : MonsterController
{
    //Generation 0 evolution posibilities
    public string fireEvoPath, waterEvoPath, airEvoPath, earthEvoPath;


    //Generation 0 evolution conditions.
    public override void Evolution()
    {
        //Destroy the current monster object. spawn in the new monster. needs to load the new evolved monster when the game is reopened after being closed.
        if(IsEvolutionAnimDone())
        {
            //Destroy the old monster
            Destroy(gameObject);
            //create the new monster
            GameObject NextEvolution = null;
            switch (monster.Element)
            {
                case MonsterElement.Fire:
                    NextEvolution = Resources.Load<GameObject>(fireEvoPath);
                    break;

                case MonsterElement.Water:
                    NextEvolution = Resources.Load<GameObject>(waterEvoPath);
                    break;

                case MonsterElement.Earth:
                    NextEvolution = Resources.Load<GameObject>(earthEvoPath);
                    break;

                case MonsterElement.Air:
                    NextEvolution = Resources.Load<GameObject>(airEvoPath);
                    break;
            }
            GameObject Spawned = Instantiate(NextEvolution);
            Spawned.transform.SetParent(transform.parent, false);
            manager.ActiveMonster.PreviousEvolution = _prefabLocation;
        }
    }

    public override void Devolution()
    {
        if (monster.DeathStatus)
        {
            //Destroy the current monster object. spawn in the new monster. needs to load the new evolved monster when the game is reopened after being closed. clears the save file with an empty monster
            Monster empty = new Monster(devolutionName, devolutionPath);
            empty.DebugMonster();
            Saver.LoadMonster(empty);
            empty.UpdateHealth(empty.GetMaxHealth);
            Saver.SaveMonster(empty);
            GameObject NextEvolution = Resources.Load<GameObject>(devolutionPath);
            GameObject Parent = GameObject.Find("__app").GetComponentInChildren<GameManager>().gameObject;
            Destroy(gameObject);
            GameObject SpawnedMonster = Instantiate(NextEvolution);
            SpawnedMonster.transform.SetParent(Parent.transform, false);
            //Saver.SaveMonster(SpawnedMonster.GetComponent<ChildGen0_MonsterController>().monster);
            manager.ActiveMonster.PreviousEvolution = _prefabLocation;
            Debug.Log("Monster died and devolved");
        }
    }
}
