using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCounting : MonoBehaviour
{
    float startCountingValue;

    [SerializeField] float startCountingValueRestart = 3.99f;

    int startCountingValueInt;

    float afterStartCountingValue;

    float afterStartCountingValueRestart = 1f;

    [HideInInspector] public static bool ifGameStarted;

    TextMeshProUGUI tmp;

    void Start()
    {
        startCountingValue = startCountingValueRestart;
        afterStartCountingValue = afterStartCountingValueRestart;
        ifGameStarted = false;
        tmp = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!ifGameStarted) 
        {
            startCountingValue -= Time.deltaTime;

            startCountingValueInt = (int)startCountingValue;

            tmp.text = startCountingValueInt.ToString();

            if (startCountingValue < 1)
            {
                ifGameStarted = true;
                startCountingValue = startCountingValueRestart;
                tmp.text = "START";
            }
        }

        if (tmp.enabled && tmp.text == "START")
        {
            afterStartCountingValue -= Time.deltaTime;

            if (afterStartCountingValue < 0)
            {
                tmp.enabled = false;
            }
        }
    }
}
