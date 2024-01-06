using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Collections;

/*
    This Script will be attached to an Empty GameObject in the Scene
    Containing all the various Sounds in the scene in the arrays

    To play a specific sound use this syntax: FindObjectOfType<AudioManager>().PlayOnce(sound array)
*/

public class AudioManager : MonoBehaviour
{
    // make Audiomanager Accessable from everywhere
    public static AudioManager AudioManagerInstance;


    public UnityEngine.Audio.AudioMixerGroup output; 
    

    // creating Arrays for the two Sound types to be accessable for other scripts
    // this is the class created ub the "Sound.cs" file 
    public Sound[]  musicSoundtrack,
                    sfxAmbience,
                    sfxOpenDoor,
                    sfxCloseDoor,
                    sfxHerzNehmen,
                    sfxHerzAbgeben,
                    sfxNPCGeisterBefreit,
                    sfxMisc;


    public AudioSource musicSource, sfxSource;

    // additional transfer Audio Source Variable
    private AudioSource source;

    // get the two gameobjects for sound and music
    private GameObject soundtrackEmpty;
    private GameObject SFXEmpty;
    private GameObject emptySoundGameObject;

    // create singleton of object
    private void Awake (){
        if (AudioManagerInstance == null){
            AudioManagerInstance = this; // set the Audiomanager Instance to this object
            DontDestroyOnLoad(gameObject); // maintain AudioManager Empty through new scene loading
        }
        else {
            Destroy(gameObject);
        }

        // create Audio sources for the various sound arrays
        foreach(Sound s in musicSoundtrack){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach(Sound s in sfxAmbience){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach(Sound s in sfxOpenDoor){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach(Sound s in sfxCloseDoor){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach(Sound s in sfxMisc){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach(Sound s in sfxHerzNehmen){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach(Sound s in sfxHerzAbgeben){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach(Sound s in sfxNPCGeisterBefreit){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start(){

        // get the GameObjects for the two Sound Empties
        soundtrackEmpty = GameObject.FindWithTag("SoundtrackEmpty");
        SFXEmpty = GameObject.FindWithTag("SFXEmpty");

        // Start playing Music / Ambience on game start
        PlayRandomConstantly(musicSoundtrack);
        PlayRandomConstantly(sfxAmbience);

    }

    // Stop the sound from the audio sources getting fed to the two mixer's
    public void StopAllSound(){

        sfxSource.Stop();
        musicSource.Stop();

    }

    // Pause the sound from the audio sources getting fed to the two mixer's
    public void PauseAllSound(){

        sfxSource.Pause();
        musicSource.Pause();

    }

    // UnPause the sound from the audio sources getting fed to the two mixer's
    public void UnpauseAllSound(){

        sfxSource.UnPause();
        musicSource.UnPause();

    }

    public void PlayConstantly (string name, Sound[] soundArray){

        // find the audio file in the array by looking for its name
        Sound sound = Array.Find(soundArray, x => x.name == name);

        // if no audio file with the name can be found return here
        if (sound == null) return;

        //  find out how the sound has to get routed (music or sfx)
        string outputName = sound.output.ToString();
        if (outputName == "Sfx") 
        {
            source = sfxSource;
            emptySoundGameObject = SFXEmpty;
        }
        else if (outputName == "Soundtrack")
        {
            source = musicSource;
            emptySoundGameObject = soundtrackEmpty;
        }

        // Create a new audio component on the dedicated empty, Copy the Data over and play the sound
        CopyAudioDataToEmptyPlayConstantly(source, sound, emptySoundGameObject);

    }

    public void PlayOnce (string name, Sound[] soundArray){
        Sound sound = Array.Find(soundArray, x => x.name == name); // ?

        if (sound == null) return;

        //  find out how the sound has to get routed (music or sfx)
        string outputName = sound.output.ToString();
        if (outputName == "Sfx") 
        {
            source = sfxSource;
            emptySoundGameObject = SFXEmpty;
        }
        else if (outputName == "Soundtrack")
        {
            source = musicSource;
            emptySoundGameObject = soundtrackEmpty;
        }

        // Create a new audio component on the dedicated empty, Copy the Data over and play the sound
        CopyAudioDataToEmptyPlayOnce(source, sound, emptySoundGameObject);

    }

    public void PlayRandomOnce (Sound[] soundArray){
        string soundName;
        int length = soundArray.Length;

        if (length == 0) return;

        soundName = soundArray[UnityEngine.Random.Range(0, length)].name;

        PlayOnce(soundName, soundArray);
    }

    public void PlayRandomConstantly (Sound[] soundArray){
        string soundName;
        int length = soundArray.Length;

        if (length == 0) return;

        soundName = soundArray[UnityEngine.Random.Range(0, length)].name;

        PlayConstantly(soundName, soundArray);
    }


    public void CopyAudioDataToEmptyPlayConstantly (AudioSource source, Sound sound, GameObject emptySoundGameObject)
    {

        // copy all the information previously set, over to the AudioSource that will play it
        //Debug.Log("copy");
        source = emptySoundGameObject.AddComponent<AudioSource>();
        source.clip = sound.clip;
        source.volume = sound.volume;
        source.pitch = sound.pitch;
        source.loop = sound.loop;
        source.outputAudioMixerGroup = sound.output;

        // play the sound constantly
        source.Play();
    
    }

    public void CopyAudioDataToEmptyPlayOnce (AudioSource source, Sound sound, GameObject emptySoundGameObject)
    {

        // copy all the information previously set, over to the AudioSource that will play it
        Debug.Log("copy");
        source = emptySoundGameObject.AddComponent<AudioSource>();
        source.clip = sound.clip;
        source.volume = sound.volume;
        source.pitch = sound.pitch;
        source.loop = sound.loop;
        source.outputAudioMixerGroup = sound.output;

        // play the sound once
        source.PlayOneShot(sound.clip);

        // get length of audio clip
        float clipLength = source.clip.length;
        Debug.Log(clipLength);

        // Destroy the Audio Source Element after it has been played
        Destroy(source, clipLength);
    
    }



}


/*
    TODO:
    + Let Each Sound Call create an audio source on the specific empty game object
    + Destroy the game object once the sound is done playing
    + leave it active when it is supposed to be looping
    - 
    
*/
