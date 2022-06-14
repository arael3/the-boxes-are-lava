using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPlatform : MonoBehaviour
{
    [SerializeField] float animationSpeedYAxis = 0.3f;

    [SerializeField] float accelerationTime = 1f;

    [SerializeField] float accelerationAmount = 1f;

    [SerializeField] float accelerationOnStart = 1f;

    float acceleration = 1f;

    float textureOffsetUpdateY;

    GameObject player;
    private bool isAccelerate = false;
    private float accelerationTimeRestart;

    // Start is called before the first frame update
    void Start()
    {
        textureOffsetUpdateY = GetComponent<MeshRenderer>().material.mainTextureOffset.y;
        player = GameObject.FindGameObjectWithTag("Player");
        accelerationTimeRestart = accelerationTime;
        acceleration = accelerationOnStart;
        accelerationAmount /= 10000;
    }

    // Update is called once per frame
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

        //Debug.Log("accelerationTime = " + accelerationTime);
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
            player.GetComponent<Rigidbody>().velocity *= acceleration;
            Debug.Log("acceleration = " + acceleration + "  |  velocity = " + player.GetComponent<Rigidbody>().velocity);
        }
        else
        {
            isAccelerate = false;
            accelerationTime = accelerationTimeRestart;
            acceleration = accelerationOnStart;
        }


    }
}
