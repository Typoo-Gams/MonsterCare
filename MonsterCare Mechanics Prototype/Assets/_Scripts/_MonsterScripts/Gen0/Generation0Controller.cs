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
            
            //create the new monster
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
            _InstantiateEvolution();
        }
    }

    public override void Devolution()
    {
        if (monster.DeathStatus)
        {
            //resets the devolution path so in case something goes wrong the monster doesnt deevolve into a higher level monster
            Saver.SaveMonsterDevolution("");
            TransferMonsterStats();
            NextEvolution = Resources.Load<GameObject>(devolutionPath);
            _InstantiateEvolution();
            Debug.Log("Monster died and devolved");
        }
    }
}
