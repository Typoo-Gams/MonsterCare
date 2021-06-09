using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation1_Controller : MonsterController
{
    //Air monster Gen 1 evolutions
    public string Evo1Path, Evo2Path;
    public MonsterElement Evo1, Evo2;

    public override void Evolution()
    {
        if (IsEvolutionAnimDone())
        {
            Saver.SaveMonsterDevolution(monster.PrefabLocation);
            if (Evo1 == monster.Element)
            {
                NextEvolution = Resources.Load<GameObject>(Evo1Path);
            }
            else if (Evo2 == monster.Element)
            {
                NextEvolution = Resources.Load<GameObject>(Evo2Path);
            }
            else
            {
                Debug.Log("This Monster Cant Evolve into this element");
                monster.CanEvolveStatus = false;
                thisAnimator.SetBool("Evolve", false);
            }
            if (NextEvolution != null)
            {
                _InstantiateEvolution();
            }
        }
    }


    override public void Devolution()
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