using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    [SerializeField] float jumpForce = 5f;

    GameObject player;

    bool jumpPlatformActivated = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //Debug.Log("IsGrounded() = " + player.GetComponent<PlayerController>().IsGrounded());
        
    }

    void FixedUpdate()
    {
        if (jumpPlatformActivated)
        {
            if (player.GetComponent<PlayerController>().IsGrounded())
            {
                jumpPlatformActivated = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("other.gameObject.tag = " + other.gameObject.tag + "  |  IsGrounded() = " + player.GetComponent<PlayerController>().IsGrounded());

        if (other.gameObject.CompareTag("Player") && player.GetComponent<PlayerController>().IsGrounded())
        {
            jumpPlatformActivated = true;
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);

            gameObject.GetComponent<Animator>().SetBool("playAnimation", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Animator>().SetBool("playAnimation", false);
        }
    }
}
