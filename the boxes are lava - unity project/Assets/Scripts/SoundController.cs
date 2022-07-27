using UnityEngine;
using System;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public Sound[] sounds;

    [HideInInspector] public static SoundController instance;

    public float defaultVolume = 0.3f;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.spatialBlend = sound.spatialBlend;
            sound.source.loop = sound.loop;
        }

        if (PlayerPrefs.GetInt("IsMusicThemePlay", 2) == 2)
        {
            PlayerPrefs.SetInt("IsMusicThemePlay", 1);
        }
    }

    void Start()
    {
        FindSound("Theme").source.volume = defaultVolume;

        if (PlayerPrefs.GetInt("IsMusicThemePlay", 2) == 0)
        {
            if (FindSound("Theme").source.isPlaying)
            {
                StopSound("Theme");
            }

            //if (GameObject.FindGameObjectWithTag("MusicIconOn").GetComponent<RawImage>().enabled)
            //{
            //    GameObject.FindGameObjectWithTag("MusicIconOn").GetComponent<RawImage>().enabled = false;
            //    GameObject.FindGameObjectWithTag("MusicIconOff").GetComponent<RawImage>().enabled = true;
            //}
        }
        else
        {
            if (!FindSound("Theme").source.isPlaying)
            {
                PlaySound("Theme");
            }

            //if (!GameObject.FindGameObjectWithTag("MusicIconOn").GetComponent<RawImage>().enabled)
            //{
            //    GameObject.FindGameObjectWithTag("MusicIconOn").GetComponent<RawImage>().enabled = true;
            //    GameObject.FindGameObjectWithTag("MusicIconOff").GetComponent<RawImage>().enabled = false;
            //}
        }
    }

    public Sound FindSound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        return sound;
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

    public void StopSound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        if (sound == null)
        {
            Debug.LogWarning("Sound: \"" + name + "\" not found!");
            return;
        }

        sound.source.Stop();
    }

    public void MusicThemePlayOrStop()
    {
        if (PlayerPrefs.GetInt("IsMusicThemePlay", 2) == 0)
        {
            PlayerPrefs.SetInt("IsMusicThemePlay", 1);
            PlaySound("Theme");
            GameObject.FindGameObjectWithTag("MusicIconOn").GetComponent<RawImage>().enabled = true;
            GameObject.FindGameObjectWithTag("MusicIconOff").GetComponent<RawImage>().enabled = false;
        }
        else
        {
            PlayerPrefs.SetInt("IsMusicThemePlay", 0);
            StopSound("Theme");
            GameObject.FindGameObjectWithTag("MusicIconOn").GetComponent<RawImage>().enabled = false;
            GameObject.FindGameObjectWithTag("MusicIconOff").GetComponent<RawImage>().enabled = true;
        }
    }
}
