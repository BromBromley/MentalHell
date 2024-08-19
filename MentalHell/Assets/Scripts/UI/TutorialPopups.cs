using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopups : MonoBehaviour
{
    [SerializeField] GameObject Space;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Space.SetActive(false);
        }
    }
}
