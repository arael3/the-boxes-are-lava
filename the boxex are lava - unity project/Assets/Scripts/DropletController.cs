using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletController : MonoBehaviour
{
    MeshRenderer mesh;
    float lifeTime = 3f;
    float transparency = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (transparency > 0)
        {
            transparency -= Time.deltaTime / lifeTime;
        }

        if (transparency < 0) transparency = 0;
        
        mesh.materials[0].color = new Color(1f, 1f, 1f, transparency);

        if (lifeTime < 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
