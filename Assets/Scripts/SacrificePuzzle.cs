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

    public GameObject bridgePath;
    public GameObject cavePath;

    public GameObject bridgeTorch1;
    public GameObject bridgeTorch2;

    public GameObject caveTorch1;
    public GameObject caveTorch2;


    public GameObject Mush_director;
    public GameObject Meat_Director;

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

        gameObject.name = "Bowl (" + inventory.Count + "/4)";

        // Despawn other object
        if (inventory.Count == 1)
            DespawnOtherObject(objName);

        if (IsFinished())
        {
            // Explode everything
            // Cinematic
            // Opens one path
            if (objName == "Meat")
            {
                // Activate torches
               // bridgeTorch1.SetActive(true);
                //bridgeTorch2.SetActive(true);

                bridgePath.SetActive(false);
                Meat_Director.GetComponent<CutsceneDirector>().StartTimeLine();
            }
            else
            {
                // Activate torches
                //caveTorch1.SetActive(true);
                //caveTorch2.SetActive(true);

                cavePath.SetActive(false);
                Mush_director.GetComponent<CutsceneDirector>().StartTimeLine();
                


            }
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
            return true;

        return false;
    }
}
