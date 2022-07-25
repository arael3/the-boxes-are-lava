using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float lerpSize;
    [SerializeField] Vector3 cameraPositionAdjustment;
    [SerializeField] float cameraXAxisAdjustment;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + cameraPositionAdjustment, lerpSize * Time.deltaTime);
        transform.LookAt(player.transform.position);
        transform.Rotate(-cameraXAxisAdjustment, 0f, 0f);
    }
}
