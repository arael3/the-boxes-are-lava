using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBox : MonoBehaviour
{
    [SerializeField] float animationSpeedXAxis = 0.3f;
    [SerializeField] float animationSpeedYAxis = 0.3f;
    float textureOffsetUpdateX;
    float textureOffsetUpdateY;

    // Start is called before the first frame update
    void Start()
    {
        textureOffsetUpdateX = GetComponent<MeshRenderer>().material.mainTextureOffset.x;
        textureOffsetUpdateY = GetComponent<MeshRenderer>().material.mainTextureOffset.y;
    }

    // Update is called once per frame
    void Update()
    {
        textureOffsetUpdateX += Time.deltaTime * animationSpeedXAxis;
        textureOffsetUpdateY += Time.deltaTime * animationSpeedYAxis;
        GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(textureOffsetUpdateX, textureOffsetUpdateY));
    }
}
