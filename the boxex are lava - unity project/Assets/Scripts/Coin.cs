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

    // Start is called before the first frame update
    void Start()
    {
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        points = GameObject.Find("Points Text").GetComponent<Points>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrigger)
        {
            if (transform.localScale.x > 0.01f)
            {
                transform.localScale -= new Vector3(1f, 1f, 1f) * Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position + coinPositionAdjustment, lerpSize * Time.deltaTime);
                //transform.position += new Vector3(0f, 10f, 0f) * Time.deltaTime;
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
