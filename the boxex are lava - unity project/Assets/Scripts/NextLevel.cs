using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    Points points;

    void Start()
    {
        points = GameObject.Find("Points Text").GetComponent<Points>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            points.addPointsForTime = true;

            //GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().StartCoroutine("NextLevel");
        }
    }
}
