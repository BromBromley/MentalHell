using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundInteraction : MonoBehaviour
{
    private bool OpenStorage = false;
    public GameObject Player;
    public Animator animator;
    private float PlayerX;
    private float StorageX;

    // Start is called before the first frame update
    void Start()
    {
        StorageX = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerX = Player.transform.position.x;
        if (Input.GetKey(KeyCode.E) && (PlayerX % StorageX <= 1))
        {
            OpenStorage = true;
            animator.SetBool("open", OpenStorage);
        }
    }
}
