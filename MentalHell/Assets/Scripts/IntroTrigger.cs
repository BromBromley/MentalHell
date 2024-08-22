using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Monster;
    [SerializeField] private GameObject Ghosts;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject introTrigger;
    [SerializeField] private GameObject Tutorial;
    [SerializeField] private bool spokeToGhosts;
    void Start()
    {
        Ghosts.SetActive(false);
        Tutorial.SetActive(false);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && spokeToGhosts == false)
        {
            Ghosts.SetActive(true);
        }
        else if (collision.CompareTag("Player") && spokeToGhosts == true)
        {
            Monster.transform.position = new Vector3(17.82f,1.76f,-0.3f);
            Destroy(introTrigger);
            Destroy(Ghosts.GetComponent<DialogueTrigger>());
        }
    }

    void Update()
    {
        spokeToGhosts = Ghosts.GetComponent<DialogueTrigger>().triggeredOnce;
        if (spokeToGhosts == true)
        {
            Player.SendMessage("ActivateSwitchingAbility");
            Tutorial.SetActive(true);
        }
    }
}
