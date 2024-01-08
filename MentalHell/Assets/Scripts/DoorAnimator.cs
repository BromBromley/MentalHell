using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    public Renderer rend;
    public GameObject Mesh;
    public Animator animator;
    public bool playOpenDoor;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playOpenDoor = player.GetComponent<PlayerInteraction>().playOpenDoor;
        if (playOpenDoor == true)
        {
            //animator.SetBool("Open", true);
            rend = Mesh.GetComponent<MeshRenderer>();
            rend.enabled = false;
        }
    }
}
