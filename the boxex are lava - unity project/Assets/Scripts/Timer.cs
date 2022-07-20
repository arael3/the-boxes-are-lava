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

    private void Awake()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        time = 100f;
        timeInt = (int)time;
        timeText.text = timeInt.ToString();
    }

    void Start()
    {
        points = GameObject.Find("Points Text").GetComponent<Points>();
    }

    void Update()
    {
        if (!points.addPointsForTime && !isPointsForTimeAdded && StartCounting.ifGameStarted)
        {
            time -= Time.deltaTime;
            timeInt = (int)time;
            timeText.text = timeInt.ToString();
        }
        else timeText.text = timeInt.ToString();
    }
}
