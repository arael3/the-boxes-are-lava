using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundController : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("SoundController").Length > 1)
        {
            Destroy(gameObject);
        }

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.spatialBlend = sound.spatialBlend;
            sound.source.loop = sound.loop;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        if (sound == null)
        {
            Debug.LogWarning("Sound: \"" + name + "\" not found!");
            return;
        }

        sound.source.Play();
    }
}
