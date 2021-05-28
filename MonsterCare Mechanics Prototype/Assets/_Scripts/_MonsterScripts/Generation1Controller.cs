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
}
