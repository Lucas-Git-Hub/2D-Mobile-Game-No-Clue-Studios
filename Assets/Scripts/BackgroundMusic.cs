using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip backgroundMusic;
    private AudioSource currentSoundSource;
    public bool playBackgroundMusic = true;
    public float musicVolume = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        currentSoundSource = GetComponent<AudioSource>();

        if(playBackgroundMusic == true)
        {
            currentSoundSource.clip = backgroundMusic;
            currentSoundSource.volume = musicVolume;
            currentSoundSource.loop = true;
            currentSoundSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
