using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    [SerializeField] float jumpForce = 5f;

    GameObject player;

    public static bool jumpPlatformActivated = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
