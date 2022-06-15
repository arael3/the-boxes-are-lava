using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPlatform : MonoBehaviour
{
    [SerializeField] float animationSpeedYAxis = 0.3f;

    [SerializeField] float accelerationTime = 1f;

    [SerializeField] float accelerationAmount = 1f;

    [SerializeField] float accelerationOnStart = 1f;

    GameObject player;
    SphereCollider playerCollider;

    float acceleration = 1f;
    
    float textureOffsetUpdateY;

    
    private bool isAccelerate = false;
    private float accelerationTimeRestart;

    private bool isDeaccelerate = false;

    void Start()
    {
        textureOffsetUpdateY = GetComponent<MeshRenderer>().material.mainTextureOffset.y;
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<SphereCollider>();
        accelerationTimeRestart = accelerationTime;
        acceleration = accelerationOnStart;
        accelerationAmount /= 10000;
    }

    void Update()
    {
        textureOffsetUpdateY += Time.deltaTime * animationSpeedYAxis;
        GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(0f, textureOffsetUpdateY));
    }

    private void FixedUpdate()
    {
        if (isAccelerate)
        {
            Accelerate();
        }

        if (isDeaccelerate)
        {
            Deaccelerate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAccelerate = true;
        }
    }

    void Accelerate()
    {
        accelerationTime -= Time.deltaTime;

        acceleration += accelerationAmount;

        if (accelerationTime > 0)
        {
            if (player.GetComponent<PlayerController>().IsGrounded())
            {
                player.GetComponent<Rigidbody>().velocity *= acceleration;
                playerCollider.sharedMaterial.staticFriction = 4f;
                playerCollider.sharedMaterial.dynamicFriction = 4f;
            }
            else
            {
                player.GetComponent<Rigidbody>().velocity *= 0.98f;
            }
        }
        else
        {
            isAccelerate = false;
            accelerationTime = accelerationTimeRestart;
            acceleration = accelerationOnStart;

            playerCollider.sharedMaterial.staticFriction = 0.1f;
            playerCollider.sharedMaterial.dynamicFriction = 0.1f;

            isDeaccelerate = true;
        }
    }

    void Deaccelerate()
    {
        if (playerCollider.sharedMaterial.staticFriction < 5f)
        {
            if (!player.GetComponent<PlayerController>().IsGrounded())
            {
                player.GetComponent<Rigidbody>().velocity *= 0.98f;
            }

            playerCollider.sharedMaterial.staticFriction += Time.deltaTime * 5;
            playerCollider.sharedMaterial.dynamicFriction += Time.deltaTime * 5;
        }
        else isDeaccelerate = false;
    }
}
