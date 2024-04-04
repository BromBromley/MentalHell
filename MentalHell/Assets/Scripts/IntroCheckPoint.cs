using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCheckPoint : MonoBehaviour
{
    public bool isIntro = true;
    Collider b_collider;
    
    void Start()
    {
        b_collider = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        b_collider.enabled = false;
        isIntro = false;
    }
}
