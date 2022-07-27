using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicIconOn : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("IsMusicThemePlay", 2) == 0)
        {
            if (gameObject.GetComponent<RawImage>().enabled)
            {
                gameObject.GetComponent<RawImage>().enabled = false;
            }
        }
        else
        {
            if (!gameObject.GetComponent<RawImage>().enabled)
            {
                gameObject.GetComponent<RawImage>().enabled = true;
            }
        }
    }
}
