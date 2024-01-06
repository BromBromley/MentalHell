using UnityEngine;
using UnityEngine.Audio;

/*
    Create Class for Sound files that can be created everyw
*/

[System.Serializable]
public class Sound
{
    public string name = "restr";
    public AudioClip clip;
    public UnityEngine.Audio.AudioMixerGroup output; 
    
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;

    public bool loop = true;

    [HideInInspector]
    public AudioSource source;
}

/*

TODO:
- Remove Source from class once the new system is in place

*/