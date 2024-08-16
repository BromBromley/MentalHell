using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwitchManager : MonoBehaviour
{
    // this script manages the ability to switch between past and present


    public float sanityLevel = 5f;
    public bool isSwitching = false;
    private bool canSwitch = true;
    // start as false, true once the player enters the building -> makes isIntro unneccessary
    private bool exhaustedSwitching;

    [SerializeField] private GameObject fadeEffect;
    [SerializeField] private Slider switchSlider;
    private Image switchBarImage;

    private AudioManager _audioManager;
    private Sound[] Soundarray;



    void Start()
    {
        switchBarImage = switchSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();

        _audioManager = FindObjectOfType<AudioManager>();
        Soundarray = _audioManager.sfxMisc;

        GameManager.onSwitching += Switch;
    }


    private void Update()
    {
        // this function checks when the player switches to the past and changes the player's position accordingly
        if (Input.GetKeyDown(KeyCode.Space) && canSwitch)
        {
            if (!isSwitching)
            {
                GameManager.onSwitching?.Invoke(true);
            }
            else
            {
                GameManager.onSwitching?.Invoke(false);
            }
        }
    }


    private void FixedUpdate()
    {
        // this function updates the sanity level 
        if (isSwitching)
        {
            sanityLevel -= Time.deltaTime;
            UpdateSwitchBar();
        }
        if (sanityLevel <= 5 && !isSwitching)
        {
            sanityLevel += Time.deltaTime;
            UpdateSwitchBar();
        }

        // this function checks if the sanity level reaches 0 and puts the player back in the present
        if (sanityLevel <= 0 && isSwitching)
        {
            exhaustedSwitching = true;
            GameManager.onSwitching?.Invoke(false);

            // plays Switching sound
            _audioManager.PlayOnce("Swap_Sound_01", Soundarray);
        }
    }



    private void ActivateSwitchingAbility()
    {
        canSwitch = true;
    }



    // this checks in what direction the player is switching
    private void Switch(bool switchUp)
    {
        fadeEffect.SetActive(true);

        if (switchUp)
        {
            isSwitching = true;
            StartCoroutine(TeleportDelay(34.0f));
        }
        else
        {
            isSwitching = false;
            StartCoroutine(TeleportDelay(-34.0f));
        }

        if (exhaustedSwitching)
        {
            StartCoroutine(RefillSanity());
        }
        else
        {
            StartCoroutine(SwitchingCooldown());
        }

        // plays Switching sound
        _audioManager.PlayOnce("Swap_Sound_01", Soundarray);
    }


    // this gives the switching a bit of a delay to match with the fade effect
    private IEnumerator TeleportDelay(float yValue)
    {
        fadeEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        this.gameObject.transform.position = transform.position + new Vector3(0.0f, yValue, 0.0f);
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

        yield return new WaitForSeconds(5);

        sanityLevel = 5f;
        canSwitch = true;
        exhaustedSwitching = false;
        switchBarImage.color = Color.green;
    }



    // updates the switch bar to match the player's sanity level and adjusts the color
    private void UpdateSwitchBar()
    {
        switchSlider.value = sanityLevel / 5;
        switchBarImage.color = Color.Lerp(Color.red, Color.green, switchSlider.value);
    }



    // unsubscribes from the event so it doesn't get called when the scene gets reloaded
    void OnDestroy()
    {
        GameManager.onSwitching -= Switch;
    }
}
