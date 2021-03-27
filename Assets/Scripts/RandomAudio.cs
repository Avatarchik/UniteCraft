using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip otherClip1;
    public AudioClip otherClip2;
    public AudioClip otherClip3;
    private float musicVolume;
    private float randomNum;
    private int state;

    // Use this for initialization
    private void Start()
    {
        musicVolume = 0.6f;
        RandomPlay();
    }

    // Update is called once per frame
    private void Update()
    {
        audioSource.volume = musicVolume;
        if (state == 1 && !audioSource.isPlaying || state == 2 && !audioSource.isPlaying ||
            state == 3 && !audioSource.isPlaying)
        {
            RandomPlay();
        }
    }

    private void RandomPlay()
    {
        randomNum = Random.Range(1.0f, 4.0f);
        if (randomNum >= 1.0f && randomNum < 2.0f)
        {
            state = 1;
            audioSource.clip = otherClip1;
            audioSource.Play();
        }
        else if (randomNum >= 2.0f && randomNum < 3.0f)
        {
            state = 2;
            audioSource.clip = otherClip2;
            audioSource.Play();
        }
        else if (randomNum >= 3.0f && randomNum <= 4.0f)
        {
            state = 3;
            audioSource.clip = otherClip3;
            audioSource.Play();
        }
    }
}