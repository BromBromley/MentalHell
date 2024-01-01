using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwitchManager : MonoBehaviour
{
    // this script manages the ability to switch between past and present

    private PlayerMovement _playerMovement;
    public float sanityLevel = 5f;
    public bool isSwitching = false;
    private bool canSwitch = true;

    [SerializeField] private Slider switchSlider;
    private Image switchBarImage;

    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        switchBarImage = switchSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();
    }

    private void Update()
    {
        // this function checks when the player switches to the past and changes the player's position accordingly
        if (Input.GetKeyDown(KeyCode.Space) && canSwitch)
        {
            if (isSwitching == false)
            {
                isSwitching = true;
                this.gameObject.transform.position = transform.position + new Vector3(0.0f, 34.0f, 0.0f);
                //Debug.Log("you're now in the past");
                StartCoroutine(SwitchingCooldown());
            }
            else
            {
                isSwitching = false;
                this.gameObject.transform.position = transform.position + new Vector3(0.0f, -34.0f, 0.0f);
                //Debug.Log("you're back in the present");
                StartCoroutine(SwitchingCooldown());
            }

            // Play Switching Sound
            Sound[] Soundarray = FindObjectOfType<AudioManager>().sfxMisc;
            FindObjectOfType<AudioManager>().PlayOnce("Swap_Sound_01", Soundarray);
        }
    }

    private void FixedUpdate()
    {
        // this function updates the sanity level 
        if (isSwitching || _playerMovement.playerIsRunning)
        {
            sanityLevel -= Time.deltaTime;
        }
        else
        {
            if (sanityLevel <= 5 && !isSwitching && !_playerMovement.playerIsRunning)
            {
                sanityLevel += Time.deltaTime;
            }
        }

        // this function checks if the sanity level reaches 0 and puts the player back in the present
        if (sanityLevel <= 0)
        {
            if (isSwitching)
            {
                isSwitching = false;
                this.gameObject.transform.position = transform.position + new Vector3(0.0f, -34.0f, 0.0f);
            }
            Debug.Log("you're back in the present");
            StartCoroutine(RefillSanity());
        }

        UpdateSwitchBar();
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
        switchBarImage.color = Color.red;
        _playerMovement.playerCanRun = false;

        yield return new WaitForSeconds(5);

        // changes the color of the switch bar when the cooldown is over
        sanityLevel = 5f;
        canSwitch = true;
        switchBarImage.color = Color.green;
        _playerMovement.playerCanRun = true;
    }

    // updates the switch bar to the match the player's sanity level
    private void UpdateSwitchBar()
    {
        switchSlider.value = sanityLevel / 5;
    }
}
