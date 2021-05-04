using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public float acceleration = 5f;
    public float maxSpeed = 5.0f;
    public float camMovSpeed = 5.0f;

    List<string> inventory;


    CharacterController controller;
    CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        virtualCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();

        inventory = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown("E"))
            StartCoroutine("RayCastToPick");
    }

    // Player Movement Input
    void PlayerMovement()
    {
        // Player acceleration
        if (speed < maxSpeed && (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0))
            speed += acceleration * Time.deltaTime;
        else if(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            speed = 1.0f;

        // Movement
        Vector3 move = Vector3.zero;
        move += Input.GetAxis("Vertical") * virtualCamera.gameObject.transform.forward;
        move += Input.GetAxis("Horizontal") * virtualCamera.gameObject.transform.right;
        move.y = 0.0f;


        controller.Move(move * Time.deltaTime * speed);

        Vector3 direction =  Vector3.Lerp(gameObject.transform.forward, virtualCamera.gameObject.transform.forward, camMovSpeed * Time.deltaTime);
        gameObject.transform.forward = new Vector3(direction.x, 0.0f, direction.z);
    }

    IEnumerator RayCastToPick()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 20.0f))
        {
            if (hit.transform.gameObject.CompareTag("Pick"))
            {
                inventory.Add(hit.transform.gameObject.name);
                Destroy(hit.transform.gameObject);
            }
        }

        yield return null;
    }
}
