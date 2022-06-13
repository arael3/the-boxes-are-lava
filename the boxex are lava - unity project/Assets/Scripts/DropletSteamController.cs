using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletSteamController : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector3(transform.parent.localScale.x, transform.localScale.y, transform.parent.localScale.z);
    }
}
