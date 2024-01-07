using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    public GameObject player;
    public Material openStorageMaterial;
    public Material my_material;
    public MeshRenderer my_renderer;
    public bool openStorage;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer my_renderer = GetComponent<MeshRenderer>();
        if (my_renderer != null)
        {
            Material my_material = my_renderer.material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        openStorage = player.GetComponent<PlayerInteraction>().openStorage;
        if (openStorage == true)
        {
            OpenSesame();
        }
    }

    void OpenSesame()
    {
        my_renderer.material = openStorageMaterial;
    }
}
