using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] public float shieldTimerOnStart = 5.99f;

    [HideInInspector]
    public bool isLevelEnd = false;

    Rigidbody rb;
    SphereCollider sphereCollider;

    SoundController soundController;

    float playerSize;
    float plashSize;
    bool isPlashed = false;

    float vertical;
    float horizontal;

    bool jump = false;
    bool afterJump = false;
    float timerAfterJump = 0.2f;
    float timerAfterJumpReset = 0.2f;
    float safeTimeAfterDamage = 0f;
    float safeTimeAfterDamageRestart = 1f;
    private bool damageTrigger = false;

    [HideInInspector] public bool isShieldActive = false;
    [HideInInspector] public float shieldTimer;

    RawImage shieldTimerIcon;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        rb.maxAngularVelocity = maxAngularVelocity;
        transform.position = GameObject.Find("StartPoint").transform.position;
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        shieldTimer = shieldTimerOnStart;

        shieldTimerIcon = GameObject.Find("ShieldTimerIcon").GetComponent<RawImage>();
    }

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
            horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetAxisRaw("Vertical") != 0 && transform.localScale.y > 0.1f && !isLevelEnd)
            vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (transform.position.y < -10f && !isPlashed) 
        {
            IsPlashed();
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().StartCoroutine("RestartLevel");
        }

        if (IsGrounded()) DropADroplet();

        if (damageTrigger && safeTimeAfterDamage > 0f)
        {
            safeTimeAfterDamage -= Time.deltaTime;
        }

        if (isShieldActive)
        {
            shieldTimer -= Time.deltaTime;

            if (shieldTimer < 0)
            {
                isShieldActive = false;
                shieldTimer = shieldTimerOnStart;
                shieldTimerIcon.GetComponent<RawImage>().enabled = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (vertical !=0 || horizontal != 0)
        {
            // Movement control by using rotation of player (sphere)
            rb.AddTorque(vertical * speed, 0f, -horizontal * speed);

            // Movement control when player don't touch ground
            if (!IsGrounded())
            {
                rb.AddForce(horizontal * speed / 100, 0f, vertical * speed / 500, ForceMode.Impulse);
            }

            vertical = 0;
            horizontal = 0;
        }

        if (jump)
        {
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
            GameObject dropletInstance = Instantiate(droplet, new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f), transform.position.y - sphereCollider.radius * transform.localScale.y, transform.position.z + Random.Range(-0.1f, 0.1f)), Quaternion.Euler(0f, Random.Range(0, 180), 0f));
            float randomScale = Random.Range(0.1f, 0.5f);
            dropletInstance.transform.localScale = new Vector3(randomScale * transform.localScale.x, 0.01f, randomScale * transform.localScale.y);
        }
        
    }

    public bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // hit.distance <= transform.localScale.x / 1.9f means that hit.distance should be <= sphere radius
            if (hit.distance <= transform.localScale.x / 1.9f)
                return true;
            else
            {
                Debug.Log("hit.transform.gameObject.name = " + hit.transform.gameObject.name);

                return false;
            }
                
        }
        else
            return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("LavaBox") && !isShieldActive)
        {
            if (transform.localScale.y > 0.2f)
            {
                // After receiving damage from lava box Player is protected from further damages over time = safeTimeAfterDamage
                if (safeTimeAfterDamage <= 0)
                {
                    safeTimeAfterDamage = safeTimeAfterDamageRestart;
                    damageTrigger = true;
                    int random = Random.Range(0, steamSoundsAfterDamage.Length);
                    AudioSource.PlayClipAtPoint(steamSoundsAfterDamage[random], collision.contacts[0].point);
                    Instantiate(steamAfterDamageParticleSystem, transform.position, Quaternion.Euler(-90f, 0f, 0f));
                    playerSize = transform.localScale.y - damageFromLavaBox;
                    transform.localScale = new Vector3(playerSize, playerSize, playerSize);
                }
            }
            else
            {
                PlayerPlashed();
            }
        }
    }

    void IsPlashed()
    {
        if (!isPlashed) soundController.PlaySound("Lose");

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
