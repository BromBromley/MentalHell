using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro_PlayerControls : MonoBehaviour
{
    // this script manages the movement and interaction of the player in the scene outside

    [SerializeField] private GameObject playerSprite;
    private bool facingLeft = true;

    public bool movementEnabled = true;
    private bool playerCanRun = true;
    public bool playerIsRunning = false;

    private Rigidbody rb;

    private float movement;
    private float lastMovement;
    private float speed = 0;
    private float maxSpeed;
    private float stoppingForce = 7;
    public Animator animator;

    private float staminaLevel = 5f;
    [SerializeField] private Slider staminaSlider;

    [SerializeField] private GameObject interactIcon;
    [SerializeField] private GameObject fadeEffect;
    [SerializeField] private GameObject entrance_door;
    public bool playOpenDoor;



    void Start()
    {
        rb = GetComponent<Rigidbody>();

        interactIcon.SetActive(false);
    }


    // Update checks for input from the player
    private void Update()
    {
        if (movementEnabled)
        {
            // this checks if the player is running and adjusts the speed
            if (Input.GetKey(KeyCode.LeftShift) && playerCanRun && Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D))
            {
                maxSpeed = 10;
                playerIsRunning = true;
            }
            else
            {
                maxSpeed = 6;
                playerIsRunning = false;
            }

            movement = Input.GetAxis("Horizontal");
        }
        else
        {
            playerIsRunning = false;
        }
        animator.SetFloat("speed", Mathf.Abs(movement));
        animator.SetBool("running", playerIsRunning);
    }


    // FixedUpdate moves the player
    private void FixedUpdate()
    {
        if (movement > 0.0f || movement < 0.0f)
        {
            MovePlayer();
        }
        else if (movement == 0.0f)
        {
            Decelerate();
        }

        // checks if the player is running and updates the stamina accordingly
        if (playerIsRunning)
        {
            staminaLevel -= Time.deltaTime;
            UpdateStaminaBar();
        }
        if (staminaLevel <= 5 && !playerIsRunning)
        {
            staminaLevel += Time.deltaTime;
            UpdateStaminaBar();
        }

        if (staminaLevel <= 0)
        {
            StartCoroutine(RunningCooldown());
        }

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



    private void MovePlayer()
    {
        rb.velocity = new Vector3(movement * maxSpeed, 0, 0);
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


    // flips the player sprite depending on the movement direction
    private void FlipSprite()
    {
        Vector3 currentScale = playerSprite.transform.localScale;
        currentScale.x *= -1;
        playerSprite.transform.localScale = currentScale;

        facingLeft = !facingLeft;
    }



    // stops the movement when player goes through a door
    public IEnumerator StopMovement()
    {
        movementEnabled = false;
        yield return new WaitForSeconds(0.1f);
        movement = 0;
        yield return new WaitForSeconds(0.6f);
        movementEnabled = true;
    }



    // short cooldown when the stamina runs out
    private IEnumerator RunningCooldown()
    {
        playerCanRun = false;
        yield return new WaitForSeconds(1);
        playerCanRun = true;
    }



    // updates the stamina bar to match the player's stamina level
    private void UpdateStaminaBar()
    {
        staminaSlider.value = staminaLevel / 5;
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "MainEntrance")
        {
            if (Input.GetKey(KeyCode.E))
            {
                playOpenDoor = true;
                SceneManager.LoadScene("Main");
                fadeEffect.SetActive(true);
                StartCoroutine(StopMovement());
            }
        }
    }

    // this manages the interact icon while the player stands in a collider
    public void OpenInteractableIcon()
    {
        Debug.Log("open");
        interactIcon.SetActive(true);
    }

    public void CloseInteractableIcon()
    {
        interactIcon.SetActive(false);
    }
}
