using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    // TODO terrorRadius (done, no reactions)
    // TODO monster chases player through stairs
    // TODO adjust radius if player is running
    // TODO spawnPoints

    public bool monsterIsChasing;
    private int randomStairs;
    private bool canUseStairs = true;

    private int movementDirection;
    private float movement;
    private float walkingSpeed = 2.5f;
    private float runningSpeed = 4f;

    [SerializeField] public GameObject player;
    public float distance; // maybe public?

    private PlayerMovement _playerMovement;
    [SerializeField] private GameObject monsterSprite;
    private bool facingLeft = true;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        ChooseDirection();
    }

    private void FixedUpdate()
    {
        // checks the distance between monster and player
        // puts it into three different ranges 
        distance = Vector3.Distance(this.transform.position, player.transform.position);

        if (distance < 5 && !_playerMovement.playerIsRunning)
        {
            // stage three
            //Debug.Log("the monster is chasing you");
            monsterIsChasing = true;
        }
        else if (distance < 8)
        {
            // stage two
            //Debug.Log("you're getting closer to the monster");
        }
        else if (distance < 10)
        {
            // stage one
            //Debug.Log("you see the monster in the distance");
            if (_playerMovement.playerIsRunning)
            {
                monsterIsChasing = true;
            }
        }
        else
        {
            monsterIsChasing = false;
        }

        // moves the monster depending on whether it's chasing the player
        if (!monsterIsChasing)
        {
            transform.position = transform.position + new Vector3(movement * walkingSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), runningSpeed * Time.deltaTime);
        }

        if (movement < 0 && !facingLeft)
        {
            FlipSprite();
        }
        else if (movement > 0 && facingLeft)
        {
            FlipSprite();
        }
    }

    // this randomly decides if the monster walks left or right
    public void ChooseDirection()
    {
        //Debug.Log("monster starts walking");
        movementDirection = Random.Range(1, 3);
        if (movementDirection == 1)
        {
            movement = -1f;
        }
        else
        {
            movement = 1f;
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
            movement = -movement;
        }

        // this checks if the monster passes by a staircases and randomly chooses if it uses them
        if (other.tag == "Stairs")
        {
            randomStairs = Random.Range(1, 3);
            if (randomStairs == 1 && canUseStairs && !monsterIsChasing)
            {
                StartCoroutine(StairsCoolDown());
                other.GetComponent<DoorManager>().EnterRoom(this.gameObject);
            }
        }
    }

    private IEnumerator StairsCoolDown()
    {
        canUseStairs = false;

        yield return new WaitForSeconds(2);

        canUseStairs = true;
    }
}
