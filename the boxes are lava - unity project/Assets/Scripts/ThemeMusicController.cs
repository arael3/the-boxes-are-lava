using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ThemeMusicController : MonoBehaviour
{
    public float defaultVolume = 0.3f;

    [SerializeField] RawImage iconMusicOn;

    [SerializeField] RawImage iconMusicOff;

    [HideInInspector] public AudioSource audioSource;

    bool isMusicPlay = true;
    

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("ThemeMusicController").Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.GetComponent<AudioSource>();

        audioSource.volume = defaultVolume;
    }

    private void Update()
    {
        if (!isMusicPlay)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            if (iconMusicOn.enabled)
            {
                iconMusicOn.enabled = false;
                iconMusicOff.enabled = true;
            } 
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            if (!iconMusicOn.enabled)
            {
                iconMusicOn.enabled = true;
                iconMusicOff.enabled = false;
            }    
        }
    }

    public void MusicPlayOrStop()
    {
        if (!audioSource.isPlaying)
        {
            isMusicPlay = true;
            audioSource.Play();
            iconMusicOn.enabled = true;
            iconMusicOff.enabled = false;
        }
        else
        {
            isMusicPlay = false;
            audioSource.Stop();
            iconMusicOn.enabled = false;
            iconMusicOff.enabled = true;
        }
    }
}
