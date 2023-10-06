using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestRocket : Ability
{
    //Projectile Refrence
    public GameObject projectilePrefab;

    //Player reference
    private GameObject playerGameObject;

    [System.NonSerialized]
    public float rocketDamage;

   

    


    // Start is called before the first frame update
    void Start()
    {
        //Get PlayerLocomotion script
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        PlayerLocomotion playerLocomotion = playerGameObject.GetComponent<PlayerLocomotion>();
        PlayerStats playerStats = playerGameObject.GetComponent<PlayerStats>();
        rocketDamage = playerStats.baseDamage;
    }

    // Update is called once per frame
    void Update()
    {
        //If player hits G
        if (Input.GetKeyDown(KeyCode.LeftControl) && playerGameObject.GetComponent<PlayerLocomotion>().canMove)
        {
            StartCoroutine(ShootRocket());

        }

    }

    public IEnumerator ShootRocket()
    {
        //Get player locomotion script to check sprinting
        PlayerLocomotion playerLocomotion = playerGameObject.GetComponent<PlayerLocomotion>();

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
