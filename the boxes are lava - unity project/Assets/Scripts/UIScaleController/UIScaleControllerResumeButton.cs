using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScaleControllerResumeButton : MonoBehaviour
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
			transform.localScale = Vector3.one * 3f;
			rectTransform.anchoredPosition = new Vector2(0f, -300f);
		}
		else 
		{
			transform.localScale = Vector3.one;
			rectTransform.anchoredPosition = new Vector2(0f, 10f);
		}
        
    }
}
