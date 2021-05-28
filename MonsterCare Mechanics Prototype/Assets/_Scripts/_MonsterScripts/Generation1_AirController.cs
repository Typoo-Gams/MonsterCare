using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation1_AirController : MonsterController
{
    bool evolved;
    //Air monster Gen 1 evolutions
    public string waterEvoPath, airEvoPath;
    public override void Evolution()
    {
        if (evolved)
        {
            switch (monster.Element)
            {
                case MonsterElement.Air:
                    break;

                case MonsterElement.Water:
                    break;

                default:
                    Debug.Log("This Monster Cant Evolve into this element");
                    break;
            }
            _InstantiateEvolution();
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
