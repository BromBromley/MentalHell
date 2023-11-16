using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    // this script manages the ability to switch between past and present

    public float sanityLevel = 5f;
    public bool isSwitching = false;
    private bool canSwitch = true;

    private void Update()
    {
        // this function checks when the player switches to the past and changes the player's position accordingly
        if (Input.GetKeyDown(KeyCode.Space) && canSwitch)
        {
            if (isSwitching == false)
            {
                isSwitching = true;
                this.gameObject.transform.position = transform.position + new Vector3(0.0f, 33.2f, 0.0f);
                //Debug.Log("you're now in the past");
                StartCoroutine(SwitchingCooldown());
            }
            else
            {
                isSwitching = false;
                this.gameObject.transform.position = transform.position + new Vector3(0.0f, -33.2f, 0.0f);
                //Debug.Log("you're back in the present");
                StartCoroutine(SwitchingCooldown());
            }
        }
    }

    private void FixedUpdate()
    {
        // this function updates the sanity level 
        if (isSwitching)
        {
            sanityLevel -= Time.deltaTime;
        }
        else
        {
            if (sanityLevel <= 5)
            {
                sanityLevel += Time.deltaTime;
            }
        }

        // this function checks if the sanity level reaches 0 and puts the player back in the present
        if (sanityLevel <= 0)
        {
            isSwitching = false;
            this.gameObject.transform.position = transform.position + new Vector3(0.0f, -33.2f, 0.0f);
            Debug.Log("you're back in the present");
            StartCoroutine(RefillSanity());
        }
    }

    // this stops the player from switching back and forth too fast
    private IEnumerator SwitchingCooldown()
    {
        canSwitch = false;

        yield return new WaitForSeconds(2);

        canSwitch = true;
    }

    // this is a longer cooldown for when the sanity level has reached 0
    private IEnumerator RefillSanity()
    {
        canSwitch = false;
        Debug.Log("refilling sanity");

        yield return new WaitForSeconds(6);

        sanityLevel = 5f;
        canSwitch = true;
        Debug.Log("sanity reset");
    }
}
