using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class StorageManager : Interactable
{
    public Material open;
    public Material closed;

    private MeshRenderer mr;

    public override void Interact()
    {
        mr.material = open;
    }

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mr.material = closed;
    }
}
