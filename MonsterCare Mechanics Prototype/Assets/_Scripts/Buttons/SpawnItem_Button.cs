using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *   This Script is meant to be attached to any button and automatically add a the spawn object method to its on click trigger.
 *   The object dragged into the SpawnObject in the inspector is the object that will be spawned.
 *   the SpawnLocation, SpawnRotationQuaternion, and ParentObject is the spawn location desired, rotation desired and its parent object desired.
 * 
 *   If a toggle for spawning is desired, ToggleCanSpawn can be used to toggle the ability to spawn an object.
 */


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

    //Method called when the button is clicked
    void TaskOnClick() 
    {
        //If the script is allowed to spawn an item
        if (canSpawn) 
        {
            //If a spawn location has been input
            if (SpawnLocation != new Vector3(0, 0, 0))
            {
                //if a spawn rotation has been input
                if (SpawnRotationQuaternion != new Quaternion(0, 0, 0, 0))
                {
                    //if a parent object has been input
                    //Spawns an object with a input spawn location, rotation with or without a parent.
                    if (ParentObject != null)
                    {
                        SpawnedObject = Instantiate(SpawnObject, SpawnLocation, SpawnRotationQuaternion);
                        SpawnedObject.transform.SetParent(ParentObject.transform, false);
                        SpawnedObject.transform.localPosition = SpawnLocation;
                        SpawnedObject.transform.localRotation = SpawnRotationQuaternion;
                    }
                    else
                        Instantiate(SpawnObject, SpawnLocation, SpawnRotationQuaternion);
                }
                else
                {
                    //Spawns an object with an input spawn location with or without a parent.
                    if (ParentObject != null)
                    {
                        SpawnedObject = Instantiate(SpawnObject, SpawnLocation, Quaternion.identity);
                        SpawnedObject.transform.SetParent(ParentObject.transform, false);
                        SpawnedObject.transform.localPosition = SpawnLocation;
                    }
                    else
                        Instantiate(SpawnObject, SpawnLocation, Quaternion.identity);
                }
            }
            //Spawns an object with or without a parent.
            else if (ParentObject != null)
            {
                Instantiate(SpawnObject, ParentObject.transform);
            }
            else
                Instantiate(SpawnObject);
        }
    }

    //Toggles the ability to spawn objects
    public void ToggleCanSpawn() 
    {
        canSpawn = !canSpawn;
    }
}
