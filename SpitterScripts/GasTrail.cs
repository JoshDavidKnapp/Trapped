using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTrail : Ability
{
    //How long the player will emit gas
    public float gasTrailDuration;
    //How long until can use gas again

    public PlayerLocomotion playerLocomotion;

    public float speedMod;
    
    
    //Time between each tick of gas damage
    public float gasDamageTick;
    //Amount of damage gas does
    
    public float gasDamage;
    //additional damage
    public float gasDamageMultiplyer;

    //Checks if player can gas
    

    //Gas trail prefab
    public GameObject gasTrail;

    //play audio for gas trail
    public AudioSource gasHiss;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if player hits left ctrl
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            //start gas
           StartCoroutine(StartGasTrail());
        }
    }

    public IEnumerator StartGasTrail()
    {
        //if player can gas
        if( canUse&& gameObject.GetComponent<PlayerLocomotion>().canMove)
        {
            gasHiss.Play();

            //cant gas until cooldown is over
            canUse = false;
            playerLocomotion.movementSpeedModifier += speedMod;
            PlayerStats playerStats = GetComponent<PlayerStats>();
            gasDamage = playerStats.baseDamage * gasDamageMultiplyer;

            StartCoroutine(gasTick());

            //wait for cooldown
            yield return new WaitForSeconds(cooldown);

            //can gas again
            canUse = true;
        }

    }

    IEnumerator gasTick()
    {
        //gets player position
        Vector3 currentPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        //releases gas for the duration
        for (int i = 0; i < gasTrailDuration; i++)
        {
            //get player position
            currentPos = this.gameObject.transform.position;
            //instantiate gas at player location
            Instantiate(gasTrail, currentPos, Quaternion.identity);
            //wait to release gas
            yield return new WaitForSeconds(1f);

        }
        playerLocomotion.movementSpeedModifier -= speedMod;
        gasHiss.Stop();
    }


}
