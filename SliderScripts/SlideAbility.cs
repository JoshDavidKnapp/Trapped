using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideAbility : Ability
{
    //Player reference
    private GameObject playerGameObject;
    //Player rigidbody reference
    private Rigidbody playerRigidbody;
    //locomotion script
    PlayerLocomotion playerLocomotion;
    //Slide Speed
    public float slideSpeed = 500f;
    //How fast player can slide again
    
    //invinciblity frames
    public float slideInvincibility = 2f;

    //slide charges
    public int slideChargeTotal = 1;

    public int currentCharges = 1;

    private bool extraCharge,recharge;

    //confidentSlidebool
    public bool confidentSlide = false;

    //confident slide counters
    private float timeAfterSlide = 3, currentTimeAfterSlide = 3,tempCrit;

    private bool slid,addResist= false;
    //add weapon here for confident slidebuff
    public Primary weapon;

    //audio reference for sliding
    public AudioSource sliding;

    //Called before Start
    private void Awake()
    {
        //sets locomotion script
        playerLocomotion = this.gameObject.GetComponent<PlayerLocomotion>();
        //sets player gameObject
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        //sets player Rigidbody
        playerRigidbody = playerGameObject.GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if player hits left control
        if(Input.GetKeyDown(KeyCode.LeftControl) && playerGameObject.GetComponent<PlayerLocomotion>().canMove)
        {
            //start sliding
            Slide();
        }
        if (currentTimeAfterSlide < timeAfterSlide)
        {
            currentTimeAfterSlide += Time.deltaTime;
            weapon.criticalStrikeChance = 1;
            if (!addResist)
            {
                playerLocomotion.playerStats.damageResist += .1f;
                addResist = true;
            }
        }
        if (currentTimeAfterSlide > timeAfterSlide&& slid)
        {
            
            weapon.criticalStrikeChance = tempCrit;
            playerLocomotion.playerStats.damageResist -= .1f;
            slid = false;
            addResist = false;
        }


        //Debug.Log(playerRigidbody.velocity);
    }

    public void Slide()
    {
        
        //if the player is able to slide and on the ground
        if((extraCharge||canUse) && playerLocomotion.IsGrounded())
        {
            //play audio on slide
            sliding.Play();

            //cant slide until finished sliding
            canUse = false;
            extraCharge = false;

            //use a charge
            currentCharges--;
           

            //Gets player stats script
            PlayerStats playerStats = this.gameObject.GetComponent<PlayerStats>();
            //player cant take damage while sliding
            playerStats.canTakeDamage = false;

            //Gets player rigidbody
            Rigidbody playerRigidbody = playerGameObject.GetComponent<Rigidbody>();
            //Pushes the player forward
            playerRigidbody.AddForce(transform.forward * slideSpeed, ForceMode.Impulse);
            StartCoroutine(slideIvincibility(playerStats));
            //Wait until slide is done to allow player to take damage again
            if (!recharge)
            {
                StartCoroutine(Recharge(playerStats));
            }
            //player can take damage again
           

            //player is able to slide again
            

        }

    }
    public IEnumerator slideIvincibility(PlayerStats player)
    {

        yield return new WaitForSeconds(slideInvincibility);
        //if player can confident Slide
        if (confidentSlide)
        {
            currentTimeAfterSlide = 0;
            if (weapon.criticalStrikeChance < 1)
            {
                tempCrit = weapon.criticalStrikeChance;
            }
            slid = true;
        } 
        player.canTakeDamage = true;
        if (currentCharges > 0)
        {
            extraCharge = true;
        }


    }
    public IEnumerator Recharge(PlayerStats player)
    {
        recharge = true;
        canUse = false;
        yield return new WaitForSeconds(cooldown);
        currentCharges++;
        if (player.canTakeDamage)
        {
            extraCharge = true;
        }
        if (currentCharges < slideChargeTotal)
        {
            StartCoroutine(Recharge(player));
        }
        else
        {
            canUse = true;
            recharge = false;
        }

    }


}
