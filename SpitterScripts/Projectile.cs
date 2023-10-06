using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Projectile : Primary
{
    //mixer reference
    public AudioMixer mixer;

    //Projectile Refrence
    public GameObject projectilePrefab;

    //Player reference
    private GameObject playerGameObject;

    public float spikeDamageTick;

    [System.NonSerialized]
    public float spikeDamage;

    //Tristin's Add -- Play Sound
    public AudioClip spikeSpit;

    

    // Start is called before the first frame update
    void Start()
    {
        //Get PlayerLocomotion script
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        PlayerLocomotion playerLocomotion = playerGameObject.GetComponent<PlayerLocomotion>();
        PlayerStats playerStats = playerGameObject.GetComponent<PlayerStats>();
        spikeDamage = playerStats.baseDamage;
    }

    // Update is called once per frame
    void Update()
    {
        //If player hits G
        if (Input.GetMouseButtonDown(0) && playerGameObject.GetComponent<PlayerLocomotion>().canMove)
        {
            StartCoroutine(ShootSpike());
                
        }

    }

    public IEnumerator ShootSpike()
    {

        GetComponentInParent<PlayerStats>().isCombat = true;
        StartCoroutine(GetComponentInParent<PlayerStats>().CombatCheck());

        //Get player locomotion script to check sprinting
        PlayerLocomotion playerLocomotion = playerGameObject.GetComponent<PlayerLocomotion>();

        //if player isn't sprinting
        if (!playerLocomotion.isSprinting && canUse)
        {

            if (playerGameObject.GetComponent<Augment>().canStartWeaaponRepeat)
            {
                playerGameObject.GetComponent<Augment>().WeaponRepeater();

            }

            if (playerGameObject.GetComponent<Augment>().weaponRepeat && playerGameObject.GetComponent<Augment>().canStartWeaaponRepeat)
            {
                canUse = false;

                //Tristin's Add ---- Play spit anim
                if (gameObject.transform.parent.gameObject.GetComponent<PlayerLocomotion>().characterRig != null)
                {
                    gameObject.transform.parent.gameObject.GetComponent<PlayerLocomotion>().characterRig.GetComponent<Animator>().SetBool("Attack", true);
                }

                //Tristin's Add -- Play Sound
                mixer.GetFloat("SFXExposedParam", out float repeatVolume);
                AudioSource.PlayClipAtPoint(spikeSpit, gameObject.transform.position, (repeatVolume + 80) / 80);

                //Spawn prefab in front of player
                Instantiate(projectilePrefab, this.gameObject.transform.position, Quaternion.identity);

                yield return new WaitForSeconds(cooldown);

                if (gameObject.transform.parent.gameObject.GetComponent<PlayerLocomotion>().characterRig != null)
                {
                    gameObject.transform.parent.gameObject.GetComponent<PlayerLocomotion>().characterRig.GetComponent<Animator>().SetBool("Attack", false);
                }

                canUse = true;
            }
            
            canUse = false;

            //Tristin's Add ---- Play spit anim
            if (gameObject.transform.parent.gameObject.GetComponent<PlayerLocomotion>().characterRig != null)
            {
                gameObject.transform.parent.gameObject.GetComponent<PlayerLocomotion>().characterRig.GetComponent<Animator>().SetBool("Attack", true);
            }

            //Tristin's Add -- Play Sound
            mixer.GetFloat("SFXExposedParam", out float volume);
            AudioSource.PlayClipAtPoint(spikeSpit, gameObject.transform.position, (volume + 80) / 80);

            //Spawn prefab in front of player
            Instantiate(projectilePrefab, this.gameObject.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(cooldown);

            if (gameObject.transform.parent.gameObject.GetComponent<PlayerLocomotion>().characterRig != null)
            {
                gameObject.transform.parent.gameObject.GetComponent<PlayerLocomotion>().characterRig.GetComponent<Animator>().SetBool("Attack", false);
            }

            canUse = true;
        }
    }
}
