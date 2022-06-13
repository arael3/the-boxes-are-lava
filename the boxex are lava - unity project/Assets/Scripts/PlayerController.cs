using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce = 100;
    [SerializeField] float fallingVelocity;
    [SerializeField] float maxfallingVelocity;
    [SerializeField] float speed = 5;
    [SerializeField] float maxAngularVelocity;
    [SerializeField] float meltingSpeed = 5;
    [SerializeField] float plashSizeSpeed = 5;
    [SerializeField] float damageFromLavaBox = 0.1f;
    [SerializeField] GameObject steamAfterDamageParticleSystem;
    [SerializeField] AudioClip[] steamSoundsAfterDamage;
    [SerializeField] GameObject droplet;

    [HideInInspector]
    public bool isLevelEnd = false;

    Rigidbody rb;
    SphereCollider sphereCollider;

    SoundController soundController;

    float playerSize;
    float plashSize;
    bool isPlashed = false;

    float XAxis;
    float ZAxis;

    bool jump = false;
    bool afterJump = false;
    float timerAfterJump = 0.2f;
    float timerAfterJumpReset = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        rb.maxAngularVelocity = maxAngularVelocity;
        transform.position = GameObject.Find("StartPoint").transform.position;
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, Vector3.down, Color.red);
        if (transform.localScale.y > 0.1f)
        {
            playerSize = transform.localScale.y - Time.deltaTime / 100 * meltingSpeed;
            transform.localScale = new Vector3(playerSize, playerSize, playerSize);
        } 
        else
            PlayerPlashed();

        if (Input.GetAxisRaw("Horizontal") != 0 && transform.localScale.y > 0.1f && !isLevelEnd)
            ZAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetAxisRaw("Vertical") != 0 && transform.localScale.y > 0.1f && !isLevelEnd)
            XAxis = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (transform.position.y < -10f && !isPlashed) 
        {
            IsPlashed();
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().StartCoroutine("RestartLevel");
        }

        DropADroplet();
    }

    private void FixedUpdate()
    {
        if (XAxis !=0 || ZAxis != 0)
        {
            rb.AddTorque(XAxis * speed, 0f, -ZAxis * speed);
            XAxis = 0;
            ZAxis = 0;
        }

        if (jump)
        {
            //Debug.Log("IsGrounded() = " + IsGrounded());

            if (IsGrounded())
            {
                rb.AddForce(new Vector3(0f, jumpForce, 0f));
                afterJump = true;
            }
            jump = false;
        }

        if (afterJump)
        {
            timerAfterJump -= Time.deltaTime;

            if (timerAfterJump < 0)
            {
                if (rb.velocity.y < 0)
                {
                    if (!IsGrounded())
                    {
                        if (rb.velocity.y > -maxfallingVelocity)
                        {
                            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - fallingVelocity, rb.velocity.z);
                        }
                    }
                    else 
                    {
                        timerAfterJump = timerAfterJumpReset;
                        afterJump = false;
                    }
                }
            }
        }
        
    }

    private void DropADroplet()
    {
        int probabilityOfInstantiate = Random.Range(1, 10);
        if (probabilityOfInstantiate == 1)
        {
            float randomX = Random.Range(-0.1f, 0.1f);
            float randomZ = Random.Range(-0.1f, 0.1f);
            GameObject dropletInstance = Instantiate(droplet, new Vector3(transform.position.x + randomX, transform.position.y - sphereCollider.radius * transform.localScale.y, transform.position.z + randomZ), Quaternion.Euler(Vector3.zero));
            float randomScale = Random.Range(0.1f, 0.5f);
            dropletInstance.transform.localScale = new Vector3(randomScale * transform.localScale.x, 0.01f, randomScale * transform.localScale.y);
        }
        
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //if (hit.transform.gameObject.name == "Platform" && hit.distance <= GetComponent<SphereCollider>().radius)
            Debug.Log($"hit.distance = {hit.distance}  |  transform.localScale.x = {transform.localScale.x / 1.9f}");
            if (hit.transform.gameObject.name == "Platform" && hit.distance <= transform.localScale.x / 1.9f)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("LavaBox"))
        {
            if (transform.localScale.y > 0.2f)
            {
                int random = Random.Range(0, steamSoundsAfterDamage.Length);
                AudioSource.PlayClipAtPoint(steamSoundsAfterDamage[random], collision.contacts[0].point);
                Instantiate(steamAfterDamageParticleSystem, transform.position, Quaternion.Euler(-90f, 0f, 0f));
                playerSize = transform.localScale.y - damageFromLavaBox;
                transform.localScale = new Vector3(playerSize, playerSize, playerSize);
            }
            else
            {
                PlayerPlashed();
            }
        }
    }

    void IsPlashed()
    {
        if (!isPlashed) soundController.PlaySound(SoundController.SoundsList.Lose);

        isPlashed = true;
    }

    void PlayerPlashed()
    {
        IsPlashed();

        rb.velocity = Vector3.zero;
        sphereCollider.radius = 0.01f;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        if (transform.localScale.x < 0.7f)
        {
            plashSize = transform.localScale.x + Time.deltaTime / 10 * plashSizeSpeed;
            transform.localScale = new Vector3(plashSize, 0.01f, plashSize);
        }
        else
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().StartCoroutine("RestartLevel");
        }
    }
}
