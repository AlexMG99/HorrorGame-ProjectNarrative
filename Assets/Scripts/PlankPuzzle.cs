using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankPuzzle : MonoBehaviour
{
    public GameObject newPath;
    int plankCurrent = 0;
    public int plankNeeded = 1;

    private void Start()
    {
        gameObject.name = "Wood Planks (" + plankCurrent.ToString() + "/" + plankNeeded.ToString() + ")";
    }

    public void AddWoodPlank()
    {
        plankCurrent++;
        gameObject.name = "Wood Planks (" + plankCurrent.ToString() + "/" + plankNeeded.ToString() + ")";

        if(plankCurrent == plankNeeded)
        {
            createPath();
        }
    }

    public void createPath()
    {
        GameObject.Instantiate(newPath, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}
