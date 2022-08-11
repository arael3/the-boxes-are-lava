using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleControllerPauseMenuBackground : MonoBehaviour
{
    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            transform.localScale = new Vector3(1f, 4f, 1f);
        }
        else 
        {
            transform.localScale = Vector3.one;
        }
    }
}
