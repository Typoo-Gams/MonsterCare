using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnItem_Button : MonoBehaviour
{

    public GameObject SpawnObject;
    public Vector3 SpawnLocation;
    public Quaternion SpawnRotationQuaternion;
    public GameObject ParentObject;
    private GameObject SpawnedObject;
    bool canSpawn = true;

    private Button ThisButton;
    

    // Start is called before the first frame update
    void Start()
    {
        ThisButton = gameObject.GetComponent<Button>();
        ThisButton.onClick.AddListener(TaskOnClick);
    }


    void TaskOnClick() 
    {
        if (canSpawn) 
        {
            if (SpawnLocation != new Vector3(0, 0, 0))
            {
                if (SpawnRotationQuaternion != new Quaternion(0, 0, 0, 0))
                {
                    if (ParentObject != null)
                    {
                        SpawnedObject = Instantiate(SpawnObject, SpawnLocation, SpawnRotationQuaternion);
                        SpawnedObject.transform.SetParent(ParentObject.transform, false);
                        SpawnedObject.transform.localPosition = SpawnLocation;
                        SpawnedObject.transform.localRotation = SpawnRotationQuaternion;
                        //SpawnedObject.transform.localScale = new Vector3(10, 10, 10);
                    }
                    else
                        Instantiate(SpawnObject, SpawnLocation, SpawnRotationQuaternion);
                }
                else
                {
                    if (ParentObject != null)
                    {
                        SpawnedObject = Instantiate(SpawnObject, SpawnLocation, Quaternion.identity);
                        SpawnedObject.transform.SetParent(ParentObject.transform, false);
                        SpawnedObject.transform.localPosition = SpawnLocation;
                        //SpawnedObject.transform.localScale = new Vector3(10, 10, 10);
                    }
                    else
                        Instantiate(SpawnObject, SpawnLocation, Quaternion.identity);
                }

            }
            else if (ParentObject != null)
            {
                Instantiate(SpawnObject, ParentObject.transform);
            }
            else
                Instantiate(SpawnObject);
        }
    }

    public void ToggleCanSpawn() 
    {
        canSpawn = !canSpawn;
    }
}
