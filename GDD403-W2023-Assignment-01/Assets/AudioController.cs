using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    //Singleton instance of this controller. Multiples will glitch the system.
    public static AudioController Instance;

    public AudioClip win;
    public AudioClip lose;
    public AudioClip shuffle;
    public AudioClip match;
    public AudioClip matchfail;

    private AudioSource source;

    public void Start()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(CLIPS c)
    {
        switch(c)
        {
            case CLIPS.WIN:
                source.clip = win;
                break;
            case CLIPS.LOSE:
                source.clip = lose;
                break;
            case CLIPS.SHUFFLE:
                source.clip = shuffle;
                break;
            case CLIPS.MATCH:
                source.clip = match;
                break;
            case CLIPS.MFAIL:
                source.clip = matchfail;
                break;

        }
        source.Play();
    }
}

public enum CLIPS
{
    WIN = 0,
    LOSE = 1,
    SHUFFLE = 2,
    MATCH = 3,
    MFAIL = 4,
}
