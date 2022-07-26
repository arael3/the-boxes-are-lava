using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool isTrigger = false;
    [SerializeField] Vector3 coinPositionAdjustment;
    [SerializeField] float lerpSize;

    SoundController soundController;
    Points points;

    void Start()
    {
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        points = GameObject.Find("Points Text").GetComponent<Points>(); ;
    }

    void Update()
    {
        if (isTrigger)
        {
            if (transform.localScale.x > 0.01f)
            {
                transform.localScale -= new Vector3(1f, 1f, 1f) * Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position + coinPositionAdjustment, lerpSize * Time.deltaTime);
            }
            else Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
            soundController.PlaySound("Coin");
            points.pointsAmount += 10;
        }
    }
}
