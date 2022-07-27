using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    Points points;

    SoundController soundController;

    GameObject player;

    void Start()
    {
        points = GameObject.Find("Points Text").GetComponent<Points>();
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            soundController.FindSound("Theme").source.volume *= 0.3f;
            soundController.PlaySound("CoinsForTime");
            points.addPointsForTime = true;
            player.GetComponent<PlayerController>().isLevelEnd = true;
            //GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().StartCoroutine("NextLevel");
        }
    }
}
