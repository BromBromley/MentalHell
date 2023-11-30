using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    // TODO stairs "stairs" tag, walls "wall" tag (+ isTrigger)

    // TODO move (done, tested)
    // TODO distance player (done, tested)
    // TODO terrorRadius (done, no reactions)
    // TODO monster chases player through stairs
    // TODO adjust radius if player is running
    // TODO spawnPoints

    public bool monsterIsChasing;
    private int randomStairs;
    private bool canUseStairs = true;

    private int movementDirection;
    private float movement;
    private float walkingSpeed = 5f;
    private float runningSpeed = 8f;

    [SerializeField] private GameObject player;
    private float distance; // maybe public?

    private PlayerMovement _playerMovement;

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
        Debug.Log(monsterIsChasing);

        // checks the distance between monster and player
        // puts it into three different ranges 
        distance = Vector3.Distance(this.transform.position, player.transform.position);

        if (distance < 5)
        {
            // stage three
            monsterIsChasing = true;
            //Debug.Log("the monster is chasing you");
        }
        else if (distance < 10)
        {
            // stage two
            //Debug.Log("you're getting closer to the monster");
        }
        else if (distance < 15)
        {
            // stage one
            //Debug.Log("you see the monster in the distance");
        }
        else
        {
            monsterIsChasing = false;
        }

        // moves the monster depending on wether it's chasing the player
        if (!monsterIsChasing)
        {
            transform.position = transform.position + new Vector3(movement * walkingSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), runningSpeed * Time.deltaTime);
        }
    }

    // this randomly decides if the monster walks left or right
    private void ChooseDirection()
    {
        movementDirection = Random.Range(1, 3);
        //movementDirection = 1;
        if (movementDirection == 1)
        {
            movement = -1f;
        }
        else
        {
            movement = 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colliding");

        if (other.tag == "Wall")
        {
            movement = -movement;
        }
    }

    // this checks if the monster passes by a staircases and randomly chooses if it uses them
    private void OnTriggerExit(Collider other)
    {
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
