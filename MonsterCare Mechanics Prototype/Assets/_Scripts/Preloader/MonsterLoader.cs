using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLoader : MonoBehaviour
{
    GameSaver Saver = new GameSaver();
    public GameObject Manager;

    // Start is called before the first frame update
    void Start()
    {
        string path = Saver.GetMonsterPrefab();
        GameObject SavedMonster = Resources.Load<GameObject>(path);
        Instantiate(SavedMonster, SavedMonster.transform.position, Quaternion.identity, Manager.transform);
    }
}
