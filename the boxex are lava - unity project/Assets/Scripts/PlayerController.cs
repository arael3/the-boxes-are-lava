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
    [SerializeField] public float maxVelocity = 10f;
    [SerializeField] float maxAngularVelocity;
    [SerializeField] float meltingSpeed = 5;
    [SerializeField] float plashSizeSpeed = 5;
    [SerializeField] float damageFromLavaBox = 0.1f;
    [SerializeField] GameObject steamAfterDamageParticleSystem;
    [SerializeField] AudioClip[] steamSoundsAfterDamage;
    [SerializeField] GameObject droplet;
    [SerializeField] public float shieldTimerOnStart = 5.99f;
    [SerializeField] Joystick joystick;

    [SerializeField] [Range(0f, 1f)] float moveProportionSet = 0.5f;

    [SerializeField] float horizontalSpeed = 1f;
    [SerializeField] float staticHorizontalSpeed = 1f;

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

    float horizontalMovementInput;
    float verticalMovementInput;


    //float jump;
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

    TurnLeftButton turnLeftButton;
    TurnRightButton turnRightButton;

    private bool isMoveHorizontal = false;
    private float actualAxisX;

    [SerializeField] float portionToMoveX = 1.5f;
    float portionToMoveXRestart = 1.5f;
    private float positionXToMove;
    private float multiplierX;
    private bool turnRight;
    private bool turnLeft;

    [SerializeField] float decreasePortionBy = 0.05f;

    // Movement control with using New Input System module
    //PlayerActionControls playerActionControls;

    int licznik = 0;
    private float initialPortionToMove = 0;
    private int toMoveCounter = 0;

    JumpingPlatform jumpingPlatform;

    float moduloPositionX;
    [SerializeField] float maxHorizontalVelocity = 1f;
    private int portionsOfAddForce;
    [SerializeField] int maxPortionsOfAddForce = 30;
    private bool ifDontReachedPositionX;

    int decreasePortionByMultiplier;

    [HideInInspector] public bool afterAccelerationPlatformUsed = false;
    private int numberOfMoves;

    private void Awake()
    {
        //playerActionControls = new PlayerActionControls();
    }

    private void OnEnable()
    {
        //playerActionControls.Enable();
    }

    private void OnDisable()
    {
        //playerActionControls.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        rb.maxAngularVelocity = maxAngularVelocity;
        //transform.position = GameObject.Find("StartPoint").transform.position;
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        shieldTimer = shieldTimerOnStart;

        shieldTimerIcon = GameObject.Find("ShieldTimerIcon").GetComponent<RawImage>();

        turnLeftButton = GameObject.Find("Turn Left Button").GetComponent<TurnLeftButton>();
        turnRightButton = GameObject.Find("Turn Right Button").GetComponent<TurnRightButton>();
    }

    void Update()
    {
        if (StartCounting.ifGameStarted)
        {
            //Debug.Log("jumpPlatformActivated = " + JumpingPlatform.jumpPlatformActivated + "  IsGrounded() = " + IsGrounded());

            // Movement control with using New Input System module
            // Read the movement value
            //horizontalMovementInput = playerActionControls.Land.Horizontal.ReadValue<float>();
            //verticalMovementInput = playerActionControls.Land.Vertical.ReadValue<float>();

            // Read the jump value
            //jump = playerActionControls.Land.Jump.ReadValue<float>();

            // Move the player
            //transform.position += new Vector3(horizontalMovementInput * speed * Time.deltaTime, 0f, verticalMovementInput * speed * Time.deltaTime);

            //Debug.DrawRay(transform.position, Vector3.down, Color.red);
            if (transform.localScale.y > 0.1f)
            {
                playerSize = transform.localScale.y - Time.deltaTime / 100 * meltingSpeed;
                transform.localScale = new Vector3(playerSize, playerSize, playerSize);
            }
            else
                PlayerPlashed();

            // Keyboard control
            //if (Input.GetAxisRaw("Horizontal") != 0 && transform.localScale.y > 0.1f && !isLevelEnd)
            if (Input.GetButtonDown("Horizontal") && transform.localScale.y > 0.1f && !isLevelEnd)
            {
                horizontal = Input.GetAxisRaw("Horizontal");

                CountNumberOfMoves();
            }

            if (turnLeftButton.isButtonPressed && transform.localScale.y > 0.1f && !isLevelEnd)
            {
                horizontal = -1;
                turnLeftButton.isButtonPressed = false;
                CountNumberOfMoves();
            }

            if (turnRightButton.isButtonPressed && transform.localScale.y > 0.1f && !isLevelEnd)
            {
                horizontal = 1;
                turnRightButton.isButtonPressed = false;
                CountNumberOfMoves();
            }

            //if (Input.GetAxisRaw("Vertical") != 0 && transform.localScale.y > 0.1f && !isLevelEnd)
            if (transform.localScale.y > 0.1f && !isLevelEnd)
            {
                //vertical = Input.GetAxisRaw("Vertical");
                vertical = 1;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump = true;
            }

            // Touch control
            if (Mathf.Abs(joystick.Horizontal) > 0.2f && transform.localScale.y > 0.1f && !isLevelEnd)
                horizontal = joystick.Horizontal;

            if (Mathf.Abs(joystick.Vertical) > 0.2f && transform.localScale.y > 0.1f && !isLevelEnd)
                vertical = joystick.Vertical;

            if (horizontal != 0)
            {
                MoveHorizontalInitial();
            }

            if (isMoveHorizontal)
            {
                IfDontReachedPositionX();
            }
            else
            {
                IfDontReachedPositionX();

                if (ifDontReachedPositionX)
                {
                    AutoReachThePositionX();
                }
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
    }

    

    private void FixedUpdate()
    {
        if (vertical != 0)
        {
            MoveVertical();
        }

        if (horizontal != 0)
        {
            //MoveHorizontalInitial();
        }

        if (isMoveHorizontal)
        {
            MoveHorizontalRigidbody();
        }

        //if (jump != 0)
        if (jump)
        {
            if (IsGrounded())
            {
                rb.AddForce(new Vector3(0f, jumpForce, 0f));
                afterJump = true;
            }
            //jump = 0;
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

        if (rb.velocity.x > maxVelocity)
        {
            rb.velocity = new Vector3(maxVelocity - 0.01f, rb.velocity.y, rb.velocity.z);
        }

        if (rb.velocity.x < -maxVelocity)
        {
            rb.velocity = new Vector3(-maxVelocity + 0.01f, rb.velocity.y, rb.velocity.z);
        }

        if (rb.velocity.z > maxVelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxVelocity - 0.01f);
        }
        
        if (rb.velocity.z < -maxVelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -maxVelocity + 0.01f);
        }
        
    }

    private void Move()
    {
        // Movement control by using rotation and force in the right proportions
        float addTorquePart = 1 - moveProportionSet;
        float addForcePart = 1 - addTorquePart;

        rb.AddTorque(vertical * speed * addTorquePart, 0f, -horizontal/2 * speed * addTorquePart);
        
        if (IsGrounded())
        {
            rb.AddForce(horizontal/2 * speed / 10 * addForcePart, 0f, vertical * speed / 10 * addForcePart, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(horizontal/2 * speed / 100, 0f, vertical * speed / 100, ForceMode.Impulse);
        }

        vertical = 0;
    }

    private void MoveVertical()
    {
        // Movement control by using rotation and force in the right proportions
        float addTorquePart = 1 - moveProportionSet;
        float addForcePart = 1 - addTorquePart;

        rb.AddTorque(vertical * speed * addTorquePart, 0f, 0f);

        if (IsGrounded())
        {
            rb.AddForce(0f, 0f, vertical * speed / 10 * addForcePart, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(0f, 0f, vertical * speed / 100, ForceMode.Impulse);
        }

        vertical = 0;
    }

    private void MoveHorizontalInitial()
    {
        if (horizontal == 1)
        {
            turnRight = true;
            turnLeft = false;
        }
        else if (horizontal == -1)
        {
            turnLeft = true;
            turnRight = false;
        }

        isMoveHorizontal = true;

        horizontal = 0;
    }

    private void MoveHorizontal()
    {
        if (IsGrounded() || transform.position.y > 0)
        {
            moduloPositionX = transform.position.x % portionToMoveX;

            initialPortionToMove += decreasePortionBy;

            if (Mathf.Abs(moduloPositionX) > decreasePortionBy || initialPortionToMove < decreasePortionBy * 3)
            {
                if (turnRight)
                {
                    transform.Translate(new Vector3(decreasePortionBy, 0f, 0f), Space.World);
                }
                else if (turnLeft)
                {
                    transform.Translate(new Vector3(-decreasePortionBy, 0f, 0f), Space.World);
                }
            }
            else
            {
                rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);
                isMoveHorizontal = false;
                initialPortionToMove = 0;
            } 
        }   
    }

    // MoveHorizontalRigidbody method moves the player along the X axis by a value close to the portionToMoveX value.
    // In other words, the method moves the player to a position equal to the portionToMoveX value, or a multiple of the portionToMoveX value.
    // MoveHorizontalRigidbody method mainly uses the Translate method to change positions and supportively uses the AddForce method.
    private void MoveHorizontalRigidbody()
    {
        if (IsGrounded() || transform.position.y > 0)
        {
            //moduloPositionX = transform.position.x % portionToMoveX;

            initialPortionToMove += decreasePortionBy;

            if (ifDontReachedPositionX || initialPortionToMove < decreasePortionBy * 5)
            {
                if (turnRight)
                {
                    transform.Translate(new Vector3(decreasePortionBy * Time.deltaTime * staticHorizontalSpeed, 0f, 0f), Space.World);
                    //transform.position = new Vector3(transform.position.x + decreasePortionBy * Time.deltaTime * staticHorizontalSpeed, transform.position.y, transform.position.z);

                    if (portionsOfAddForce < maxPortionsOfAddForce)
                    {
                        //Debug.Log("rb.velocity.x = " + rb.velocity.x);
                        portionsOfAddForce++;
                        rb.AddForce(horizontalSpeed * Time.deltaTime, 0f, 0f, ForceMode.Impulse);
                    }
                }
                else if (turnLeft)
                {
                    transform.Translate(new Vector3(-decreasePortionBy * Time.deltaTime * staticHorizontalSpeed, 0f, 0f), Space.World);
                    //transform.position = new Vector3(transform.position.x - decreasePortionBy * Time.deltaTime * staticHorizontalSpeed, transform.position.y, transform.position.z);

                    //if (Mathf.Abs(rb.velocity.x) < maxHorizontalVelocity)
                    if (portionsOfAddForce < maxPortionsOfAddForce)
                    {
                        portionsOfAddForce++;
                        rb.AddForce(-horizontalSpeed * Time.deltaTime, 0f, 0f, ForceMode.Impulse);
                    }
                }
            }
            else
            {
                //Debug.Log("Zero rb.velocity.x");
                rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);
                initialPortionToMove = 0;
                portionsOfAddForce = 0;
                
                if (numberOfMoves > 0) numberOfMoves--;
                else numberOfMoves++;

                if (numberOfMoves == 0)
                {
                    isMoveHorizontal = false;
                }
            }
        }
    }

    // Corrects the player position after after executing MoveHorizontalRigidbody method.
    private void AutoReachThePositionX()
    {
        Debug.Log("AutoReachThePositionX()");

        if (IsGrounded() || transform.position.y > 0)
        {
            float multiplesOfThePortionToMove = Mathf.Abs(transform.position.x / portionToMoveX);

            while (multiplesOfThePortionToMove > 1f)
            {
                multiplesOfThePortionToMove -= 1f;
            }

            //moduloPositionX = transform.position.x % portionToMoveX;

            //initialPortionToMove += decreasePortionBy;

            //if (ifDontReachedPositionX || initialPortionToMove < decreasePortionBy * 5)
            //{
            if (transform.position.x > 0)
            {
                if (multiplesOfThePortionToMove > 0.5f)
                {
                    transform.Translate(new Vector3(decreasePortionBy * Time.deltaTime * staticHorizontalSpeed / 2, 0f, 0f), Space.World);
                    //transform.position = new Vector3(transform.position.x + decreasePortionBy * Time.deltaTime * staticHorizontalSpeed, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.Translate(new Vector3(-decreasePortionBy * Time.deltaTime * staticHorizontalSpeed / 2, 0f, 0f), Space.World);
                    //transform.position = new Vector3(transform.position.x - decreasePortionBy * Time.deltaTime * staticHorizontalSpeed, transform.position.y, transform.position.z);
                }
            }
            else 
            {
                if (multiplesOfThePortionToMove < 0.5f)
                {
                    transform.Translate(new Vector3(decreasePortionBy * Time.deltaTime * staticHorizontalSpeed / 2, 0f, 0f), Space.World);
                    //transform.position = new Vector3(transform.position.x + decreasePortionBy * Time.deltaTime * staticHorizontalSpeed, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.Translate(new Vector3(-decreasePortionBy * Time.deltaTime * staticHorizontalSpeed / 2, 0f, 0f), Space.World);
                    //transform.position = new Vector3(transform.position.x - decreasePortionBy * Time.deltaTime * staticHorizontalSpeed, transform.position.y, transform.position.z);
                }
            }
            
            //}
            //else
            //{
            //    Debug.Log("Zero rb.velocity.x");
            //    rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);
            //    isMoveHorizontal = false;
            //    initialPortionToMove = 0;
            //    portionsOfAddForce = 0;
            //}
        }
    }

    // IfDontReachedPositionX method checks if the player reaches the position.x = 0 or a multiple of the portionToMoveX value
    private bool IfDontReachedPositionX()
    {
        moduloPositionX = transform.position.x % portionToMoveX;

        if (isMoveHorizontal)
        {
            decreasePortionByMultiplier = 3;
        }
        else decreasePortionByMultiplier = 2;

        if (Mathf.Abs(moduloPositionX) > decreasePortionBy * decreasePortionByMultiplier) return ifDontReachedPositionX = true;
        else return ifDontReachedPositionX = false;
    }

    // CountNumberOfMoves method counts how many moves player have to make. Number of moves depends on numbers of pressed keys or buttons.
    // CountNumberOfMoves method improves / makes it easier to change the track by several positions
    private void CountNumberOfMoves()
    {
        if (horizontal < 0)
        {
            if (numberOfMoves < 0)
            {
                numberOfMoves--;
            }
            else
            {
                numberOfMoves = 0;
                numberOfMoves--;
            }
        }
        else
        {
            if (numberOfMoves > 0)
            {
                numberOfMoves++;
            }
            else
            {
                numberOfMoves = 0;
                numberOfMoves++;
            }
        }
    }

    public void IsJump()
    {
        jump = true;
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
            if (hit.distance <= transform.localScale.x / 1.8f)
                return true;
            else
            {
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
