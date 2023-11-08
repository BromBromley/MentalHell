using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    // this script manages the ability to switch between past and present

    public float sanityLevel = 5f;
    public bool isSwitching = false;
    private bool canSwitch = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canSwitch)
        {
            if (isSwitching == false)
            {
                isSwitching = true;
                this.gameObject.transform.position = transform.position + new Vector3(0.0f, 58.372f, 0.0f);
                Debug.Log("you're now in the past");
            }
            else
            {
                isSwitching = false;
                this.gameObject.transform.position = transform.position + new Vector3(0.0f, -58.372f, 0.0f);
                Debug.Log("you're back in the present");
            }
        }

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


        if (sanityLevel <= 0)
        {
            isSwitching = false;
            canSwitch = false;
            this.gameObject.transform.position = transform.position + new Vector3(0.0f, -58.372f, 0.0f);
            Debug.Log("you're back in the present");
            StartCoroutine(RefillSanity());
        }
    }

    private IEnumerator RefillSanity()
    {
        Debug.Log("refilling sanity");

        yield return new WaitForSeconds(5);

        sanityLevel = 5f;
        canSwitch = true;
        Debug.Log("sanity reset");
    }
}
