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

    private Button ThisButton;
    

    // Start is called before the first frame update
    void Start()
    {
        ThisButton = gameObject.GetComponent<Button>();
        ThisButton.onClick.AddListener(TaskOnClick);
    }


    void TaskOnClick() 
    {
        if (SpawnLocation != new Vector3(0, 0, 0))
        {
            if (SpawnRotationQuaternion != new Quaternion(0, 0, 0, 0))
            {
                if (ParentObject != null)
                {
                    SpawnedObject = Instantiate(SpawnObject, SpawnLocation, SpawnRotationQuaternion, ParentObject.transform);
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
                    SpawnedObject = Instantiate(SpawnObject, SpawnLocation, Quaternion.identity, ParentObject.transform);
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


         //  SpawnedObject.transform.parent = ParentObject.transform;
         //  SpawnedObject.transform.localPosition = SpawnLocation;
         //  SpawnedObject.transform.localRotation = SpawnRotation
         //  SpawnedObject.transform.localScale = new Vector3(10, 10, 10);
    
    }
}
