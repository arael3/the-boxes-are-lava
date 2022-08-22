using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScaleControllerExitGameButton : MonoBehaviour
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
                    transform.localScale = Vector3.one;
                    rectTransform.anchoredPosition = new Vector2(0f, -400f);
                }
                else
                {
                    transform.localScale = Vector3.one * 3f;
                    rectTransform.anchoredPosition = new Vector2(0f, -800f);
                } 
            }
            else 
            {
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    transform.localScale = Vector3.one;
                    rectTransform.anchoredPosition = new Vector2(0f, -400f);
                }
                else
                {
                    transform.localScale = Vector3.one;
                    rectTransform.anchoredPosition = new Vector2(0f, -189f);
                }
            }

    }
}
