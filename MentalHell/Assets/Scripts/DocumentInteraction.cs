using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentInteraction : MonoBehaviour
{
    // this script destroys the document element from storage once it was opened
    // attached to every DOCUMENT empty

    public void DestroyDocument()
    {
        Destroy(this.gameObject);
    }
}
