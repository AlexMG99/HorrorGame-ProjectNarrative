using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public float acceleration = 5f;
    public float maxSpeed = 5.0f;
    public float camMovSpeed = 5.0f;

    List<string> inventory;

    CharacterController controller;
    CinemachineVirtualCamera virtualCamera;

    public GameObject middlePoint;
    public Text objectText;

   
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        virtualCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();

        inventory = new List<string>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("RayCast");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown("e"))
            PickObject();
    }

    // Player Movement Input
    void PlayerMovement()
    {
        // Player acceleration
        if (speed < maxSpeed && (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0))
            speed += acceleration * Time.deltaTime;
        else if(speed > 1.0 && Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            speed -= acceleration * Time.deltaTime;

        // Movement
        Vector3 move = Vector3.zero;
        move += Input.GetAxis("Vertical") * virtualCamera.gameObject.transform.forward;
        move += Input.GetAxis("Horizontal") * virtualCamera.gameObject.transform.right;
        move.y = 0.0f;
        //move += Physics.gravity * 0.5f;
        controller.Move(move * Time.deltaTime * speed);



        Vector3 direction =  Vector3.Lerp(gameObject.transform.forward, virtualCamera.gameObject.transform.forward, camMovSpeed * Time.deltaTime);
        gameObject.transform.forward = new Vector3(direction.x, 0.0f, direction.z);
    }

    IEnumerator RayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(virtualCamera.gameObject.transform.position, virtualCamera.gameObject.transform.forward, out hit, 100.0f))
        {
            middlePoint.SetActive(false);
            objectText.text = "";

            if (hit.transform.gameObject.CompareTag("Pick"))
            {
                middlePoint.SetActive(true);
                objectText.text = hit.transform.gameObject.name;
            }
        }

        yield return new WaitForSeconds(.5f);
        StartCoroutine("RayCast");
    }

    void PickObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(virtualCamera.gameObject.transform.position, virtualCamera.gameObject.transform.forward, out hit, 100.0f))
        {
            Debug.DrawRay(virtualCamera.gameObject.transform.position, virtualCamera.gameObject.transform.forward * 10000, Color.green, 5);
            if (hit.transform.gameObject.CompareTag("Pick"))
            {
                inventory.Add(hit.transform.gameObject.name);
                Debug.Log("Object: " + inventory[0]);
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
