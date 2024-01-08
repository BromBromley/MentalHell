using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.SceneManagement;

/*
    This Script will be attached to an Empty GameObject in the Scene
    Containing all the various Sounds in the scene in the arrays

    To play a specific sound use this syntax: FindObjectOfType<AudioManager>().PlayOnce(sound array)
*/

public class AudioManager : MonoBehaviour
{
    // make Audiomanager Accessable from everywhere
    public static AudioManager AudioManagerInstance;

    // chance system for Soundtrack to play
    [HideInInspector]
    public Sound[] soundArrayChance;
    public int invokeChance;
    

    // creating Arrays for the two Sound types to be accessable for other scripts
    // this is the class created ub the "Sound.cs" file 
    public Sound[]  musicSoundtrack,
                    sfxAmbience,
                    sfxOpenDoor,
                    sfxCloseDoor,
                    sfxHerzNehmen,
                    sfxHerzAbgeben,
                    sfxNPCGeisterBefreit,
                    sfxMonster,
                    sfxMonsterPatrol,
                    sfxMisc;




    // create AudioSources for the two busses
    //public AudioSource musicSource, sfxSource;

    // additional transfer Audio Source Variable
    private AudioSource source;

    // get the gameobjects determining where the sound is played
    private GameObject soundtrackEmpty;
    private GameObject SFXEmpty;
    private GameObject ghostEmpty;
    private GameObject monsterEmpty;
    private GameObject emptySoundGameObject;

    // create singleton of object so it doesnt get destroyed
    private void Awake (){
        if (AudioManagerInstance == null){
            AudioManagerInstance = this; // set the Audiomanager Instance to this object
            DontDestroyOnLoad(gameObject); // maintain AudioManager Empty through new scene loading
        }
        else {
            Destroy(gameObject);
        }
    }

    // initiate basis Sounds and get GameObjects
    public void InitializeAudio(){

        Debug.Log("sdfghjk");

        // Start playing Ambience on game start
        PlayRandomConstantly(sfxAmbience);

        // Invoke Chance to Play Soundtrack in given time
        InvokeChanceToPlay(30f, 10f, musicSoundtrack, 3);

        // Invoke Chance to Play Monster Sound
        //InvokeChanceToPlay(3f, 3f, sfxMonster, 3);
        // start Monster Walk sound
        PlayRandomConstantly(sfxMonsterPatrol);
  
    }


    /*

        Create Audio Source with Copied Data

    */

    public void CopyAudioDataToEmptyPlayConstantly (Sound sound, GameObject emptySoundGameObject)
    {

        // copy all the information previously set, over to the AudioSource that will play it
        source = emptySoundGameObject.AddComponent<AudioSource>();
        source.clip = sound.clip;
        source.volume = sound.volume;
        source.pitch = sound.pitch;
        source.loop = sound.loop;
        source.outputAudioMixerGroup = sound.output;

        source.spatialBlend = sound.spatialBlend;
        source.maxDistance = sound.maxDistance;
        source.minDistance = sound.minDistance;
        source.rolloffMode = sound.rolloffMode;

        // play the sound constantly
        source.Play();
    
    }

    public void CopyAudioDataToEmptyPlayOnce (Sound sound, GameObject emptySoundGameObject)
    {

        // copy all the information previously set, over to the AudioSource that will play it
        source = emptySoundGameObject.AddComponent<AudioSource>();
        source.clip = sound.clip;
        source.volume = sound.volume;
        source.pitch = sound.pitch;
        source.loop = sound.loop;
        source.outputAudioMixerGroup = sound.output;

        source.spatialBlend = sound.spatialBlend;
        source.maxDistance = sound.maxDistance;
        source.minDistance = sound.minDistance;
        source.rolloffMode = sound.rolloffMode;

        // play the sound once
        source.PlayOneShot(sound.clip);

        // get length of audio clip
        float clipLength = source.clip.length;

        // Destroy the Audio Source Element after it has been played
        Destroy(source, clipLength);
    
    }



    /*

        Play Sound

    */

    public void PlayConstantly (string name, Sound[] soundArray){

        // find the audio file in the array by looking for its name
        Sound sound = Array.Find(soundArray, x => x.name == name);

        // if no audio file with the name can be found return here
        if (sound == null) return;

        //  find out how the sound has to get routed (music or sfx)
        emptySoundGameObject = GetSoundType(soundArray);


        // Create a new audio component on the dedicated empty, Copy the Data over and play the sound
        CopyAudioDataToEmptyPlayConstantly(sound, emptySoundGameObject);

    }

    public void PlayOnce (string name, Sound[] soundArray){
        Sound sound = Array.Find(soundArray, x => x.name == name); // ?

        if (sound == null) return;

        //  find out how the sound has to get routed (music or sfx)
        emptySoundGameObject = GetSoundType(soundArray);

        // Create a new audio component on the dedicated empty, Copy the Data over and play the sound
        CopyAudioDataToEmptyPlayOnce(sound, emptySoundGameObject);

    }

    //  get the type of the sound and return the gameobject it has to get attached to
    public GameObject GetSoundType (Sound[] soundArray){

        // get the GameObject determining where the sound is played
        soundtrackEmpty = GameObject.FindWithTag("SoundtrackEmpty");
        SFXEmpty = GameObject.FindWithTag("SFXEmpty");
        ghostEmpty = GameObject.FindWithTag("GhostEmpty");
        monsterEmpty = GameObject.FindWithTag("MonsterEmpty");

        // get type of first element in array
        string soundtype = soundArray[0].type;

        // determine the GameObject that is supposed to play the sound
        if (soundtype == "sfx") 
        {
            emptySoundGameObject = SFXEmpty;
        }
        else if (soundtype == "music")
        {
            emptySoundGameObject = soundtrackEmpty;
        }
        else if (soundtype == "ghost")
        {
            emptySoundGameObject = ghostEmpty;
        }
        else if (soundtype == "monster")
        {
            emptySoundGameObject = monsterEmpty;
        }

        // return GameObject that will play the Sound
        return emptySoundGameObject;

    }


    /*

        Play One Random Sound of a Sound Array

    */

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



    /*

        Chance of Playing a Sound

    */

    // Invoke the Playing of a Sound on a Fixed Chance
    public void InvokeChanceToPlay(float invokeTime, float repeatTime, Sound[] soundArray, int chance){
        
        // copy values to Variables
        soundArrayChance = soundArray;
        invokeChance = chance;

        // Start the Invoke of the Chance to play a soundtrack
        InvokeRepeating("ChanceToPlay", invokeTime, repeatTime);

    }

    // Play a Sound Determined by a Fixed Chance
    public void ChanceToPlay(){
        
        // check if A Soundtrack is already playing
        if (soundtrackEmpty.GetComponent<AudioSource>() == null){

            // get a random Number
            System.Random randomRandom = new System.Random();
            int randomValue = randomRandom.Next(1, invokeChance);

            // Play the Soundtrack if you are lucky
            if (randomValue == invokeChance - 1){
                PlayRandomOnce(soundArrayChance);
            }

        }

    }




    /*

        Misc Functions to control the Sound

    */
    
    // Stop the sound from the audio sources getting fed to the two mixer's
    public void StopAllSound(){

        // sfxSource.Stop();
        // musicSource.Stop();

    }

    // Pause the sound from the audio sources getting fed to the two mixer's
    public void PauseAllSound(){

        // sfxSource.Pause();
        // musicSource.Pause();

    }

    // UnPause the sound from the audio sources getting fed to the two mixer's
    public void UnpauseAllSound(){

        // sfxSource.UnPause();
        // musicSource.UnPause();

    }

}


/*
    TODO:
    + Let Each Sound Call create an audio source on the specific empty game object
    + Destroy the game object once the sound is done playing
    + leave it active when it is supposed to be looping
    - 
    
*/
