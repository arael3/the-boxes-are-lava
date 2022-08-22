using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleControllerResetHighScoreButton : MonoBehaviour
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
                rectTransform.anchoredPosition = new Vector2(-150f, -200f);
            }
            else
            {
                transform.localScale = Vector3.one;
                rectTransform.anchoredPosition = new Vector2(281f, 0f);
            }

    }
}
