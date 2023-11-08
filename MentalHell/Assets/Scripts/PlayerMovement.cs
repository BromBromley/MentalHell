using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerMovement : MonoBehaviour
{
    // this script manages the player movement

    public bool movementEnabled = true;
    // private bool playerIsRunning = false;

    private Rigidbody rb;

    private float movement;
    private float lastMovement;
    private float speed = 0;
    private float maxSpeed;
    private float acceleration = 3;
    private float stoppingForce = 4;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // update checks the input 
    private void Update()
    {
        if (movementEnabled)
        {
            // this checks if the player is running
            if (Input.GetKey(KeyCode.LeftShift))
            {
                maxSpeed = 12;
                // playerIsRunning = true;
            }
            else
            {
                maxSpeed = 4;
                // playerIsRunning = false;
            }

            movement = Input.GetAxis("Horizontal");
        }
    }

    // fixedUpdate moves the player
    private void FixedUpdate()
    {
        if (movement > 0.0f || movement < 0.0f)
        {
            Accelerate();
        }
        else if (movement == 0.0f)
        {
            Decelerate();
        }
    }

    // this gives the movement a fade in
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

    // this gives the movement a fade out
    private void Decelerate()
    {
        speed -= stoppingForce * Time.deltaTime;

        rb.velocity = new Vector3(lastMovement * speed, 0, 0);

        if (speed <= 0.0f)
        {
            speed = 0.0f;
            lastMovement = 0;
        }
    }
}
