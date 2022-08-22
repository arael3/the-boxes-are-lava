using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleControllerPoints : MonoBehaviour
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
                rectTransform.anchoredPosition = new Vector2(404f, -264f);
            }
            else 
            {
                transform.localScale = Vector3.one;
                rectTransform.anchoredPosition = new Vector2(206f, -54f);
            }

    }
}
