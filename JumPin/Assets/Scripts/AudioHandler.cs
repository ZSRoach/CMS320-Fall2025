using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource musicSource;
    public AudioClip intro;
    [SerializeField]
    public OptionsMenu settings;
    //public AudioClip backgroundMusic, stumbleSound;

    void Start()
    {
        musicSource.PlayOneShot(intro);
        musicSource.PlayScheduled(AudioSettings.dspTime + intro.length);
    }
}
