using UnityEngine;
using Cinemachine;
using System.Collections;

public class PlayerLocomotion : MonoBehaviour
{
    //Rigidbody Reference
    Rigidbody playerRigidbody;

    //Virtual Camera Reference
    public CinemachineVirtualCamera vcam;
    //Camera  reference

    private Camera mainCamera;
    //Player Transform
    public Transform cameraLookAt;

    //Zoom Field of View
    int targetFOV = 60;

    [Header("Movement Speed")]
    //PlayerMovementSpeed
    public float strafeSpeed = 7;
    public float movementSpeed = 7;
    public float sprintSpeed = 10;
    public float jumpForce = 150;
    public float origJumpForce;
    public float qsJump;
    public float iceMovementSpeed = 9000;

    //movementspeedmodifer to increase speed incrementally with upgrades
    public float movementSpeedModifier = 0;

    //sprint speed modifier
    public float sprintSpeedModifier = 0;

    //Distance to Ground for Jump Check
    private float distToGround;

    //Capsule Collider for Jump Check
    private Collider capsuleCollider;

   

    [Header("Bool")]
    public bool isAiming = false;
    public bool isSprinting = false;
    public bool onIce = false;
    public bool canMove = true;
    public bool canDoubleJump;
    private bool boosted;
    public bool doubleJumpActivated;
    [Header("Mouse Sensativity")]
    //Mouse
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    //audio source reference for walking/running
    private bool walk;
    private bool jump;
    [Header("Audio Source and Clips for Movement")]
    public AudioSource movement;

    public AudioClip walking;
    public AudioClip running;


    //Animator Reference
    Animator animator;

    //rigs to determine parameters to set
    [Header("Rigs to set parameters")]
    public GameObject characterRig;

    //Set isAiming in animator
    int isAimingParam = Animator.StringToHash("isAiming");

    //made public for confident slide
    [Header("Public Variables")]
    public PlayerStats playerStats;

    public Vector3 safeSpot;


    [Header ("Fall off Map")]
    public float respawnDamagePercent;

    //rotation for sideways movement
    private float rigRotation;
    private float rotateCalc;
    private bool forward;
    private bool backwards;

    //public GameObject camTransform;
    public bool IsGrounded()
    {
        //Returns if the raycast that shoots downwards is hitting the ground
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private void Awake()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
        //Sets the Player's Rigidbody
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //Set animator
        animator = GetComponent<Animator>();
        //Sets the Player's Capsule Collider
        capsuleCollider = GetComponent<CapsuleCollider>();
        //Sets the distance to the ground based on the capsule collider's y value
        distToGround = capsuleCollider.bounds.extents.y;
        canMove = true;
        StartCoroutine(TrackSafeSpot());
    }

    private void Update()
    {

        if(IsGrounded() && doubleJumpActivated)
        {
            canDoubleJump = true;
        }

        if (playerStats.boostedRegen&&!boosted)
        {
            movementSpeedModifier += 0.5f;
            boosted = true;
        }
        if (!playerStats.boostedRegen && boosted)
        {
            boosted = false;
            movementSpeedModifier-= .5f;
        }

       
        if(gameObject.transform.position.y < -50)
        {
            transform.position = safeSpot;
            playerStats.currentPlayerHealth -= playerStats.maxPlayerHealth * (respawnDamagePercent/100);
        }

       if(canMove)
        {
            //Stops camera movement when paused.
           // if (gameObject.GetComponent<PauseMenu>().Paused == false)
           // {
                //Track Mouse Position
               
          //  }
            xAxis.Update(Time.fixedDeltaTime);
            yAxis.Update(Time.fixedDeltaTime);

            //Move Camera to Mouse Position
            cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);

            /*
            //Aim toggle
            if (Input.GetMouseButtonDown(1))
            {
                //toggle
                isAiming = !isAiming;

                if(isAiming)
                {
                    //Zoom in Animation
                    animator.SetBool(isAimingParam, true);

                    //Sets Mouse speeds slower when aiming
                    yAxis.m_MaxSpeed = 150;
                    xAxis.m_MaxSpeed = 150;
                }
                else
                {
                    //Zoom out Animation
                    animator.SetBool(isAimingParam, false);

                    //Sets Mouse Speeds back to normal
                    yAxis.m_MaxSpeed = 300;
                    xAxis.m_MaxSpeed = 300;
                }
            }
            */

            //If Player right clicks
            if (Input.GetMouseButtonDown(1))
            {
                //Zoom in Animation
                animator.SetBool(isAimingParam, true);

                //Sets Mouse speeds slower when aiming
                yAxis.m_MaxSpeed = 150;
                xAxis.m_MaxSpeed = 150;

                //stop sprinting
                isSprinting = false;
                movementSpeed = strafeSpeed + strafeSpeed*movementSpeedModifier;

            }

            //If Player releases right click
            if (Input.GetMouseButtonUp(1))
            {
                //Zoom out Animation
                animator.SetBool(isAimingParam, false);

                //Sets Mouse Speeds back to normal
                yAxis.m_MaxSpeed = 300;
                xAxis.m_MaxSpeed = 300;

            }


            //If Player hits spsace while on the ground
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                //Player Jumps
                playerRigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

                if (characterRig != null)
                {
                    characterRig.GetComponent<Animator>().SetBool("Jump", true);

                    jump = true;
                }
            }

            //If Player hits spsace while on the ground
            if (Input.GetKeyDown(KeyCode.Space) && !IsGrounded() && canDoubleJump)
            {
                //Player Jumps
                canDoubleJump = false;
                playerRigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

                if (characterRig != null)
                {
                    characterRig.GetComponent<Animator>().SetBool("Jump", true);

                    jump = true;
                }

                if (IsGrounded())
                {
                    canDoubleJump = true;
                }
                
            }


            //If Player lets go of W
            if (Input.GetKeyUp(KeyCode.W) && !onIce)
            {
                //stop sprinting
                isSprinting = false;
                movementSpeed = strafeSpeed + strafeSpeed * movementSpeedModifier;

            }

            //If Player Hits Shift while walking
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //toggle
                isSprinting = !isSprinting;

                if (isSprinting)
                {
                    //Increase Movement Speed
                    movementSpeed = sprintSpeed + sprintSpeed * movementSpeedModifier + sprintSpeed*sprintSpeedModifier;
                    vcam.m_Lens.FieldOfView = 200;
                    movement.clip = running;
                }
                else
                {
                    //Decrease Movement Speed
                    movementSpeed = strafeSpeed+strafeSpeed*movementSpeedModifier;
                    vcam.m_Lens.FieldOfView = 90;
                    movement.clip = walking;

                }


            }
        }
    }

    

 
    private void FixedUpdate()
    {
       
       
        if (canMove)
        {
            walk = false;
            //if player hits WASD for animations
            //forward
            if (Input.GetKey(KeyCode.W))
            {
                forward = true;
                if (gameObject.GetComponent<GasTrail>() || gameObject.GetComponent<StealthGenerator>())
                {
                    rotateCalc += (0 - rotateCalc) / 10f;
                    if (rotateCalc < -45)
                    {
                        rotateCalc += 8f;
                    }
                    if (rotateCalc > 45)
                    {
                        rotateCalc -= 8f;
                    }
                }
                if (backwards)
                {
                    walk = false;
                }
                else
                {
                    walk = true;
                }
            }
            else
            {
                forward = false;
            }
            //back
            if (Input.GetKey(KeyCode.S))
            {
                backwards = true;
                if (gameObject.GetComponent<GasTrail>() || gameObject.GetComponent<StealthGenerator>())
                {
                    rotateCalc += (0 - rotateCalc) / 10f;
                    if (rotateCalc < -45)
                    {
                        rotateCalc += 5f;
                    }
                    if (rotateCalc > 45)
                    {
                        rotateCalc -= 5f;
                    }
                }
                if (forward)
                {
                    walk = false;
                }
                else
                {
                    walk = true;
                }
            }
            else
            {
                backwards = false;
            }
            //left
            if (Input.GetKey(KeyCode.A))
            {
                walk = true;
                if (gameObject.GetComponent<GasTrail>() || gameObject.GetComponent<StealthGenerator>())
                {
                    if (rotateCalc > -90)
                    {
                        rotateCalc -= 5f;
                    }
                    else
                    {
                        rotateCalc = -90;
                    }
                }
            }
            //right
            if (Input.GetKey(KeyCode.D))
            {
                walk = true;
                if (gameObject.GetComponent<GasTrail>() || gameObject.GetComponent<StealthGenerator>())
                {
                    if (rotateCalc < 90)
                    {
                        rotateCalc += 5f;
                    }
                    else
                    {
                        rotateCalc = 90;
                    }
                }
            }

            if (walk)
            {
                if (!IsGrounded())
                {
                    walk = false;
                    movement.Stop();

                    if (characterRig != null)
                    {
                        if (jump)
                        {
                            characterRig.GetComponent<Animator>().SetBool("Jump", true);
                        }
                        characterRig.GetComponent<Animator>().SetBool("Movement", false);
                    }
                    
                }
                else if (!movement.isPlaying)
                {
                    movement.Play();
                }

                if (IsGrounded() && characterRig != null)
                {
                    characterRig.GetComponent<Animator>().SetBool("Movement", true);
                    characterRig.GetComponent<Animator>().SetBool("Jump", false);
                }
            }
            else
            {
                movement.Stop();

                if (characterRig != null)
                {
                    if (jump)
                    {
                        characterRig.GetComponent<Animator>().SetBool("Jump", true);
                    }
                    characterRig.GetComponent<Animator>().SetBool("Movement", false);
                }

                if (IsGrounded() && characterRig != null)
                {
                    characterRig.GetComponent<Animator>().SetBool("Jump", false);
                }
                /*if (IsGrounded() && characterRig != null)
                {
                    jump = false;
                    characterRig.GetComponent<Animator>().SetBool("Jump", false);
                }
                else if (!IsGrounded() && jump && characterRig != null)
                {
                    characterRig.GetComponent<Animator>().SetBool("Jump", true);
                }*/
            }

            if (gameObject.GetComponent<GasTrail>() || gameObject.GetComponent<StealthGenerator>())
            {
                //forward backwards running flip
                float mod = 1;
                if (backwards)
                {
                    mod = -1;
                }
                if (forward)
                {
                    mod = 1;
                }
                characterRig.GetComponent<Animator>().SetFloat("runningSpeed", mod);

                //croc body rotation
                if (gameObject.GetComponent<GasTrail>() || gameObject.GetComponent<StealthGenerator>())
                {
                    if (!walk)
                    {
                        rotateCalc += (0 - rotateCalc) / 5f;
                    }

                    rigRotation += ((rotateCalc * mod) - rigRotation) / 5f;

                    characterRig.transform.localRotation = Quaternion.Euler(0, rigRotation, 0);
                }

                //mp guard
                if (gameObject.GetComponent<StealthGenerator>())
                {
                    
                }

            }

            if (!onIce)
            {
                //If Player Hits W
                if (Input.GetKey(KeyCode.W))
                {
                    //Move Player Forward
                    transform.position += transform.forward * Time.deltaTime * movementSpeed;
                }

                //If Player Hits A
                if (Input.GetKey(KeyCode.A))
                {
                    //Move Player Left
                    transform.position += -transform.right * Time.deltaTime * movementSpeed;
                }

                //If Player Hits D
                if (Input.GetKey(KeyCode.D))
                {
                    //Move Player Right
                    transform.position += transform.right * Time.deltaTime * movementSpeed;
                }

                //If Player Hits S
                if (Input.GetKey(KeyCode.S))
                {
                    //Move Player Back
                    transform.position += -transform.forward * Time.deltaTime * movementSpeed;
                }
            }
            if (onIce)
            {

                if (playerRigidbody.velocity.magnitude > 18)
                {
                    playerRigidbody.velocity = Vector3.ClampMagnitude(playerRigidbody.velocity, 18);
                }



                Debug.Log(playerRigidbody.velocity);

                //If Player Hits W
                if (Input.GetKey(KeyCode.W))
                {
                    //Move Player Forward
                    playerRigidbody.AddForce(transform.forward * iceMovementSpeed * Time.deltaTime);
                }

                //If Player Hits A
                if (Input.GetKey(KeyCode.A))
                {
                    //Move Player Left
                    //transform.position += -transform.right * Time.deltaTime * strafeSpeed;
                    playerRigidbody.AddForce(-transform.right * iceMovementSpeed * Time.deltaTime);
                }

                //If Player Hits D
                if (Input.GetKey(KeyCode.D))
                {
                    //Move Player Right
                    //transform.position += transform.right * Time.deltaTime * strafeSpeed;
                    playerRigidbody.AddForce(transform.right * iceMovementSpeed * Time.deltaTime);
                }

                //If Player Hits S
                if (Input.GetKey(KeyCode.S))
                {
                    //Move Player Back
                    // transform.position += -transform.forward * Time.deltaTime * strafeSpeed;
                    playerRigidbody.AddForce(-transform.forward * iceMovementSpeed * Time.deltaTime);
                }
            }

            if (isSprinting)
            {
                movement.clip = running;
            }
            else
            {
                movement.clip = walking;
            }
        }
    }

    public IEnumerator TrackSafeSpot()
    {
        if (IsGrounded())
        {
            safeSpot = gameObject.transform.position;
        }

        yield return new WaitForSeconds(2);

        StartCoroutine(TrackSafeSpot());
    }

    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.tag == "Ice")
        {
            if(!onIce)
            {
                onIce = true;

            }
        }

        if(other.gameObject.tag == "Floor")
        {

            if(onIce)
            {
                playerRigidbody.velocity = new Vector3(0, 0, 0);
                onIce = false;

            }

        }


    }

    private void OnCollisionEnter(Collision other)
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "IED" && GetComponent<PlayerStats>().canTakeDamage)
        {
            other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            playerRigidbody.AddExplosionForce(other.gameObject.GetComponent<DestroyOnPlayerCollision>().iedKnockback, other.transform.position, 10);
            playerRigidbody.AddForce(0, 150, 0, ForceMode.Impulse);
            playerStats.TakeDamage(other.gameObject.GetComponent<DestroyOnPlayerCollision>().iedDamage);
           
        }

        if(other.gameObject.tag == "Quicksand")
        {
            movementSpeed = 1;
            origJumpForce = jumpForce;
            qsJump = jumpForce / 3;
            jumpForce = qsJump;
        }

        if(other.gameObject.tag == "Laser")
        {
            Debug.Log("laserhit");

           if(GetComponent<PlayerStats>().canTakeDamage)
           {
                playerStats.TakeDamage(other.gameObject.GetComponentInParent<RotateLaser>().laserDamage);
                playerRigidbody.AddExplosionForce(other.gameObject.GetComponentInParent<RotateLaser>().laserKnockback, other.transform.position, 14);

            }

        }

        if (other.gameObject.tag == "Vine" && GetComponent<PlayerStats>().canTakeDamage)
        {
            playerRigidbody.AddExplosionForce(other.gameObject.GetComponent<Vine>().vineKnockback, other.transform.position, 1);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Quicksand")
        {
            movementSpeed = strafeSpeed;
            jumpForce = origJumpForce;
        }
    }

    public IEnumerator Stun(float stunDuration)
    {
        canMove = false;
        yield return new WaitForSeconds(stunDuration); //enemy stun duration
        canMove = true;
    }

    






}


