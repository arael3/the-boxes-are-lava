using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBonus : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.transform.localScale.y < 1.6f)
            {
                player.transform.localScale = new Vector3(player.transform.localScale.x + 0.2f, player.transform.localScale.y + 0.2f, player.transform.localScale.z + 0.2f);
                Destroy(gameObject);
            }
        }
    }
}
