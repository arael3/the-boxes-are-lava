using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    public enum SoundsList
    {
        LifeBonus
    }

    public AudioClip lifeBonusAudio;

    AudioSource audioSource;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("SoundController").Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(SoundsList soundName)
    {
        switch (soundName)
        {
            case SoundsList.LifeBonus:
                audioSource.PlayOneShot(lifeBonusAudio);
                break;
            default:
                break;
        }
    }
}
