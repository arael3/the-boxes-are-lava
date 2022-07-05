using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public bool isButtonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
        Debug.Log("isButtonPressed = " + isButtonPressed);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
        Debug.Log("isButtonPressed = " + isButtonPressed);
    }
}
