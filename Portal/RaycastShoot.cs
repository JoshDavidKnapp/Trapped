using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastShoot : Primary
{
    //Player reference
    private GameObject playerGameObject;

    public GameObject spikeExplosion;


    private PlayerStats playerStats;
    //Muzzle flash particle system reference
    public ParticleSystem muzzleFlash;
    public ParticleSystem hitMarker;

    public float damageMultiplier;

    public TextMeshPro damageText;

    public GameObject bulletCasing;

    public GameObject gunObject;

    public GameObject bulletObject;

    public GameObject bulletSpawn;

    //time between autofire shots
    public float timeBetweenShot = 0.1f;
    //bool to wait for next auto shot
    private bool fireNext = true;

    [Header("Damage Zones")]
    public float distanceZone1;
    public float distanceZone2;
    public float distanceZone3;
    public float distanceZone4;
    public float distanceZone5;

    private float distanceShot;

    private void Awake()
    {
       

    }

    // Start is called before the first frame update
    void Start()
    {
        //Get Player Locomotion Script
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        PlayerLocomotion playerLocomotion = playerGameObject.GetComponent<PlayerLocomotion>();
        playerStats = playerGameObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerGameObject.GetComponent<PlayerLocomotion>().canMove)
        {
            //Raycast variable
            RaycastHit hit;

            //Draw Ray for Debugging, comment in final version
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1000f))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);

            }

            //If player left clicks
            if (Input.GetMouseButton(0)&&fireNext)
            {

                GetComponentInParent<PlayerStats>().isCombat = true;
                StartCoroutine(GetComponentInParent<PlayerStats>().CombatCheck());

                if(playerGameObject.GetComponent<Augment>().spikedExplosion == true)
                {
                    int rand = Random.Range(0, 10);
                    if (rand == 0)
                    {

                        if(hit.transform.gameObject.GetComponent<EnemyDamage>())
                        {

                            if (playerGameObject.GetComponent<Augment>().canLifeDrain)
                            {
                                playerGameObject.GetComponent<Augment>().LifeDrain();
                            }

                            Instantiate(spikeExplosion, hit.transform.position, Quaternion.identity);

                            EnemyDamage enemyScript = hit.transform.gameObject.GetComponent<EnemyDamage>();

                            //enemyScript.damageTaken += (int)(((playerStats.baseDamage * damageMultiplier)) * (distanceShot / 100));
                            enemyScript.damageTaken += (int)criticalStrikeCheck((playerStats.baseDamage * damageMultiplier) * (distanceShot / 100));
                            damageText.text = ((int)enemyScript.damageTaken).ToString();

                            if (enemyScript.damageTaken > 0)
                            {
                                Instantiate(damageText, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), Quaternion.identity);

                            }
                        }
                        
                    }

                }

                //Get PlayerLocomotion Script to check sprinting
                PlayerLocomotion playerLocomotion = playerGameObject.GetComponent<PlayerLocomotion>();

                //if player isn't sprinting
                if (!playerLocomotion.isSprinting)
                {

                    if (playerGameObject.GetComponent<Augment>().canStartWeaaponRepeat)
                    {
                        playerGameObject.GetComponent<Augment>().WeaponRepeater();

                    }

                    if (playerGameObject.GetComponent<Augment>().weaponRepeat && playerGameObject.GetComponent<Augment>().canStartWeaaponRepeat)
                    {
                                            
                        //play right flash
                        muzzleFlash.Play();

                        Instantiate(bulletCasing, gunObject.transform.position, Quaternion.Euler(90, playerGameObject.transform.rotation.eulerAngles.y, 0));

                        Instantiate(bulletObject, bulletSpawn.transform.position, Quaternion.Euler(90, 90, 90));

                        //create ray from camera to cente rof the screen
                        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1000f))
                        {



                            if (hit.distance <= distanceZone1)
                            {
                                distanceShot = 100;
                            }
                            if (hit.distance > distanceZone1 && hit.distance <= distanceZone2)
                            {
                                distanceShot = 75;
                            }
                            if (hit.distance > distanceZone2 && hit.distance <= distanceZone3)
                            {
                                distanceShot = 50;
                            }
                            if (hit.distance > distanceZone3 && hit.distance <= distanceZone4)
                            {
                                distanceShot = 25;
                            }
                            if (hit.distance > distanceZone5)
                            {
                                distanceShot = 0;
                            }
                            waitForEffect(hit.point);
                            //Draw ray for debugging
                            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);

                            if (hit.transform.gameObject.GetComponent<EnemyDamage>() && hit.distance < distanceZone5)
                            {


                                EnemyDamage enemyScript = hit.transform.gameObject.GetComponent<EnemyDamage>();

                                //enemyScript.damageTaken += (int)(((playerStats.baseDamage * damageMultiplier)) * (distanceShot / 100));
                                enemyScript.damageTaken += (int)criticalStrikeCheck((playerStats.baseDamage * damageMultiplier) * (distanceShot / 100));
                                damageText.text = ((int)enemyScript.damageTaken).ToString();

                                if (enemyScript.damageTaken > 0)
                                {
                                    Instantiate(damageText, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), Quaternion.identity);

                                }
                            }
                            //Tristin's Add 4/27 ---- Bubble Shields
                            if (hit.transform.gameObject.GetComponent<BubbleShield>() && hit.distance < distanceZone5)
                            {
                                BubbleShield shieldScript = hit.transform.gameObject.GetComponent<BubbleShield>();

                                shieldScript.damageTaken += (int)criticalStrikeCheck((playerStats.baseDamage * damageMultiplier) * (distanceShot / 100));
                            }
                            //for spawners ---- Tristin's Add
                            if (hit.transform.gameObject.GetComponent<EnemySpawnEngine>() && hit.distance < distanceZone5)
                            {

                                EnemySpawnEngine spawnScript = hit.transform.gameObject.GetComponent<EnemySpawnEngine>();

                                spawnScript.damageTaken += (int)(((playerStats.baseDamage * damageMultiplier)) * (distanceShot / 100));

                                damageText.text = ((int)(spawnScript.damageTaken)).ToString();
                                if (spawnScript.damageTaken > 0)
                                {
                                    Instantiate(damageText, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), Quaternion.identity);

                                }
                            }
                            //for Hunter Commando Boss -- Tristin's Add 3/24
                            if (hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>() && hit.distance < distanceZone5)
                            {
                                if (hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>().stunReady && !hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>().parent.GetComponent<BossAction>().stealthed)
                                {
                                    hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>().GenStun();
                                }
                                else if (!hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>().parent.GetComponent<BossAction>().stealthed)
                                {
                                    BossDamage damageScript = hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>().parent.GetComponent<BossDamage>();

                                    int damageDone = (int)(((playerStats.baseDamage * damageMultiplier)) * (distanceShot / 100));

                                    if (damageDone > 0)
                                    {
                                        damageScript.damageTaken += damageDone;
                                        damageText.text = damageDone.ToString();
                                        Instantiate(damageText, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), Quaternion.identity);
                                    }
                                }
                            }
                            else if (hit.transform.gameObject.GetComponent<BossDamage>() && hit.distance < distanceZone5)
                            {
                                if (!hit.transform.gameObject.GetComponent<BossAction>().stealthed)
                                {
                                    BossDamage damageScript = hit.transform.gameObject.GetComponent<BossDamage>();

                                    int damageDone = (int)(((playerStats.baseDamage * damageMultiplier)) * (distanceShot / 100));

                                    if (damageDone > 0)
                                    {
                                        damageScript.damageTaken += damageDone;
                                        damageText.text = damageDone.ToString();
                                        Instantiate(damageText, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), Quaternion.identity);
                                    }
                                }
                            }
                            //Print the name of the object hit
                            Debug.Log(hit.transform.name);


                        }
                    }


                    //wait for the next auto shot
                    fireNext = false;
                    StartCoroutine(DoubleShot());


                    //play right flash
                    muzzleFlash.Play();

                    Instantiate(bulletCasing, gunObject.transform.position, Quaternion.Euler(90, playerGameObject.transform.rotation.eulerAngles.y, 0));

                    //Instantiate(bulletObject, bulletSpawn.transform.position, Quaternion.Euler(90, 90, 90));

                    //create ray from camera to cente rof the screen
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1000f))
                    {

                        if (hit.distance <= distanceZone1)
                        {
                            distanceShot = 100;
                        }
                        if (hit.distance > distanceZone1 && hit.distance <= distanceZone2)
                        {
                            distanceShot = 75;
                        }
                        if (hit.distance > distanceZone2 && hit.distance <= distanceZone3)
                        {
                            distanceShot = 50;
                        }
                        if (hit.distance > distanceZone3 && hit.distance <= distanceZone4)
                        {
                            distanceShot = 25;
                        }
                        if (hit.distance > distanceZone5)
                        {
                            distanceShot = 0;
                        }
                       
                        //Draw ray for debugging
                        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);

                        if (hit.transform.gameObject.GetComponent<EnemyDamage>() && hit.distance < distanceZone5)
                        {
                            EnemyDamage enemyScript = hit.transform.gameObject.GetComponent<EnemyDamage>();

                            //enemyScript.damageTaken += (int)(((playerStats.baseDamage * damageMultiplier)) * (distanceShot / 100));
                            enemyScript.damageTaken += (int)criticalStrikeCheck((playerStats.baseDamage * damageMultiplier) * (distanceShot / 100));
                            damageText.text = ((int)enemyScript.damageTaken).ToString();

                            if(enemyScript.damageTaken > 0)
                            {
                                Instantiate(damageText, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), Quaternion.identity);

                            }
                        }
                        //Tristin's Add 4/27 ---- Bubble Shields
                        if (hit.transform.gameObject.GetComponent<BubbleShield>() && hit.distance < distanceZone5)
                        {
                            BubbleShield shieldScript = hit.transform.gameObject.GetComponent<BubbleShield>();

                            shieldScript.damageTaken += (int)criticalStrikeCheck((playerStats.baseDamage * damageMultiplier) * (distanceShot / 100));
                        }
                        //for spawners ---- Tristin's Add
                        if (hit.transform.gameObject.GetComponent<EnemySpawnEngine>() && hit.distance < distanceZone5)
                        {

                            EnemySpawnEngine spawnScript = hit.transform.gameObject.GetComponent<EnemySpawnEngine>();

                            spawnScript.damageTaken += (int)(((playerStats.baseDamage * damageMultiplier)) * (distanceShot / 100));

                            damageText.text = ((int)(spawnScript.damageTaken)).ToString();
                            if(spawnScript.damageTaken > 0)
                            {
                                Instantiate(damageText, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), Quaternion.identity);

                            }
                        }
                        //for Hunter Commando Boss -- Tristin's Add 3/24
                        if (hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>() && hit.distance < distanceZone5)
                        {
                            if (hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>().stunReady && !hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>().parent.GetComponent<BossAction>().stealthed)
                            {
                                hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>().GenStun();
                            }
                            else if (!hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>().parent.GetComponent<BossAction>().stealthed)
                            {
                                BossDamage damageScript = hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>().parent.GetComponent<BossDamage>();

                                int damageDone = (int)(((playerStats.baseDamage * damageMultiplier)) * (distanceShot / 100));

                                if (damageDone > 0)
                                {
                                    damageScript.damageTaken += damageDone;
                                    damageText.text = damageDone.ToString();
                                    Instantiate(damageText, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), Quaternion.identity);
                                }
                            }
                        }
                        else if (hit.transform.gameObject.GetComponent<BossDamage>() && hit.distance < distanceZone5)
                        {
                            if (!hit.transform.gameObject.GetComponent<BossAction>().stealthed)
                            {
                                BossDamage damageScript = hit.transform.gameObject.GetComponent<BossDamage>();

                                int damageDone = (int)(((playerStats.baseDamage * damageMultiplier)) * (distanceShot / 100));

                                if (damageDone > 0)
                                {
                                    damageScript.damageTaken += damageDone;
                                    damageText.text = damageDone.ToString();
                                    Instantiate(damageText, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), Quaternion.identity);
                                }
                            }
                        }

                        //Print the name of the object hit
                        Debug.Log(hit.transform.name);
                        if (distanceShot > 0)
                        {
                            StartCoroutine(waitForEffect(hit.point));
                        }
                    }
                }


            }
        }
       

       

    }
    IEnumerator waitForNextShot()
    {
        yield return new WaitForSeconds(timeBetweenShot);
        fireNext = true;


    }   

    IEnumerator DoubleShot()
    {
        yield return new WaitForSeconds(timeBetweenShot *1.5f);
        fireNext = true;

    }


    IEnumerator waitForEffect(Vector3 hitPos)
    {
        Debug.Log("Hit Particle");
       
        ParticleSystem quickParticle;
        quickParticle = Instantiate(hitMarker);
        quickParticle.transform.position = hitPos;
        //quickParticle.transform.localScale = new Vector3(4, 4, 4);
        quickParticle.Play();
        yield return new WaitForSeconds(.3f);
        Destroy(quickParticle);

    }




}
