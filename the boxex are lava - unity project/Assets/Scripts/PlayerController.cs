using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float maxAngularVelocity;
    [SerializeField] float meltingSpeed = 5;
    [SerializeField] float plashSizeSpeed = 5;
    [SerializeField] float damageFromLavaBox = 0.1f;
    [SerializeField] GameObject steamAfterDamageParticleSystem;

    Rigidbody rb;
    SphereCollider sphereCollider;

    float playerSize;
    float plashSize;

    float XAxis;
    float ZAxis;
    



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        rb.maxAngularVelocity = maxAngularVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.y > 0.1f)
        {
            playerSize = transform.localScale.y - Time.deltaTime / 100 * meltingSpeed;
            transform.localScale = new Vector3(playerSize, playerSize, playerSize);
        } 
        else
        {
            GameOver();
        }

        if (Input.GetAxisRaw("Horizontal") != 0 && transform.localScale.y > 0.1f)
            ZAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetAxisRaw("Vertical") != 0 && transform.localScale.y > 0.1f)
            XAxis = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (XAxis !=0 || ZAxis != 0)
        {
            rb.AddTorque(XAxis * speed, 0f, -ZAxis * speed);
            XAxis = 0;
            ZAxis = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava Box"))
        {
            if (transform.localScale.y > 0.2f)
            {
                Instantiate(steamAfterDamageParticleSystem, transform.position, Quaternion.Euler(-90f, 0f, 0f));
                playerSize = transform.localScale.y - damageFromLavaBox;
                transform.localScale = new Vector3(playerSize, playerSize, playerSize);
            }
            else
            {
                GameOver();
            }
                
        }
    }

    void GameOver()
    {
        rb.velocity = Vector3.zero;
        sphereCollider.radius = 0.01f;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        if (transform.localScale.x < 0.7f)
        {
            plashSize = transform.localScale.x + Time.deltaTime / 10 * plashSizeSpeed;
            transform.localScale = new Vector3(plashSize, 0.09f, plashSize);
        }

        
    }
}
