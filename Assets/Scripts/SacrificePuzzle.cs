using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificePuzzle : MonoBehaviour
{
    List<string> inventory;

    public GameObject spawnPoint;
    public GameObject meat;
    public GameObject mushroom;

    public GameObject knife;
    public GameObject mortar;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<string>();
    }

    public void AddObjectToBowl(string objName)
    {
        if (objName == "Meat")
        {
            GameObject newObj = Instantiate(meat, transform.position, transform.rotation);
            inventory.Add(newObj.name);
        }
        else if (objName == "Poisonous Mushroom")
        {
            GameObject newObj = Instantiate(mushroom, transform.position, transform.rotation);
            inventory.Add(newObj.name);
        }
        else
        {
            Debug.Log("No object!");
        }

        // Despawn other object
        if (inventory.Count == 1)
            DespawnOtherObject(objName);

        if (IsFinished())
        {
            // Explode everything
            // Cinematic
            // Opens one path
        }
    }

    void DespawnOtherObject(string name)
    {
        if (name == "Meat")
        {
            // Pop Up effect
            mortar.SetActive(false);
        }
        else if (name == "Poisonous Mushroom") 
        {
            knife.SetActive(false);
        }
    }

    bool IsFinished()
    {

        if (inventory.Count == 4)
        {
            return true;
        }

        return false;
    }
}
