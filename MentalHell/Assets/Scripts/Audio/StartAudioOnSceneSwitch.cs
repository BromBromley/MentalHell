using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAudioOnSceneSwitch : MonoBehaviour
{
    private AudioManager audioManagerScript;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioManagerScript = FindObjectOfType<AudioManager>();
        audioManagerScript.GetComponent<AudioManager>().InitializeAudio();
    }

}
