using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(BoxCollider))]
public class DialogueTrigger : MonoBehaviour
{

    [SerializeField] private string dialogueTrigger;
    private DialogueRunner dialogueRunner;
    private bool isCurrentConversation = false;
    private bool interactable;
    public bool isEvent;
    private bool isInteracting = false;

    public void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }

    private void StartConversation()
    {
        //Debug.Log($"Started conversation with {name}.");
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(dialogueTrigger);
    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
            isInteracting = false;
            dialogueRunner.Stop();
            //Debug.Log($"Ended conversation with {name}.");
        }
    }

    public void DisableConversation()
    {
        interactable = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && isEvent == false)
        {
            //Debug.Log("COLLIDES");
            collision.GetComponent<Intro_PlayerControls>()?.OpenInteractableIcon();
            collision.GetComponent<PlayerInteraction>()?.OpenInteractableIcon();
        }
        else if (collision.CompareTag("Player") && isEvent == true)
        {
            StartConversation();
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey("e"))
        {
            if (isInteracting == true) return;
            //Debug.Log("ITS GETTING TRIGGERED");
            StartConversation();
            isInteracting = true;
        }
        if (collision.CompareTag("Player") && Input.GetKey("space") | Input.GetKey("escape"))
        {
            EndConversation();
            isInteracting = false;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInteraction>()?.CloseInteractableIcon();
            collision.GetComponent<Intro_PlayerControls>()?.CloseInteractableIcon();
            PlayerWalksOff();
        }
    }

    public void PlayerWalksOff()
    {
        EndConversation();
        isInteracting = false;
    }
}
