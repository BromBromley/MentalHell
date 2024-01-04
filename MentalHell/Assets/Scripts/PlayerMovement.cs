using System.Collections;
using System.Collections.Generic;
using System.Data;

//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // this script manages the player movement

    [SerializeField] private GameObject playerSprite;
    private bool facingLeft = true;

    public bool movementEnabled = true;
    public bool playerCanRun = true;
    public bool playerIsRunning = false;

    private Rigidbody rb;

    private float movement;
    private float lastMovement;
    private float speed = 0;
    private float maxSpeed;
    private float acceleration;
    private float stoppingForce = 7;
    public Animator animator;

    private float staminaLevel = 5f;
    [SerializeField] private Slider staminaSlider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // update checks the input 
    private void Update()
    {
        if (movementEnabled)
        {
            // this checks if the player is running and adjusts the speed
            if (Input.GetKey(KeyCode.LeftShift) && playerCanRun)
            {
                maxSpeed = 10;
                acceleration = 5;
                playerIsRunning = true;
            }
            else
            {
                maxSpeed = 6;
                acceleration = 3;
                playerIsRunning = false;
            }

            movement = Input.GetAxis("Horizontal");
        }
        animator.SetFloat("speed", Mathf.Abs(movement));
        animator.SetBool("running", playerIsRunning);
    }

    // fixedUpdate moves the player
    private void FixedUpdate()
    {
        if (movement > 0.0f || movement < 0.0f)
        {
            //Accelerate();
            MovePlayer();
        }
        else if (movement == 0.0f)
        {
            Decelerate();
        }

        // checks if the player is running and updates the stamina accordingly
        // no cooldown when it runs out
        if (playerIsRunning)
        {
            staminaLevel -= Time.deltaTime;
        }
        else
        {
            if (staminaLevel <= 5 && !playerIsRunning)
            {
                staminaLevel += Time.deltaTime;
            }
        }

        UpdateStaminaBar();

        // this checks the player's direction and flips the sprite accordingly
        if (movement < 0 && !facingLeft)
        {
            FlipSprite();
        }
        else if (movement > 0 && facingLeft)
        {
            FlipSprite();
        }
    }

    // moves player without acceleration
    private void MovePlayer()
    {
        rb.velocity = new Vector3(movement * maxSpeed, 0, 0);
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

    // updates the stamina bar to match the player's stamina level
    private void UpdateStaminaBar()
    {
        staminaSlider.value = staminaLevel / 5;
    }

    // flips the player sprite depending on the movement direction
    private void FlipSprite()
    {
        Vector3 currentScale = playerSprite.transform.localScale;
        currentScale.x *= -1;
        playerSprite.transform.localScale = currentScale;

        facingLeft = !facingLeft;
    }
}
