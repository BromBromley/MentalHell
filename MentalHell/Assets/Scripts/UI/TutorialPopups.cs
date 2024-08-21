using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopups : MonoBehaviour
{
    [SerializeField] GameObject Space;
    [SerializeField] GameObject I;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Space.SetActive(false);
        }
        if (Input.GetKeyDown("i"))
        {
            I.SetActive(false);
        }
    }
}
