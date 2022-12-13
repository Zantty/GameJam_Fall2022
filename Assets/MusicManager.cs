using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioClip[] music;
    private AudioClip currentMusic;
    
    // Start is called before the first frame update
    void Start()
    {
        //musicAudioSource = GetComponent<AudioSource>();
        currentMusic = music[0];
        musicAudioSource.clip = currentMusic;
        if (!musicAudioSource.isPlaying)
        {
            musicAudioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "SafeZone")
        {
            if(musicAudioSource.isPlaying && currentMusic == music[0])
            {
                musicAudioSource.Stop();
            }
            if (!musicAudioSource.isPlaying)
            {
                currentMusic = music[1];
                musicAudioSource.clip = currentMusic;
                musicAudioSource.Play();
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "SafeZone")
        {
            if (musicAudioSource.isPlaying && currentMusic == music[1])
            {
                musicAudioSource.Stop();
            }
            if (!musicAudioSource.isPlaying)
            {
                currentMusic = music[0];
                musicAudioSource.clip = currentMusic;
                musicAudioSource.Play();
            }
        }
    }
}
