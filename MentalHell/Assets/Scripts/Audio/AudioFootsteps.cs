using UnityEngine;

public class AudioFootsteps : MonoBehaviour
{

    private SpriteRenderer animationSprites;
    private Sound[] Soundarray;
    private string[] footstepNames = {"mentalhell_running__0000", "mentalhell_running__0008", "mentalhell_walk_0", "mentalhell_walk_4"};
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

                // check if sprite name starts with walk then play walk sound
                if (animationSprites.sprite.ToString().StartsWith("mentalhell_walk")){
                    Soundarray = FindObjectOfType<AudioManager>().sfxStepsWalk;
                    FindObjectOfType<AudioManager>().PlayRandomOnce(Soundarray);
                }

                // check if sprite name starts with run then play run sound
                if (animationSprites.sprite.ToString().StartsWith("mentalhell_run")){
                    Soundarray = FindObjectOfType<AudioManager>().sfxStepsRun;
                    FindObjectOfType<AudioManager>().PlayRandomOnce(Soundarray);
                }

            }
        }
        
    }


}
