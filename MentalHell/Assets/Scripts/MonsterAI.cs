using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    // this script manages all the monster behavior except for spawning

    public bool monsterIsChasing;
    private int randomStairs;
    //private bool canUseStairs = true;

    private int movementDirection;
    private float monsterMovement;
    private float walkingSpeed = 3f;
    private float runningSpeed = 7f;

    [SerializeField] public GameObject player;
    public float distance;

    private PlayerMovement _playerMovement;
    private PlayerInteraction _playerInteraction;
    [SerializeField] private GameObject monsterSprite;
    private bool facingLeft = true;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    private void Start()
    {
        ChooseDirection();
    }

    private void FixedUpdate()
    {
        // checks the distance between monster and player
        distance = Vector3.Distance(this.transform.position, player.transform.position);

        // sets the range at which the monster starts chasing the player depending on if they're running or carrying a heart
        if (distance < 8 && !_playerMovement.playerIsRunning)
        {
            monsterIsChasing = true;
        }
        else if (distance < 15 && _playerMovement.playerIsRunning)
        {
            monsterIsChasing = true;
        }
        else if (distance < 20 && _playerInteraction.pickedUpHeart)
        {
            monsterIsChasing = true;
        }
        else
        {
            monsterIsChasing = false;
        }

        // moves the monster depending on whether it's chasing the player
        if (!monsterIsChasing)
        {
            transform.position = transform.position + new Vector3(monsterMovement * walkingSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
        else
        {
            // checks in what direction the monster is running and flips the sprite accordingly
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), runningSpeed * Time.deltaTime);
            if ((transform.position.x - player.transform.position.x) < 0)
            {
                monsterMovement = 1f;
            }
            else
            {
                monsterMovement = -1f;
            }
        }

        // flips the sprite according to the monster's movement
        if (monsterMovement < 0 && !facingLeft)
        {
            FlipSprite();
        }
        else if (monsterMovement > 0 && facingLeft)
        {
            FlipSprite();
        }
    }

    // this randomly decides if the monster walks left or right
    public void ChooseDirection()
    {
        movementDirection = Random.Range(1, 3);
        if (movementDirection == 1)
        {
            monsterMovement = -1f;
        }
        else
        {
            monsterMovement = 1f;
        }
    }

    // flips the monster sprite depending on the movement direction
    private void FlipSprite()
    {
        Vector3 currentScale = monsterSprite.transform.localScale;
        currentScale.x *= -1;
        monsterSprite.transform.localScale = currentScale;

        facingLeft = !facingLeft;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            monsterMovement = -monsterMovement;
        }
        /*
        // this checks if the monster passes by a staircases and randomly chooses if it uses them
        if (other.tag == "Stairs")
        {
            randomStairs = Random.Range(1, 3);
            if (randomStairs == 1 && canUseStairs && !monsterIsChasing)
            {
                StartCoroutine(StairsCoolDown());
                other.GetComponent<StairsManager>().EnterRoom(this.gameObject);
            }
        }*/
    }

    /*
    private IEnumerator StairsCoolDown()
    {
        canUseStairs = false;

        yield return new WaitForSeconds(2);

        canUseStairs = true;
    }
    */
}
