using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleController : MonoBehaviour
{
    void Update()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            transform.localScale = Vector3.one * 2f;
        }
        else 
        {
            transform.localScale = Vector3.one;
        }
    }
}
