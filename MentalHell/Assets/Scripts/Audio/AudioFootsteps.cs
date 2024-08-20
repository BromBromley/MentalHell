using UnityEngine;

public class AudioFootsteps : MonoBehaviour
{

    private SpriteRenderer animationSprites;
    private Sound[] Soundarray;
    private string[] footstepNames = {"run_0009", "run_0001", "walk_0009", "walk_0001"};
    private Sprite spriteCheck;

    void Start()
    {
        // get the sprites component
        animationSprites = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // return if sprite sound has been played already
        if (animationSprites.sprite == spriteCheck) return;

        for (int i = 0; i < footstepNames.Length; i++)
        {

            // check if sprite names are same as ones from array (sprites when turned into string have a Unit.Object addon thats why StartsWith)
            if (animationSprites.sprite.ToString().StartsWith(footstepNames[i])){

                
                
                // set sprite equal to check to check that the sprite sound wont get played twice
                // update is faster than the animation
                spriteCheck = animationSprites.sprite;

                Debug.Log("Walk.");

                // check if sprite name starts with walk then play walk sound
                if (animationSprites.sprite.ToString().StartsWith("walk_")){
                    Soundarray = FindObjectOfType<AudioManager>().sfxStepsWalk;
                    FindObjectOfType<AudioManager>().PlayRandomOnce(Soundarray);
                }

                // check if sprite name starts with run then play run sound
                if (animationSprites.sprite.ToString().StartsWith("run_")){
                    Soundarray = FindObjectOfType<AudioManager>().sfxStepsRun;
                    FindObjectOfType<AudioManager>().PlayRandomOnce(Soundarray);
                }

            }
        }
        
    }


}
