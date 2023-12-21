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
        // Start playing Music / Ambience on game start
        PlayRandomConstantly(musicSoundtrack);
        PlayRandomConstantly(sfxAmbience);
    }

    // pause/ stop doesnt work yet (dunno)
    public void StopAllSound(){
        foreach(Sound s in musicSoundtrack){
            s.source.Stop();
        }
        foreach(Sound s in sfxAmbience){
            s.source.Stop();
        }
        foreach(Sound s in sfxOpenDoor){
            s.source.Stop();   
        }
        foreach(Sound s in sfxCloseDoor){
            s.source.Stop();
        }
        return;
    }

    public void PauseAllSound(){
        foreach(Sound s in musicSoundtrack){
            s.source.Pause();
        }
        foreach(Sound s in sfxAmbience){
            s.source.Pause();
        }
        foreach(Sound s in sfxOpenDoor){
            s.source.Pause();   
        }
        foreach(Sound s in sfxCloseDoor){
            s.source.Pause();
        }
        return;
    }

    public void UnpauseAllSound(){
        foreach(Sound s in musicSoundtrack){
            s.source.UnPause();
        }
        foreach(Sound s in sfxAmbience){
            s.source.UnPause();
        }
        foreach(Sound s in sfxOpenDoor){
            s.source.UnPause();   
        }
        foreach(Sound s in sfxCloseDoor){
            s.source.UnPause();
        }
        return;
    }

    public void PlayConstantly (string name, Sound[] soundArray){
        Sound sound = Array.Find(soundArray, x => x.name == name); // ?

        if (sound == null) return;

        sound.source.clip = sound.clip;
        sound.source.Play();
        
    }

    public void PlayOnce (string name, Sound[] soundArray){
        Sound sound = Array.Find(soundArray, x => x.name == name); // ?

        if (sound == null) return;

        sound.source.PlayOneShot(sound.clip);
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
    
}