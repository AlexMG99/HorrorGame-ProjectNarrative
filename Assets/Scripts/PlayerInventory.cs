﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject middlePoint;
    public Text objectText;
    public SacrificePuzzle sacrificePuzzle;

    public Text meatText;
    public Text mushroomText;

    List<string> inventory;

    GameObject inventoryUI;

    bool isPushing = false;
   

    // Start is called before the first frame update
    void Awake()
    {
        inventory = new List<string>();
    }

    void Start()
    {
        StartCoroutine("RayCast");

        inventoryUI = GameObject.Find("Inventory");
        inventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
            Interact();
        if (Input.GetKeyDown("i"))
            inventoryUI.SetActive(!inventoryUI.active);
        if (Input.GetKeyDown("1"))
        {
            inventory.Add("Meat");
            Debug.Log("Object: " + inventory[inventory.Count - 1]);
        }
        if (Input.GetKeyDown("2"))
        {
            inventory.Add("Poisonous Mushroom");
            Debug.Log("Object: " + inventory[inventory.Count - 1]);
        }
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

            if (hit.transform.gameObject.CompareTag("Push") && Vector3.Distance(hit.transform.gameObject.transform.position, gameObject.transform.position) < 5.0f)
            {
                middlePoint.SetActive(true);
                objectText.text = hit.transform.gameObject.name;
            }
        }

        yield return new WaitForSeconds(.5f);
        StartCoroutine("RayCast");
    }

    IEnumerator CheckDistance(RaycastHit rock)
    {
        while(isPushing && Vector3.Distance(rock.transform.gameObject.transform.position, gameObject.transform.position) < 10.0f)
        {
            yield return new WaitForSeconds(.1f);
        }

        rock.transform.parent = null;
        ChangePlayerMaxSpeed(5.0f);

        yield return null;
    }



    void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(GameManager.Instance.GetVirtualCamera().transform.position, GameManager.Instance.GetVirtualCamera().transform.forward, out hit, 20.0f))
        {
            if (hit.transform.gameObject.CompareTag("Pick"))
            {
                hit.transform.gameObject.gameObject.GetComponent<Pickable>().OnPickUp();
                inventory.Add(hit.transform.gameObject.name);
                meatText.text = "x" + GetInventoryCount("Meat");
                mushroomText.text = "x" + GetInventoryCount("Poisonous Mushroom");
                Destroy(hit.transform.gameObject);
            }
            else if (hit.transform.gameObject.CompareTag("Interactable"))
            {
                if (hit.transform.gameObject.name == "Knife")
                {
                    string objName = GetInventoryObject("Meat");
                    if (objName != "No object")
                    {
                        sacrificePuzzle.AddObjectToBowl(objName);
                        meatText.text = "x" + GetInventoryCount("Meat");
                    }
                }
                else if (hit.transform.gameObject.name == "Mortar")
                {
                    string objName = GetInventoryObject("Poisonous Mushroom");
                    if (objName != "No object")
                    {
                        sacrificePuzzle.AddObjectToBowl(objName);
                        mushroomText.text = "x" + GetInventoryCount("Poisonous Mushroom");
                    }
                }
            }
            if (hit.transform.gameObject.CompareTag("Push") && Vector3.Distance(hit.transform.gameObject.transform.position, gameObject.transform.position) < 5.0f)
            {
                if (isPushing)
                {
                    isPushing = false;
                }
                else
                {
                    hit.transform.parent = gameObject.transform;
                    ChangePlayerMaxSpeed(1.0f);
                    isPushing = true;
                    StartCoroutine(CheckDistance(hit));
                }
            }
        }
    }

    void ChangePlayerMaxSpeed(float speed)
    {
        GetComponent<PlayerController>().maxSpeed = speed;
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

    int GetInventoryCount(string name)
    {
        int objCount = 0;

        foreach (string obj in inventory)
        {
            if (obj == name)
            {
                objCount++;
            }
        }

        return objCount;
    }
}
