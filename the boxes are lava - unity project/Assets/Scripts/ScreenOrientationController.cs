using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOrientationController : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(Screen.orientation);
    }

    public void ChangeScreenOrientation()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
}
