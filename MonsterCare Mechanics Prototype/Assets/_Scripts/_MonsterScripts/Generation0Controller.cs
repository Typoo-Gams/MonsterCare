using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation0Controller : MonsterController
{
    public string fireEvoPath, waterEvoPath, airEvoPath, earthEvoPath;
    //Generation 0 evolution conditions.
    public override void Evolution()
    {
        //This is what happens when the monster is evolving.
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
}
