using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource worldSound;

    //Sound Clip
    public AudioClip keyCollectSound;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    public AudioClip manaBallSpawnSound;
    public AudioClip borderSound;
    public AudioClip manaBallCollectSound;
    public AudioClip puzzleActiveSound;

    //Static bool
    static public bool playKeyCollectSound;
    static public bool playDoorOpenSound;
    static public bool playDoorCloseSound;
    static public bool playBorderSound;
    static public bool playManaBallSpawnSound;
    static public bool playManaBallCollectSound;
    static public bool playPuzzleActiveSound;

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
    }
}
