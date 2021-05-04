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

    CharacterController controller;
   
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
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
        move += Input.GetAxis("Vertical") * GameManager.Instance.GetVirtualCamera().transform.forward;
        move += Input.GetAxis("Horizontal") * GameManager.Instance.GetVirtualCamera().transform.right;
        move.y = 0.0f;
        //move += Physics.gravity * 0.5f;
        controller.Move(move * Time.deltaTime * speed);

        Vector3 direction =  Vector3.Lerp(gameObject.transform.forward, GameManager.Instance.GetVirtualCamera().transform.forward, camMovSpeed * Time.deltaTime);
        gameObject.transform.forward = new Vector3(direction.x, 0.0f, direction.z);
    }


}
