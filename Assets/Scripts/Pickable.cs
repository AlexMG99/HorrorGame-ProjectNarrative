using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public GameObject assignedTorch;
    public GameObject newTorch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPickUp()
    {
        assignedTorch.SetActive(false);
        newTorch.SetActive(true);
    }
}
