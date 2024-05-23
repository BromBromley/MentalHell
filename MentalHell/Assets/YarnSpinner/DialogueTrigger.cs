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
    private bool interactable = true;

    public void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }

    private void StartConversation() {
        Debug.Log($"Started conversation with {name}.");
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(dialogueTrigger);
    }

    private void EndConversation() {
        if (isCurrentConversation) {
            isCurrentConversation = false;
            Debug.Log($"Ended conversation with {name}.");
        }
    }

    public void DisableConversation() {
        interactable = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("COLLIDES");
            collision.GetComponent<PlayerInteraction>().OpenInteractableIcon();
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown("e"))
        {
            Debug.Log("ITS GETTING TRIGGERED");
            StartConversation();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInteraction>().CloseInteractableIcon();
        }
    }
}
