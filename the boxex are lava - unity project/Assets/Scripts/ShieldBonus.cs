using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBonus : MonoBehaviour
{
    [SerializeField] GameObject shieldTimer;
    GameObject player;
    private bool isTrigger;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrigger)
        {
            if (transform.localScale.x > 0.01f)
            {
                transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * 2, player.transform.localScale.y - Time.deltaTime * 2, player.transform.localScale.z - Time.deltaTime * 2);
            }
            else Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
            player.GetComponent<PlayerController>().isShieldActive = true;
            shieldTimer.SetActive(true);
        }
    }
}
