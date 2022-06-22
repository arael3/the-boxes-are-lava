using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    public int pointsAmount;
    TextMeshProUGUI pointsUI;

    // Start is called before the first frame update
    void Start()
    {
        pointsAmount = 0;
        pointsUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        pointsUI.text = pointsAmount.ToString();
    }


}
