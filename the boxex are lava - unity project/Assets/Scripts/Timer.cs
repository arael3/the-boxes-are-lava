using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timeText;
    float time;
    int timeInt;
    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        time = 100;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeInt = (int)time;
        timeText.text = timeInt.ToString();
    }
}
