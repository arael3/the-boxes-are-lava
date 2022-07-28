using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScaleControllerMusicOnOffButton : MonoBehaviour
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
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                transform.localScale = Vector3.one * 3f;
                rectTransform.anchoredPosition = new Vector2(168f, 117f);
            }
            else
            {
                transform.localScale = Vector3.one * 3f;
                rectTransform.anchoredPosition = new Vector2(-121f, -5f);
            } 
        }
        else 
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                transform.localScale = Vector3.one;
                rectTransform.anchoredPosition = new Vector2(168f, 117f);
            }
            else
            {
                transform.localScale = Vector3.one;
                rectTransform.anchoredPosition = new Vector2(-121f, -5f);
            }
        }
    }
}
