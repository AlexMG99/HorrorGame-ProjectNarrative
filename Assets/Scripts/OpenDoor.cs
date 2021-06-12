using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject doorLeft;
    public GameObject doorRight;


    void OnTriggerEnter(Collider hit)
    {
       if(hit.transform.gameObject.CompareTag("Push"))
       {
            OpenDoors();
       }
    }

    void OpenDoors()
    {
        doorLeft.transform.Rotate(new Vector3(0, -80, 0));
        doorRight.transform.Rotate(new Vector3(0, 80, 0));
    }
}
