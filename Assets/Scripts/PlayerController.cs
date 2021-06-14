using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public float acceleration = 5f;
    public float maxSpeed = 5.0f;
    public float camMovSpeed = 5.0f;

    public Image blinkImage;

    bool isRunning = false;
    bool isCrouched = false;
    public bool isBlind = false;

    CharacterController controller;
    Animator animator;
   
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;
        else if (Input.GetKey(KeyCode.Space))
            CloseEyes();
        else if (Input.GetKeyUp(KeyCode.Space))
            StartCoroutine("OpenEyes");


        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouched = true;
            animator.SetBool("Crouched", isCrouched);

            GameManager.Instance.ChangeCameraOffset(new Vector3(0, 0, 1.0f));
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouched = false;
            animator.SetBool("Crouched", isCrouched);

            GameManager.Instance.ChangeCameraOffset(new Vector3(0, 0.15f, 0.5f));
        }

        // Player Movement
        PlayerMovement();

        // Animations
        animator.SetFloat("Speed", Input.GetAxis("Vertical"));
    }

    void CloseEyes()
    {
        if (blinkImage.color.a < 1.0)
        {
            Color newCol = blinkImage.color;
            newCol.a += 2.5f * Time.deltaTime;
            blinkImage.color = newCol;

            if (blinkImage.color.a >= 0.9f)
                isBlind = true;
        }
    }

    IEnumerator OpenEyes()
    {
        while (blinkImage.color.a > 0.0)
        { 
            Color newCol = blinkImage.color;
            newCol.a -= 2.5f * Time.deltaTime;
            blinkImage.color = newCol;

            if (blinkImage.color.a < 0.1f)
                isBlind = false;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                newCol = blinkImage.color;
                newCol.a = 0.0f;
                blinkImage.color = newCol;
                isBlind = false;

                StopCoroutine("OpenEyes");
            }

            yield return null;
        }
    }

    // Player Movement Input
    void PlayerMovement()
    {
        // Player acceleration
        if (speed < maxSpeed && (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0))
            speed += acceleration * Time.deltaTime;
        else if(speed > 1.0 && Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            speed = 1.0f;

        // Movement
        Vector3 move = Vector3.zero;
        move += Input.GetAxis("Vertical") * GameManager.Instance.GetVirtualCamera().transform.forward;
        move += Input.GetAxis("Horizontal") * GameManager.Instance.GetVirtualCamera().transform.right;
        move.y = 0.0f;

        // Run
        if(isRunning)
            move *= 1.5f;

        move += Physics.gravity * 0.5f;
        controller.Move(move * Time.deltaTime * speed);

        Vector3 direction =  Vector3.Lerp(gameObject.transform.forward, GameManager.Instance.GetVirtualCamera().transform.forward, camMovSpeed * Time.deltaTime);
        gameObject.transform.forward = new Vector3(direction.x, 0.0f, direction.z);
    }


}
