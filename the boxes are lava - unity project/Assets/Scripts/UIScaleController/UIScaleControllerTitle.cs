using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScaleControllerTitle : MonoBehaviour
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
                transform.localScale = Vector3.one * 1.7f;
                rectTransform.anchoredPosition = new Vector2(0f, -450f);
                tmp.text = "the boxes\nare lava";
            }
            else 
            {
                transform.localScale = Vector3.one * 1.8f;
                rectTransform.anchoredPosition = new Vector2(0f, -12f);
                tmp.text = "the boxes are lava";
            }

    }
}
