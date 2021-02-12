using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAtHome_FeedingPrototype : MonoBehaviour
{

    public Monster Monster;
    // Start is called before the first frame update
    void Start()
    {
        Monster = new Monster("PleaseFeedMe");
        Monster.DebugMonster();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
