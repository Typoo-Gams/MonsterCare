using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation1_Controller : MonsterController
{
    bool evolved;
    //Air monster Gen 1 evolutions
    public string Evo1Path, Evo2Path;
    public MonsterElement Evo1, Evo2;

    public override void Evolution()
    {
        if (evolved)
        {
            if (Evo1 == monster.Element)
            {
                NextEvolution = Resources.Load<GameObject>(Evo1Path);
            }
            else if (Evo2 == monster.Element)
            {
                NextEvolution = Resources.Load<GameObject>(Evo2Path);
            }
            else
                Debug.Log("This Monster Cant Evolve into this element");
        }
        _InstantiateEvolution();
    }


    override public void Devolution()
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

