using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitIntro : Interactable
{
    public override void Interact()
    {
        SceneManager.LoadScene("Main");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("COLLIDES");
            collision.GetComponent<Intro_PlayerControls>().OpenInteractableIcon();
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Intro_PlayerControls>()?.CloseInteractableIcon();
        }
    }

}
