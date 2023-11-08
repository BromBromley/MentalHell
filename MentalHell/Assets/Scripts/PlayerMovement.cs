using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerMovement : MonoBehaviour
{
    // this script manages the player input and movement

    public bool movementEnabled = true;
    private bool playerMoving = false;

    private Rigidbody rb;

    private float movement;
    private float lastMovement;
    private float speed = 0;
    private float maxSpeed = 5;
    private float acceleration = 6;
    private float stoppingForce = 6;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (movementEnabled)
        {
            movement = Input.GetAxis("Horizontal");

            if (movement > 0.0f || movement < 0.0f)
            {
                Accelerate();
                playerMoving = true;
            }
            else if (movement == 0.0f)
            {
                Decelerate();
            }

            if (playerMoving)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    maxSpeed = 8;
                    Debug.Log("running");
                }
                else
                {
                    maxSpeed = 5;
                }
            }
        }

        //Debug.Log(playerMoving);
    }

    private void Accelerate()
    {
        if (speed < maxSpeed)
        {
            speed += acceleration * Time.deltaTime;
        }
        else if (speed > maxSpeed)
        {
            speed -= stoppingForce * Time.deltaTime;
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        rb.velocity = new Vector3(movement * speed, 0, 0);

        lastMovement = movement;
    }

    private void Decelerate()
    {
        speed -= stoppingForce * Time.deltaTime;

        rb.velocity = new Vector3(lastMovement * speed, 0, 0);

        playerMoving = false;

        if (speed <= 0.0f)
        {
            speed = 0.0f;
            lastMovement = 0;
            playerMoving = false;
        }
    }
}
