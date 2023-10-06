using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsecutiveShots : Primary
{
    //Player reference
    private GameObject playerGameObject;

    public GameObject spikeExplosion;

    //Boolean to check if reloaded


    //Muzzle flash particle system reference
    public ParticleSystem muzzleFlash;

    //Muzzle flash 2nd particle system reference
    public ParticleSystem muzzleFlash2;

    public ParticleSystem hitMarker;

    public int shotsFired = 4;

    //Time to wait between each shot
    public float attackSpeed;
    //Time it takes to reload


    public float damageMultiplier;

    private PlayerStats playerStats;

    public TextMeshPro damageText;

    public GameObject bulletCasing;

    public GameObject bulletObject;

    public bool emerge = false;
    private float specialDamageMod = 1.5f;

    [Header("Damage Zones")]
    public float distanceZone1;
    public float distanceZone2;
    public float distanceZone3;
    public float distanceZone4;
    public float distanceZone5;

    private float distanceShot;

    //animator reference
    [Header("Rig for Firing Animation Trigger")]
    public Animator mpRig;


    //public GameObject bulletSpawn;

    private void Awake()
    {
        //Get Player Locomotion Script
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        PlayerLocomotion playerLocomotion = playerGameObject.GetComponent<PlayerLocomotion>();
        playerStats = playerGameObject.GetComponent<PlayerStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if (playerGameObject.GetComponent<PlayerLocomotion>().canMove)
        {
            //If player left clicks
            if (Input.GetMouseButton(0))
            {
                StartCoroutine(Shoot());

            }
        }
       

       // if(Input.GetMouseButtonUp(0))
        {
           // StopCoroutine(Shoot());
        }

    }

    public IEnumerator Shoot()
    {
        //Raycast variable
        RaycastHit hit;

        //play firing animation
        mpRig.SetTrigger("Shoot");

        //Get PlayerLocomotion Script to check sprinting
        PlayerLocomotion playerLocomotion = playerGameObject.GetComponent<PlayerLocomotion>();

        //if player isn't sprinting
        if (!playerLocomotion.isSprinting && canUse)
        {
            //Can't shoot again until firing the 4 shots
            canUse = false;



            StartCoroutine(waitForCoolDown());
            //for loop for firing shots
            for (int i = 0; i < shotsFired; i++)
            {




                if (playerGameObject.GetComponent<Augment>().canStartWeaaponRepeat)
                {
                    playerGameObject.GetComponent<Augment>().WeaponRepeater();

                }

                if (playerGameObject.GetComponent<Augment>().weaponRepeat && playerGameObject.GetComponent<Augment>().canStartWeaaponRepeat)
                {

                    for (int j = 0; j < shotsFired; j++)
                    {
                       
                        //if i is even
                        if (i % 2 == 0)
                        {
                            //play right flash
                            muzzleFlash.Play();

                            Instantiate(bulletCasing, muzzleFlash.transform.position, Quaternion.Euler(90, playerGameObject.transform.rotation.eulerAngles.y, 0));

                           // Instantiate(bulletObject, muzzleFlash.transform.position, Quaternion.Euler(90, 90, 90));


                        }
                        else
                        {
                            //play left muzzle flash
                            muzzleFlash2.Play();

                            Instantiate(bulletCasing, muzzleFlash2.transform.position, Quaternion.Euler(90, playerGameObject.transform.rotation.eulerAngles.y, 0));

                           // Instantiate(bulletObject, muzzleFlash2.transform.position, Quaternion.Euler(90, 90, 90));
                        }


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

                            float tempDamageMod;
                            if (emerge)
                                tempDamageMod = specialDamageMod;
                            else
                                tempDamageMod = damageMultiplier;

                            if (hit.transform.gameObject.GetComponent<EnemyDamage>() && hit.distance < distanceZone5)
                            {


                                EnemyDamage enemyScript = hit.transform.gameObject.GetComponent<EnemyDamage>();

                               

                                //enemyScript.damageTaken += (int)(((playerStats.baseDamage * damageMultiplier)) * (distanceShot/100));
                                enemyScript.damageTaken += (int)criticalStrikeCheck((playerStats.baseDamage * tempDamageMod) * (distanceShot / 100));


                                damageText.text = ((int)criticalStrikeCheck((playerStats.baseDamage * tempDamageMod) * (distanceShot / 100))).ToString();

                                if (enemyScript.damageTaken > 0)
                                {
                                    Instantiate(damageText, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), Quaternion.identity);
                                }
                            }


                            //for spawners ---- Tristin's Add
                            if (hit.transform.gameObject.GetComponent<EnemySpawnEngine>() && hit.distance < distanceZone5)
                            {
                                EnemySpawnEngine spawnScript = hit.transform.gameObject.GetComponent<EnemySpawnEngine>();

                                spawnScript.damageTaken += (int)criticalStrikeCheck((playerStats.baseDamage * tempDamageMod) * (distanceShot / 100));

                                damageText.text = ((int)criticalStrikeCheck((playerStats.baseDamage * tempDamageMod) * (distanceShot / 100))).ToString();

                                if (spawnScript.damageTaken < 0)
                                {
                                    Instantiate(damageText, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), Quaternion.identity);

                                }
                            }
                            //Tristin's Add 4/27 ---- Bubble Shields
                            if (hit.transform.gameObject.GetComponent<BubbleShield>() && hit.distance < distanceZone5)
                            {
                                BubbleShield shieldScript = hit.transform.gameObject.GetComponent<BubbleShield>();
                                Debug.Log("Consecutive hit on Shield");
                                shieldScript.damageTaken += (int)criticalStrikeCheck((playerStats.baseDamage * tempDamageMod) * (distanceShot / 100));
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

                        //Waits before firing next attack
                        yield return new WaitForSeconds(attackSpeed);
                    }



                }


                //if i is even
                if (i % 2 == 0 )
                {
                    //play right flash
                    muzzleFlash.Play();

                    Instantiate(bulletCasing, muzzleFlash.transform.position, Quaternion.Euler(90, playerGameObject.transform.rotation.eulerAngles.y, 0));

                    //Instantiate(bulletObject, muzzleFlash.transform.position, Quaternion.Euler(90, 90, 90));


                }
                else
                {
                    //play left muzzle flash
                    muzzleFlash2.Play();

                    Instantiate(bulletCasing, muzzleFlash2.transform.position, Quaternion.Euler(90, playerGameObject.transform.rotation.eulerAngles.y, 0));

                    //Instantiate(bulletObject, muzzleFlash2.transform.position, Quaternion.Euler(90, 90, 90));
                }


                //create ray from camera to cente rof the screen
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1000f))
                {
                    GetComponentInParent<PlayerStats>().isCombat = true;
                    StartCoroutine(GetComponentInParent<PlayerStats>().CombatCheck());

                    if (hit.distance <= distanceZone1)
                    {
                        distanceShot = 100;
                    }
                    if(hit.distance > distanceZone1 && hit.distance <= distanceZone2)
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

                    float tempDamageMod;
                    if (emerge)
                        tempDamageMod = specialDamageMod;
                    else
                        tempDamageMod = damageMultiplier;

                    if (hit.transform.gameObject.GetComponent<EnemyDamage>() && hit.distance < distanceZone5)
                    {
                       

                        EnemyDamage enemyScript = hit.transform.gameObject.GetComponent<EnemyDamage>();


                        if (playerGameObject.GetComponent<Augment>().spikedExplosion == true)
                        {
                            int rand = Random.Range(0, 10);
                            if (rand == 0)
                            {

                                if (hit.transform.gameObject.GetComponent<EnemyDamage>())
                                {

                                    if (playerGameObject.GetComponent<Augment>().canLifeDrain)
                                    {
                                        playerGameObject.GetComponent<Augment>().LifeDrain();
                                    }

                                    Instantiate(spikeExplosion, hit.transform.position, Quaternion.identity);

                                    enemyScript = hit.transform.gameObject.GetComponent<EnemyDamage>();

                                    enemyScript.damageTaken += (int)criticalStrikeCheck((playerStats.baseDamage * damageMultiplier) * (distanceShot / 100));
                                    damageText.text = ((int)enemyScript.damageTaken).ToString();

                                    if (enemyScript.damageTaken > 0)
                                    {
                                        Instantiate(damageText, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), Quaternion.identity);

                                    }
                                }

                            }

                        }


                        //enemyScript.damageTaken += (int)(((playerStats.baseDamage * damageMultiplier)) * (distanceShot/100));
                        enemyScript.damageTaken += (int)criticalStrikeCheck((playerStats.baseDamage * tempDamageMod) * (distanceShot / 100));
                       
                      
                        damageText.text = ((int)criticalStrikeCheck((playerStats.baseDamage * tempDamageMod) * (distanceShot / 100))).ToString();

                        if(enemyScript.damageTaken > 0)
                        {
                            Instantiate(damageText, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), Quaternion.identity);
                        }
                    }
                    //Tristin's Add 4/27 ---- Bubble Shields
                    if (hit.transform.gameObject.GetComponent<BubbleShield>() && hit.distance < distanceZone5)
                    {
                        BubbleShield shieldScript = hit.transform.gameObject.GetComponent<BubbleShield>();
                        Debug.Log("Consecutive hit on Shield");
                        shieldScript.damageTaken += (int)criticalStrikeCheck((playerStats.baseDamage * tempDamageMod) * (distanceShot / 100));
                    }

                    //for spawners ---- Tristin's Add
                    if (hit.transform.gameObject.GetComponent<EnemySpawnEngine>() && hit.distance < distanceZone5)
                    {
                        EnemySpawnEngine spawnScript = hit.transform.gameObject.GetComponent<EnemySpawnEngine>();

                        spawnScript.damageTaken += (int)criticalStrikeCheck((playerStats.baseDamage * tempDamageMod) * (distanceShot / 100));

                        damageText.text = ((int)criticalStrikeCheck((playerStats.baseDamage * tempDamageMod) * (distanceShot / 100))).ToString();

                        if(spawnScript.damageTaken > 0)
                        {
                            Instantiate(damageText, new Vector3(hit.transform.position.x, hit.transform.position.y + 2, hit.transform.position.z), Quaternion.identity);

                        }
                    }
                    //for Hunter Commando Boss -- Tristin's Add 3/24
                    if (hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>() && hit.distance < distanceZone5)
                    {
                        Debug.Log("Hit Gen");
                        if (hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>().stunReady && !hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>().parent.GetComponent<BossAction>().stealthed)
                        {
                            hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>().GenStun();
                        }
                        else if(!hit.transform.gameObject.GetComponent<HunterCommandoStealthGen>().parent.GetComponent<BossAction>().stealthed)
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
                        Debug.Log("Hit Body");
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

                //Waits before firing next attack
                yield return new WaitForSeconds(attackSpeed);
            }
            mpRig.ResetTrigger("Shoot");
            emerge = false;

        }
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
    IEnumerator waitForCoolDown()
    {

        //Waits to reload
        yield return new WaitForSeconds(cooldown);

        //can start next 4 round burst
        canUse = true;

    }
}
