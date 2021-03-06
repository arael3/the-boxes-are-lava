using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShieldTimerUI : MonoBehaviour
{
    TextMeshProUGUI tmp;
    PlayerController playerController;
    int time;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tmp.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isShieldActive)
        {
            time = (int)playerController.shieldTimer;
            tmp.text = time.ToString();
        }
        else tmp.text = "";
    }
}
