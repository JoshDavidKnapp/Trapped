using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAttack : Primary
{//Projectile Refrence
    public GameObject projectilePrefab;

    //Player reference
    private GameObject playerGameObject;

    
    

    

    
    // Start is called before the first frame update
    void Start()
    {
        //Get PlayerLocomotion script
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        PlayerLocomotion playerLocomotion = playerGameObject.GetComponent<PlayerLocomotion>();
        PlayerStats playerStats = playerGameObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGameObject.GetComponent<PlayerLocomotion>().canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(ShootRocket());

            }
        }

       

    }

    public IEnumerator ShootRocket()
    {
        GetComponentInParent<PlayerStats>().isCombat = true;
        StartCoroutine(GetComponentInParent<PlayerStats>().CombatCheck());

        //Get player locomotion script to check sprinting
        PlayerLocomotion playerLocomotion = playerGameObject.GetComponent<PlayerLocomotion>();

        if (playerGameObject.GetComponent<Augment>().canStartWeaaponRepeat)
        {
            playerGameObject.GetComponent<Augment>().WeaponRepeater();

        }

        if (playerGameObject.GetComponent<Augment>().weaponRepeat && playerGameObject.GetComponent<Augment>().canStartWeaaponRepeat)
        {
            canUse = false;

            //Spawn prefab in front of player
            Instantiate(projectilePrefab, this.gameObject.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(cooldown);

            canUse = true;
        }

        //if player isn't sprinting
        if (!playerLocomotion.isSprinting && canUse)
        {

            

            canUse = false;

            //Spawn prefab in front of player
            Instantiate(projectilePrefab, this.gameObject.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(cooldown);

            canUse = true;
        }
    }
    
}
