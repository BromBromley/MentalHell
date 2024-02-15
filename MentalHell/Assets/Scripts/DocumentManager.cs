using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentManager : MonoBehaviour
{
    // this script manages the documents shown in the UI
    // game manager checks for input and opens the inventory

    private List<GameObject> documents;
    private List<GameObject> allDocuments;
    private int index;

    [SerializeField] private GameObject documentList;
    public GameObject documentScreen;
    public bool showingInventory = false;

    void Start()
    {
        documents = new List<GameObject>(GameObject.FindGameObjectsWithTag("Document"));
        allDocuments = new List<GameObject>(GameObject.FindGameObjectsWithTag("Document"));
        CloseAllDocuments();

        for (int i = 0; i < documentList.transform.childCount; i++)
        {
            documentList.transform.GetChild(i).gameObject.SetActive(false);
        }
        documentScreen.SetActive(false);
    }

    // picks a random document to show when the player interacts with storage
    public void ShowRandomDocument()
    {
        index = Random.Range(0, documents.Count);
        documents[index].SetActive(true);
        documentList.transform.Find(documents[index].name).gameObject.SetActive(true);
    }

    // opens or closes the screen, called by GameManager
    public void OpenInventory()
    {
        documentScreen.SetActive(true);
        showingInventory = true;
    }
    public void CloseInventory()
    {
        documentScreen.SetActive(false);
        showingInventory = false;
    }

    public void CloseAllDocuments()
    {
        foreach (GameObject document in allDocuments)
        {
            document.SetActive(false);
        }
        foreach (GameObject document in documents)
        {
            document.SetActive(false);
        }
    }

    public void CloseDocument()
    {
        documents[index].SetActive(false);
        documents.RemoveAt(index);
    }
}
