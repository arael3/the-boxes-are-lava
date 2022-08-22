using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScaleControllerHighScoreText : MonoBehaviour
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
                transform.localScale = Vector3.one * 1.7f;
                rectTransform.anchoredPosition = new Vector2(0f, 600f);
            }
            else 
            {
                transform.localScale = Vector3.one;
                rectTransform.anchoredPosition = new Vector2(0f, 190f);
            }

    }
}
