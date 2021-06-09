using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastEvolution_Controller : MonsterController
{
    Animator evolveButton;

    //Deevolves into the monster that the player had before this monster
    public override void Devolution()
    {
        if (monster.DeathStatus)
        {
            TransferMonsterStats();
            if (Saver.LoadMonsterDevolution() != "")
                NextEvolution = Resources.Load<GameObject>(Saver.LoadMonsterDevolution());
            _InstantiateEvolution();
            Debug.Log("Monster died and devolved");
            //resets the devolution path so in case something goes wrong the monster doesnt deevolve into a higher level monster
            Saver.SaveMonsterDevolution("");
        }
    }

    public override void Evolution()
    {
        if (evolveButton == null)
            evolveButton = GameObject.FindGameObjectWithTag("EvolveButtonAnim").GetComponent<Animator>();
        monster.CanEvolveStatus = false;
        evolveButton.SetBool("Active", false);
    }
}
