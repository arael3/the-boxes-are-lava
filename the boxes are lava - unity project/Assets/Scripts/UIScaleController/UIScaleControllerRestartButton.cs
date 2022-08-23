using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleControllerRestartButton: MonoBehaviour
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
                transform.localScale = Vector3.one * 2f;
                rectTransform.anchoredPosition = new Vector2(0f, 0f);
            }
            else 
            {
                transform.localScale = Vector3.one;
                rectTransform.anchoredPosition = new Vector2(0f, 8f);
            }

    }
}
