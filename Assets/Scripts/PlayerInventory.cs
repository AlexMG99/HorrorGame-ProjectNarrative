using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject middlePoint;
    public Text objectText;
    public SacrificePuzzle sacrificePuzzle;

    List<string> inventory;
   

    // Start is called before the first frame update
    void Awake()
    {
        inventory = new List<string>();
    }

    void Start()
    {
        StartCoroutine("RayCast");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
            Interact();
    }

    IEnumerator RayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(GameManager.Instance.GetVirtualCamera().transform.position, GameManager.Instance.GetVirtualCamera().transform.forward, out hit, 20.0f))
        {
            middlePoint.SetActive(false);
            objectText.text = "";

            if (hit.transform.gameObject.CompareTag("Pick") || hit.transform.gameObject.CompareTag("Interactable"))
            {
                middlePoint.SetActive(true);
                objectText.text = hit.transform.gameObject.name;
            }
        }

        yield return new WaitForSeconds(.5f);
        StartCoroutine("RayCast");
    }

    void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(GameManager.Instance.GetVirtualCamera().transform.position, GameManager.Instance.GetVirtualCamera().transform.forward, out hit, 20.0f))
        {
            if (hit.transform.gameObject.CompareTag("Pick"))
            {
                inventory.Add(hit.transform.gameObject.name);
                Debug.Log("Object: " + inventory[0]);
                Destroy(hit.transform.gameObject);
            }
            else if (hit.transform.gameObject.CompareTag("Interactable"))
            {
                if (hit.transform.gameObject.name == "Knife")
                {
                    string objName = GetInventoryObject("Meat");
                    if (objName != "No object") 
                        sacrificePuzzle.AddObjectToBowl(objName);
                }
                else if (hit.transform.gameObject.name == "Mortar")
                {
                    string objName = GetInventoryObject("Poisonous Mushroom");
                    if (objName != "No object")
                        sacrificePuzzle.AddObjectToBowl(objName);
                }
            }
        }
    }

    string GetInventoryObject(string name)
    {
        foreach (string obj in inventory)
        {
            if (obj == name)
            {
                inventory.Remove(obj);
                return name;
            }
        }

        return "No object";
    }
}
