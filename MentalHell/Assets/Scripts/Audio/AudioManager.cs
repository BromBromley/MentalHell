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
    public Sound[] soundArrayChanceSoundtrack, soundArrayChanceMonster, soundArrayChanceGhosts;
    public int invokeChance;

    private bool Paused = false;
    

    // creating Arrays for the two Sound types to be accessable for other scripts
    // this is the class created ub the "Sound.cs" file 
    public Sound[]  musicSoundtrack,
                    sfxAmbience,
                    sfxOpenDoor,
                    sfxCloseDoor,
                    sfxStairsUp,
                    sfxStairsDown,
                    sfxMetallSchrankOffnen,
                    sfxHerzNehmen,
                    sfxHerzAbgeben,
                    sfxNPCGhostsIdle,
                    sfxNPCGeisterBefreit,
                    sfxStepsWalk,
                    sfxStepsRun,
                    sfxMonster,
                    sfxMonsterPatrol,
                    sfxHerzAmbience,
                    sfxMisc;

    // additional transfer Audio Source Variable
    private AudioSource source;

    // get the gameobjects determining where the sound is played
    private GameObject soundtrackEmpty;
    private GameObject SFXEmpty;
    private GameObject ghostEmpty;
    private GameObject monsterEmpty;
    private GameObject[] hearts;
    private GameObject[] emptySoundGameObjects = new GameObject[1];

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

        // get the GameObject determining where the sound is played
        soundtrackEmpty = GameObject.FindWithTag("SoundtrackEmpty");
        SFXEmpty = GameObject.FindWithTag("SFXEmpty");
        ghostEmpty = GameObject.FindWithTag("GhostEmpty");
        monsterEmpty = GameObject.FindWithTag("MonsterEmpty");
        hearts = GameObject.FindGameObjectsWithTag("Heart");

        // stop all Invokes
        CancelInvoke();

        // delete all audio sources of objects that wont get destroyed on scene reload
        GameObject[] gameobjects = {soundtrackEmpty, SFXEmpty};
        AudioSource[] audioSources;
        int gameObjectCounter = 0;
        for(int i = 0; i < gameobjects.Length; i++)
        {

            // get all audiosources of the object
            audioSources = gameobjects[gameObjectCounter].GetComponents<AudioSource>();
            gameObjectCounter++;
            
            // destroy all audiosources and reset the audioSources Array
            for (int x = 0; x < audioSources.Length; x++){
                Destroy(audioSources[x], 0);
                audioSources[x] = null;
            }
            audioSources = null;

        }

        if (SceneManager.GetActiveScene().buildIndex == 0){

            // Play Main in a loop Theme when in the main menu
            musicSoundtrack[0].loop = true;
            PlayConstantly("MH_Musik_1", musicSoundtrack);

        }
        else if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            // turn off loop characteristic of main theme
            musicSoundtrack[0].loop = false;

            // Start playing Ambience on game start
            PlayRandomConstantly(sfxAmbience);

            // Invoke Chance to Play Soundtrack in given time
            InvokeChanceToPlaySoundtrack(15f, 40f, musicSoundtrack, 3);

            // Invoke Chance to Play Monster Sound
            InvokeChanceToPlayMonster(3f, 3f, sfxMonster, 3);

            // start Monster Walk sound
            PlayRandomConstantly(sfxMonsterPatrol);

            // start heartbeats on all hearts
            PlayRandomConstantly(sfxHerzAmbience);

            // start chance of Ghosts Screamin
            InvokeChanceToPlayGhosts(5f, 10f, sfxNPCGhostsIdle, 3);

        }
  
    }


    /*

        Create Audio Source with Copied Data

    */

    public void CopyAudioDataToEmptyPlayConstantly (Sound sound, GameObject[] emptySoundGameObjects)
    {

        for(int i = 0; i < emptySoundGameObjects.Length; i++)
        {

            // copy all the information previously set, over to the AudioSource that will play it
            source = emptySoundGameObjects[i].AddComponent<AudioSource>();
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
    
    }

    public void CopyAudioDataToEmptyPlayOnce (Sound sound, GameObject[] emptySoundGameObjects)
    {

        // copy all the information previously set, over to the AudioSource that will play it
        source = emptySoundGameObjects[0].AddComponent<AudioSource>();
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
        if (sound == null || Paused == true) return;

        //  find out how the sound has to get routed (music or sfx)
        emptySoundGameObjects = GetSoundType(soundArray);


        // Create a new audio component on the dedicated empty, Copy the Data over and play the sound
        CopyAudioDataToEmptyPlayConstantly(sound, emptySoundGameObjects);

    }

    public void PlayOnce (string name, Sound[] soundArray){
        Sound sound = Array.Find(soundArray, x => x.name == name); // ?

        if (sound == null || Paused == true) return;

        //  find out how the sound has to get routed (music or sfx)
        emptySoundGameObjects = GetSoundType(soundArray);

        // Create a new audio component on the dedicated empty, Copy the Data over and play the sound
        CopyAudioDataToEmptyPlayOnce(sound, emptySoundGameObjects);

    }

    //  get the type of the sound and return the gameobject it has to get attached to
    public GameObject[] GetSoundType (Sound[] soundArray){

        // get the GameObject determining where the sound is played
        soundtrackEmpty = GameObject.FindWithTag("SoundtrackEmpty");
        SFXEmpty = GameObject.FindWithTag("SFXEmpty");
        ghostEmpty = GameObject.FindWithTag("GhostEmpty");
        monsterEmpty = GameObject.FindWithTag("MonsterEmpty");
        hearts = GameObject.FindGameObjectsWithTag("Heart");

        // get type of first element in array
        string soundtype = soundArray[0].type;

        // Delete Array (length is important and not all sounds need to be played on multiple objects)
        Array.Clear(emptySoundGameObjects, 0, emptySoundGameObjects.Length);

        // set Array length to 1
        // if it needs to be longer it will be set to the array of gameobjects itself
        // which will increase the array length automatically
        emptySoundGameObjects = new GameObject[1];

        // determine the GameObject that is supposed to play the sound
        if (soundtype == "sfx") 
        {
            emptySoundGameObjects[0] = SFXEmpty;
        }
        else if (soundtype == "music")
        {
            emptySoundGameObjects[0] = soundtrackEmpty;
        }
        else if (soundtype == "ghost")
        {
            emptySoundGameObjects[0] = ghostEmpty;
        }
        else if (soundtype == "monster")
        {
            emptySoundGameObjects[0] = monsterEmpty;
        }
        else if (soundtype == "heart")
        {
            emptySoundGameObjects = hearts;
        }

        // return GameObject that will play the Sound
        return emptySoundGameObjects;

    }


    /*

        Play One Random Sound of a Sound Array

    */

    public void PlayRandomOnce (Sound[] soundArray){

        if (soundArray == null) return;

        string soundName;
        int length = soundArray.Length;
      
        soundName = soundArray[UnityEngine.Random.Range(0, length)].name;

        PlayOnce(soundName, soundArray);
    }

    public void PlayRandomConstantly (Sound[] soundArray){

        if (soundArray == null) return;

        string soundName;
        int length = soundArray.Length;

        soundName = soundArray[UnityEngine.Random.Range(0, length)].name;

        PlayConstantly(soundName, soundArray);
    }



    /*

        Chance of Playing a Sound

    */

    // Soundtrack Chance
    public void InvokeChanceToPlaySoundtrack(float invokeTime, float repeatTime, Sound[] soundArray, int chance){
        
        // copy values to Variables
        soundArrayChanceSoundtrack = soundArray;
        invokeChance = chance;

        // Start the Invoke of the Chance to play a soundtrack
        InvokeRepeating("ChanceToPlaySoundtrack", invokeTime, repeatTime);

    }
    public void ChanceToPlaySoundtrack(){
        
        // check if A Soundtrack is already playing
        if (soundtrackEmpty.GetComponent<AudioSource>() == null){

            // get a random Number
            System.Random randomRandom = new System.Random();
            int randomValue = randomRandom.Next(1, invokeChance);

            // Play the Soundtrack if you are lucky
            if (randomValue == invokeChance - 1){
                PlayRandomOnce(soundArrayChanceSoundtrack);
            }

        }

    }

    // Monster Chance
    public void InvokeChanceToPlayMonster(float invokeTime, float repeatTime, Sound[] soundArray, int chance){
        
        // copy values to Variables
        soundArrayChanceMonster = soundArray;
        invokeChance = chance;

        // Start the Invoke of the Chance to play
        InvokeRepeating("ChanceToPlayMonster", invokeTime, repeatTime);

    }
    public void ChanceToPlayMonster(){
        
        // get a random Number
        System.Random randomRandom = new System.Random();
        int randomValue = randomRandom.Next(1, invokeChance);

        // Play if you are lucky
        if (randomValue == invokeChance - 1){
            PlayRandomOnce(soundArrayChanceMonster);
        }

    }

    // Ghosts Chance
    public void InvokeChanceToPlayGhosts(float invokeTime, float repeatTime, Sound[] soundArray, int chance){
        
        // copy values to Variables
        soundArrayChanceGhosts = soundArray;
        invokeChance = chance;

        // Start the Invoke of the Chance to play
        InvokeRepeating("ChanceToPlayGhosts", invokeTime, repeatTime);

    }
    public void ChanceToPlayGhosts(){
        
        // get a random Number
        System.Random randomRandom = new System.Random();
        int randomValue = randomRandom.Next(1, invokeChance);

        // Play if you are lucky
        if (randomValue == invokeChance - 1){
            PlayRandomOnce(soundArrayChanceGhosts);
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

        // get the GameObject determining where the sound is played
        soundtrackEmpty = GameObject.FindWithTag("SoundtrackEmpty");
        SFXEmpty = GameObject.FindWithTag("SFXEmpty");
        ghostEmpty = GameObject.FindWithTag("GhostEmpty");
        monsterEmpty = GameObject.FindWithTag("MonsterEmpty");
        hearts = GameObject.FindGameObjectsWithTag("Heart");

        // stop new sounds from getting created (eg swap runs out in the background etc)
        Paused = true;

        // Pause all audio sources
        GameObject[] singlegameobjects = {soundtrackEmpty, SFXEmpty, ghostEmpty, monsterEmpty};

        // create new array including all objects that have audiosources
        GameObject[] gameobjects = new GameObject[singlegameobjects.Length + hearts.Length];
        singlegameobjects.CopyTo(gameobjects, 0);
        hearts.CopyTo(gameobjects, singlegameobjects.Length);

        AudioSource[] audioSources;
        int gameObjectCounter = 0;

        for(int i = 0; i < gameobjects.Length; i++)
        {

            // get all audiosources of the object
            audioSources = gameobjects[gameObjectCounter].GetComponents<AudioSource>();
            gameObjectCounter++;
           
            // Pause all audiosources and reset the audioSources Array
            for (int x = 0; x < audioSources.Length; x++){
                audioSources[x].Pause();
                audioSources[x] = null;
            }

        }

    }

    // UnPause the sound from the audio sources getting fed to the two mixer's
    public void UnPauseAllSound(){

        if (Paused == false) return;

        // stop new sounds from getting created (eg swap runs out in the background etc)
        Paused = false;

        // Pause all audio sources
        GameObject[] singlegameobjects = {soundtrackEmpty, SFXEmpty, ghostEmpty, monsterEmpty};

        // create new array including all objects that have audiosources
        GameObject[] gameobjects = new GameObject[singlegameobjects.Length + hearts.Length];
        singlegameobjects.CopyTo(gameobjects, 0);
        hearts.CopyTo(gameobjects, singlegameobjects.Length);

        AudioSource[] audioSources;
        int gameObjectCounter = 0;
        for(int i = 0; i < gameobjects.Length; i++)
        {

            // get all audiosources of the object
            audioSources = gameobjects[gameObjectCounter].GetComponents<AudioSource>();
            gameObjectCounter++;
            
            // Pause all audiosources and reset the audioSources Array
            for (int x = 0; x < audioSources.Length; x++){
                audioSources[x].UnPause();
                audioSources[x] = null;
            }

        }

    }


    // Destroy the sound from the audio sources getting fed to the two mixer's
    public void DestroyAllSound(){

        // Pause all audio sources
        GameObject[] singlegameobjects = {soundtrackEmpty, SFXEmpty, ghostEmpty, monsterEmpty};

        // create new array including all objects that have audiosources
        GameObject[] gameobjects = new GameObject[singlegameobjects.Length + hearts.Length];
        singlegameobjects.CopyTo(gameobjects, 0);
        hearts.CopyTo(gameobjects, singlegameobjects.Length);

        AudioSource[] audioSources;
        int gameObjectCounter = 0;
        for(int i = 0; i < gameobjects.Length; i++)
        {

            // get all audiosources of the object
            audioSources = gameobjects[gameObjectCounter].GetComponents<AudioSource>();
            gameObjectCounter++;
            
            // Pause all audiosources and reset the audioSources Array
            for (int x = 0; x < audioSources.Length; x++){
                Destroy(audioSources[x]);
                audioSources[x] = null;
            }

        }

    }

}


/*
    TODO:
    + Let Each Sound Call create an audio source on the specific empty game object
    + Destroy the game object once the sound is done playing
    + leave it active when it is supposed to be looping
    - 
    
*/
