using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource worldSound;
    public AudioSource playerSound;

    //Sound Clip
    public AudioClip keyCollectSound;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    public AudioClip manaBallSpawnSound;
    public AudioClip borderSound;
    public AudioClip manaBallCollectSound;
    public AudioClip puzzleActiveSound;

    //Player
    public AudioClip hurt;
    public AudioClip death;
    public AudioClip pant;
    public AudioClip changeSpell;


    //Static bool
    static public bool playKeyCollectSound;
    static public bool playDoorOpenSound;
    static public bool playDoorCloseSound;
    static public bool playBorderSound;
    static public bool playManaBallSpawnSound;
    static public bool playManaBallCollectSound;
    static public bool playPuzzleActiveSound;

    //Player
    static public bool playHurt;
    static public bool playDeath;
    static public bool playPant;
    static public bool playChangeSpell;

    // Start is called before the first frame update
    void Start()
    {
        worldSound = this.GetComponent<AudioSource>();

        playKeyCollectSound = false;
        playDoorOpenSound = false;
        playDoorCloseSound = false;
        playBorderSound = false;
        playManaBallSpawnSound = false;
        playManaBallCollectSound = false;
        playPuzzleActiveSound = false;

        playHurt = false;
        playDeath = false;
        playPant = false;
        playChangeSpell = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playDoorOpenSound)
        {
            worldSound.Stop();
            worldSound.clip = doorOpenSound;
            worldSound.Play();
            playDoorOpenSound = false;
        }

        else if (playDoorCloseSound)
        {
            worldSound.Stop();
            worldSound.clip = doorCloseSound;
            worldSound.Play();
            playDoorCloseSound = false;
        }

        else if (playKeyCollectSound)
        {
            worldSound.Stop();
            worldSound.clip = keyCollectSound;
            worldSound.Play();
            playKeyCollectSound = false;
        }

        else if(playPuzzleActiveSound)
        {
            worldSound.Stop();
            worldSound.clip = puzzleActiveSound;
            worldSound.Play();
            playPuzzleActiveSound = false;
        }

        else if(playBorderSound)
        {
            worldSound.Stop();
            worldSound.clip = borderSound;
            worldSound.Play();
            playBorderSound = false;
        }
        
        else if(playManaBallSpawnSound)
        {
            worldSound.Stop();
            worldSound.clip = manaBallSpawnSound;
            worldSound.Play();
            playManaBallSpawnSound = false;
        }

        else if(playManaBallCollectSound)
        {
            worldSound.Stop();
            worldSound.clip = manaBallCollectSound;
            worldSound.Play();
            playManaBallCollectSound = false;
        }

        //player Sound
        if(playPant)
        {
            playerSound.Stop();
            playerSound.clip = pant;
            playerSound.Play();
            playPant = false;
        }

        else if (playHurt)
        {
            playerSound.Stop();
            playerSound.clip = hurt;
            playerSound.Play();
            playHurt = false;
        }

        else if(playDeath)
        {
            playerSound.Stop();
            playerSound.clip = death;
            playerSound.Play();
            playDeath = false;
        }

        else if(playChangeSpell)
        {
            playerSound.Stop();
            playerSound.clip = changeSpell;
            playerSound.Play();
            playChangeSpell = false;
        }
    }
}
