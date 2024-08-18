using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentManager : MonoBehaviour
{
    // this script manages the documents shown in the UI
    // game manager checks for input and opens the inventory

    private List<GameObject> documents;
    private List<GameObject> allDocuments;
    private int index;

    [SerializeField] private GameObject documentList;
    public GameObject documentScreen;
    [SerializeField] private GameObject interactIcon;
    public bool showingInventory = false;

    private PlayerInteraction _playerInteraction;

    //public GameObject switchBar;
    //public GameObject staminaBar;
    //public GameObject heartSlot;
    //public GameObject heartCounter;
    public GameObject docBackground;
    public GameObject closeButton;



    void Awake()
    {
        _playerInteraction = FindObjectOfType<PlayerInteraction>();
        documents = new List<GameObject>(GameObject.FindGameObjectsWithTag("Document"));
        allDocuments = new List<GameObject>(GameObject.FindGameObjectsWithTag("Document"));
    }


    void Start()
    {
        CloseAllDocuments();

        for (int i = 0; i < documentList.transform.childCount; i++)
        {
            documentList.transform.GetChild(i).gameObject.SetActive(false);
        }
        documentScreen.SetActive(false);
        docBackground.SetActive(false);
        closeButton.SetActive(false);
    }



    // picks a random document to show when the player interacts with storage
    public void ShowRandomDocument()
    {
        index = Random.Range(0, documents.Count);
        documents[index].SetActive(true);
        documentList.transform.Find(documents[index].name).gameObject.SetActive(true);
        //switchBar.SetActive(false); staminaBar.SetActive(false); heartSlot.SetActive(false); heartCounter.SetActive(false);
        docBackground.SetActive(true);
        closeButton.SetActive(true);
    }



    // checks if inventory is open, called by GameManager
    public bool ActivatingInventory()
    {
        if (documentScreen.activeSelf)
        {
            CloseInventory();
            return false;
        }

        OpenInventory();
        return true;
    }


    public void OpenInventory()
    {
        documentScreen.SetActive(true);
        showingInventory = true;
        docBackground.SetActive(true);
    }

    public void CloseInventory()
    {
        CloseAllDocuments();
        documentScreen.SetActive(false);
        showingInventory = false;
        docBackground.SetActive(false);
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
        //switchBar.SetActive(true); staminaBar.SetActive(true); heartSlot.SetActive(true); heartCounter.SetActive(true);
        docBackground.SetActive(false);
        closeButton.SetActive(false);
    }


    // called when the player opens a document through the inventory
    public void OpenDocument(GameObject documentButton)
    {
        foreach (GameObject document in allDocuments)
        {
            if (document.name == documentButton.name)
            {
                _playerInteraction.showingDocument = true;
                document.SetActive(true);
                documentScreen.SetActive(false);
                interactIcon.SetActive(false);
                showingInventory = false;
            }
        }
    }
}
