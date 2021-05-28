using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation1Controller : MonsterController
{
    bool evolved;
    public override void Evolution()
    {
        if(!evolved)
        {
            evolved = true;
            throw new System.NotImplementedException();
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
