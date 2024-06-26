using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwitchManager : MonoBehaviour
{
    // this script manages the ability to switch between past and present

    private GameManager _gameManager;
    private IntroCheckPoint _introCheckPoint;
    public float sanityLevel = 5f;
    public bool isSwitching = false;
    private bool canSwitch = true;
    private bool isIntro;

    [SerializeField] private GameObject fadeEffect;
    [SerializeField] private Slider switchSlider;
    private Image switchBarImage;

    void Start()
    {
        switchBarImage = switchSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();
        _gameManager = FindObjectOfType<GameManager>();
        _introCheckPoint = FindObjectOfType<IntroCheckPoint>();
    }

    private void Update()
    {
            // this function checks when the player switches to the past and changes the player's position accordingly
            if (Input.GetKeyDown(KeyCode.Space) && canSwitch)
            {
                if (isSwitching == false && isIntro == false)
                {
                    fadeEffect.SetActive(true);
                    isSwitching = true;
                    StartCoroutine(TeleportDelay(34.0f));
                    StartCoroutine(SwitchingCooldown());
                    StartCoroutine(_gameManager.SpawnMonster(0.5f));
                }
                else
                {
                    fadeEffect.SetActive(true);
                    isSwitching = false;
                    StartCoroutine(_gameManager.SpawnMonster(0.5f));
                    StartCoroutine(TeleportDelay(-34.0f));
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
        if (isSwitching)
        {
            sanityLevel -= Time.deltaTime;
            switchBarImage.color = Color.Lerp(Color.red, Color.green, switchSlider.value);
        }
        if (sanityLevel <= 5 && !isSwitching)
        {
            sanityLevel += Time.deltaTime;
            if (canSwitch)
            {
                switchBarImage.color = Color.Lerp(Color.red, Color.green, switchSlider.value);
            }
        }

        // this function checks if the sanity level reaches 0 and puts the player back in the present
        if (sanityLevel <= 0)
        {
            // Play Switching Sound
            Sound[] Soundarray = FindObjectOfType<AudioManager>().sfxMisc;
            FindObjectOfType<AudioManager>().PlayOnce("Swap_Sound_01", Soundarray);

            if (isSwitching)
            {
                isSwitching = false;
                _gameManager.SpawnMonster(0.5f);
                StartCoroutine(TeleportDelay(-34.0f));
            }
            StartCoroutine(RefillSanity());
        }

        UpdateSwitchBar();
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

        // changes the color of the switch bar when the cooldown is over
        sanityLevel = 5f;
        canSwitch = true;
        switchBarImage.color = Color.green;
    }

    // updates the switch bar to the match the player's sanity level
    private void UpdateSwitchBar()
    {
        switchSlider.value = sanityLevel / 5;
    }
}
