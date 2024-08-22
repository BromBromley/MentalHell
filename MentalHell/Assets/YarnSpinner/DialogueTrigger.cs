using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(BoxCollider))]
public class DialogueTrigger : MonoBehaviour
{

    [SerializeField] public string dialogueTrigger;
    [SerializeField] private string updatedDialogueTrigger;
    private DialogueRunner dialogueRunner;
    private bool isCurrentConversation = false;
    private bool interactable;
    public bool isEvent;
    public bool triggeredOnce;
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
        if (updatedDialogueTrigger != "null" && triggeredOnce == true)
        {
            dialogueRunner.StartDialogue(updatedDialogueTrigger);
        }
        else
        {
            dialogueRunner.StartDialogue(dialogueTrigger);
            triggeredOnce = true;
        }
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
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
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
