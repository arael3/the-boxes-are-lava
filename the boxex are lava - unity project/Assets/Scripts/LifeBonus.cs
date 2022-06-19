using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBonus : MonoBehaviour
{
    [SerializeField] float lifeBonusAmount = 0.8f;

    GameObject player;

    SoundController soundController;

    bool isTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrigger)
        {
            if (transform.localScale.x > 0.01f)
            {
                player.transform.localScale = new Vector3(player.transform.localScale.x + Time.deltaTime * lifeBonusAmount, player.transform.localScale.y + Time.deltaTime * lifeBonusAmount, player.transform.localScale.z + Time.deltaTime * lifeBonusAmount);

                transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * 2, transform.localScale.y - Time.deltaTime * 2, transform.localScale.z - Time.deltaTime * 2);
            }
            else Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.transform.localScale.y < 1.5f)
            {
                soundController.PlaySound("LifeBonus");
                isTrigger = true;
            }
        }
    }
}
