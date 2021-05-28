using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation1Controller : MonsterController
{
    bool evolved;
    //Gen 1 evolutions
    public string fireEvoPath, waterEvoPath, airEvoPath, earthEvoPath;
    public override void Evolution()
    {
        if(!evolved)
        {
            evolved = true;

            switch (monster.Element)
            {
                case MonsterElement.Air:
                break;

                case MonsterElement.Earth:
                    break;

                case MonsterElement.Fire:
                    break;

                case MonsterElement.Water:
                    break;
            }
        }
    }


    public override void Devolution()
    {
        if (monster.DeathStatus)
        {
            TransferMonsterStats();
            NextEvolution = Resources.Load<GameObject>(devolutionPath);
            _InstantiateEvolution();
            Debug.Log("Monster died and devolved");
        }
    }
}
