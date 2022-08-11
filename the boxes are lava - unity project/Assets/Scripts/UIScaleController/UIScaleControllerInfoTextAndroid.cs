using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScaleControllerInfoTextAndroid : MonoBehaviour
{
    RectTransform rectTransform;
    TextMeshProUGUI tmp;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        tmp = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            transform.localScale = Vector3.one * 2f;
            rectTransform.anchoredPosition = new Vector2(0f, -1000f);
            tmp.text = "Use arrow touch buttons\nto turn left/right.";
        }
        else 
        {
            transform.localScale = Vector3.one;
            rectTransform.anchoredPosition = new Vector2(0f, 9f);
            tmp.text = "Use arrow touch buttons to turn left/right.";
        }
    }
}
