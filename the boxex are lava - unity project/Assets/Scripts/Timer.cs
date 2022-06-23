using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timeText;
    float time;
    [HideInInspector] public int timeInt;

    [HideInInspector] public bool isPointsForTimeAdded = false;

    Points points;

    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        time = 100;
        points = GameObject.Find("Points Text").GetComponent<Points>();
    }

    void Update()
    {
        if (!points.addPointsForTime && !isPointsForTimeAdded)
        {
            time -= Time.deltaTime;
            timeInt = (int)time;
            timeText.text = timeInt.ToString();
        }
        else timeText.text = timeInt.ToString();
    }
}
