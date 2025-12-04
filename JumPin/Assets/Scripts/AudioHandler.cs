using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource musicSource;
    public AudioClip intro;
    //public AudioClip backgroundMusic, stumbleSound;

    void Start()
    {
        if (OptionsMenu.musicOn) {
            musicSource.PlayOneShot(intro);
            musicSource.PlayScheduled(AudioSettings.dspTime + intro.length);
        }
    }
}
