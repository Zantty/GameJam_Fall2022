using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private DragonHealth dragonHealth;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioClip[] music;

    public bool dead;
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

        dragonHealth = GetComponent<DragonHealth>();
    }

    private void OnTriggerStay2D(Collider2D collision)
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale <= 0.1f)
            {
                currentMusic = music[2];
                musicAudioSource.clip = currentMusic;
                musicAudioSource.Play();
            }
            else
            {
                currentMusic = music[0];
                musicAudioSource.clip = currentMusic;
                musicAudioSource.Play();
            }
        }

        if (dragonHealth.dead && currentMusic != music[3])
        {
            musicAudioSource.Stop();
            if (!musicAudioSource.isPlaying)
            {
                currentMusic = music[3];
                musicAudioSource.clip = currentMusic;
                musicAudioSource.Play();
            }
        }
    }
}
