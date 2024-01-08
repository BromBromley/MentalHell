using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentManager : MonoBehaviour
{
    // this script manages the documents shown in the UI

    // TODO connection to documents menu

    private List<GameObject> documents;
    private int index;

    void Start()
    {
        documents = new List<GameObject>(GameObject.FindGameObjectsWithTag("Document"));
        foreach (GameObject document in documents)
        {
            document.SetActive(false);
        }
    }

    public void ShowRandomDocument()
    {
        index = Random.Range(0, documents.Count);
        documents[index].SetActive(true);
    }

    public void CloseDocument()
    {
        documents[index].SetActive(false);
        documents.RemoveAt(index);
    }
}
