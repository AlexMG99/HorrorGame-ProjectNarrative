using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    public float camMovSpeed = 5.0f;
    CharacterController controller;

    // Start is called before the first frame update
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
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        gameObject.transform.forward = Vector3.Lerp(gameObject.transform.forward, move, camMovSpeed * Time.deltaTime);
    }
}
