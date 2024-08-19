using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_TutorialPopups : MonoBehaviour
{
    [SerializeField] GameObject Shift;
    [SerializeField] GameObject Left;
    [SerializeField] GameObject Right;

    void Update()
    {
        if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            Left.SetActive(false);
        }
        if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            Right.SetActive(false);
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow))
        {
            Shift.SetActive(false);
        }
    }
}
