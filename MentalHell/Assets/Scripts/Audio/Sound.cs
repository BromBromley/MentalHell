using UnityEngine;
using UnityEngine.Audio;

/*
    Create Class for Sound files that can be created everywhere
*/

[System.Serializable]
public class Sound
{
    public string name = "restr";
    public string type = "sfx";
    public AudioClip clip;
    public UnityEngine.Audio.AudioMixerGroup output;
    
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    public bool loop = true;

    [Range(0f, 1f)]
    public float spatialBlend = 0f; // is a sound's volume determined by range or not
    [Range(0f, 25f)]
    public float maxDistance = 25f;
    [Range(0f, 25f)]
    public float minDistance = 0f;

    public AudioRolloffMode rolloffMode;

}

/*

TODO:
    + Remove Source from class once the new system is in place
    - find path to audiomixergroups and set default group

*/