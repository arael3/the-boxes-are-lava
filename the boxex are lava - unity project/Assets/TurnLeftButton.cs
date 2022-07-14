using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnLeftButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public bool isButtonPressed;
    bool isButtonRelease = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isButtonRelease)
        {
            isButtonRelease = false;
            isButtonPressed = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonRelease = true;
    }
}
