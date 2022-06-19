using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldBonus : MonoBehaviour
{
    RawImage shieldTimerIcon;

    PlayerController playerController;
    SoundController soundController;
    private bool isTrigger;

    // Start is called before the first frame update
    void Start()
    {
        shieldTimerIcon = GameObject.Find("ShieldTimerIcon").GetComponent<RawImage>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrigger)
        {
            if (transform.parent.localScale.x > 0.01f)
            {
                transform.parent.localScale = new Vector3(transform.parent.localScale.x - Time.deltaTime * 2, transform.parent.localScale.y - Time.deltaTime * 2, transform.parent.localScale.z - Time.deltaTime * 2);
            }
            else Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
            soundController.PlaySound("Shield");
            playerController.shieldTimer = playerController.shieldTimerOnStart;
            playerController.isShieldActive = true;
            shieldTimerIcon.enabled = true;
        }
    }
}
