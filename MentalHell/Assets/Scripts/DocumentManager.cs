using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentManager : MonoBehaviour
{
    // this script activates picked up documents
    // attached to every document

    // TODO connection to documents menu

    [SerializeField] GameObject documentUI;

    void Start()
    {
        documentUI.SetActive(false);
    }

    // called by PlayerInteraction script
    public void ShowDocument()
    {
        documentUI.SetActive(true);
    }
}
