using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class Interactable : MonoBehaviour
{
    private void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
    public abstract void Interact();

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInteraction>()?.OpenInteractableIcon();
            collision.GetComponent<Intro_PlayerControls>()?.OpenInteractableIcon();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInteraction>()?.CloseInteractableIcon();
            collision.GetComponent<Intro_PlayerControls>()?.CloseInteractableIcon();
        }
    }
}
