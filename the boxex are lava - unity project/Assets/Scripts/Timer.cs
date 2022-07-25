using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timeText;
    [SerializeField] float time = 100f;
    [HideInInspector] public static int timeInt;

    [HideInInspector] public bool isPointsForTimeAdded = false;

    Points points;

    private void Awake()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        timeInt = (int)time;
        timeText.text = timeInt.ToString();
    }

    void Start()
    {
        points = GameObject.Find("Points Text").GetComponent<Points>();
    }

    void Update()
    {
        if (!points.addPointsForTime && !isPointsForTimeAdded && StartCounting.ifGameStarted && time > 0)
        {
            time -= Time.deltaTime;
            timeInt = (int)time;
            timeText.text = timeInt.ToString();

            if (time < 16)
            {
                timeText.color = new Color(1f, 0.26f, 0.26f, 1f);
                GameObject.Find("Time Icon").GetComponent<RawImage>().color = new Color(1f, 0.26f, 0.26f, 1f);
            }
        }
        else timeText.text = timeInt.ToString();
    }
}
