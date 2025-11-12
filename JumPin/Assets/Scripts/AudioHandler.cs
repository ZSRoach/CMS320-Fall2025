using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    AudioSource intro;
    AudioSource loop;
    bool looping;
    void Start()
    {
        AudioSource intro = GetComponents<AudioSource>()[0];
        AudioSource loop = GetComponents<AudioSource>()[1];
        looping = false;
        intro.Play();
        loop.PlayDelayed((float)(34.8));
    }

    // Update is called once per frame
    void Update()
    {
        if (looping==false&&intro.isPlaying == false) {
            loop.Play();
            looping = true;
        }
    }
}
