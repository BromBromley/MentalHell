using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour

{

public GameObject pauseScreen;
public bool hide;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            hide = !hide;
            if (hide)
            {
                pauseScreen.SetActive(false);
            }
            else pauseScreen.SetActive(true);
        }

    }
}
