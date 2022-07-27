using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MusicButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>().MusicThemePlayOrStop();
    }
}
