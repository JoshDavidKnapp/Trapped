using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rocket : MonoBehaviour
{
    GameObject playerGameObject;
    public GameObject spikeExplosion;
    //Rigidbody reference
    Rigidbody projectileRB;

    //Speed of the bullet
    public float bulletSpeed;

    //Camera reference
    private Camera mainCamera;

    //particle system reference
    public ParticleSystem explosion;

    //playerstats reference
    private PlayerStats playerStats;

    //enemy health script reference
    EnemyDamage enemyTakeDamage;
    //enemy spawner health reference
    EnemySpawnEngine spawnerTakeDamage;

    public float damageMultiplier;

    //text pop for spawners
    public TextMeshPro damageText;

    //explosion audio
    public AudioClip rocketExplosion;


    //Called before Start
    private void Awake()
    {
        //get playerstats
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        //Get projectile rigidbody
        projectileRB = GetComponent<Rigidbody>();
        //Get camera
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        //Create a vector3 in front of the camera
        Vector3 aimSpot = mainCamera.transform.position;
        aimSpot += mainCamera.transform.forward * 50f;

        //Projectile is facing the aim spot
        transform.LookAt(aimSpot);

        //Shoot projectile forward (towards the aim spot)
        projectileRB.AddForce(this.gameObject.transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        //instead disable objects to allow audio to play
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        //gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<TrailRenderer>().forceRenderingOff = true;
        

        //play audio clip
        AudioSource.PlayClipAtPoint(rocketExplosion, gameObject.transform.position, 0.75f);

        //spawn explosion
        Instantiate(explosion, this.gameObject.transform.position, Quaternion.identity);

        //if enemy hit
        if (other.gameObject.tag == "Enemy")
        {
            //find hp script
            enemyTakeDamage = other.gameObject.GetComponent<EnemyDamage>();
            //reduce hp
            enemyTakeDamage.damageTaken += (int)(playerStats.baseDamage * damageMultiplier);

            damageText.text = ((int)(playerStats.baseDamage * damageMultiplier)).ToString();

            Instantiate(damageText, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 2, other.gameObject.transform.position.z), Quaternion.identity);

            if (playerGameObject.GetComponent<Augment>().spikedExplosion == true)
            {
                int rand = Random.Range(0, 2);
                if (rand == 0)
                {

                    if (other.transform.gameObject.GetComponent<EnemyDamage>())
                    {
                        if (playerGameObject.GetComponent<Augment>().canLifeDrain)
                        {
                            playerGameObject.GetComponent<Augment>().LifeDrain();
                        }
                        Instantiate(spikeExplosion, other.transform.position, Quaternion.identity);

                        EnemyDamage enemyScript = other.transform.gameObject.GetComponent<EnemyDamage>();

                        //enemyScript.damageTaken += (int)(((playerStats.baseDamage * damageMultiplier)) * (distanceShot / 100));

                        damageText.text = ((int)enemyScript.damageTaken).ToString();

                        if (enemyScript.damageTaken > 0)
                        {
                            Instantiate(damageText, new Vector3(other.transform.position.x, other.transform.position.y + 2, other.transform.position.z), Quaternion.identity);

                        }
                    }

                }

            }

        }
        //if bubbleShield hit
        if (other.gameObject.tag == "BubbleShield")
        {
            BubbleShield shieldScript = other.transform.gameObject.GetComponent<BubbleShield>();

            shieldScript.damageTaken += (int)(playerStats.baseDamage * damageMultiplier);
        }
        //if enemyspawner hit ---- Tristin's Add
        if (other.gameObject.tag == "EnemySpawner")
        {
            //find hp script
            spawnerTakeDamage = other.gameObject.GetComponent<EnemySpawnEngine>();
            //reduce hp
            spawnerTakeDamage.damageTaken = (int)(playerStats.baseDamage * damageMultiplier);

            damageText.text = ((int)(playerStats.baseDamage * damageMultiplier)).ToString();

            Instantiate(damageText, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 2, other.gameObject.transform.position.z), Quaternion.identity);
        }
        //if boss hit ---- Tristin's Add 3/25
        if (other.gameObject.tag == "CommandoGen")
        {
            if (other.gameObject.GetComponent<HunterCommandoStealthGen>().stunReady && !other.gameObject.GetComponent<HunterCommandoStealthGen>().parent.GetComponent<BossAction>().stealthed)
            {
                other.gameObject.GetComponent<HunterCommandoStealthGen>().GenStun();
            }
            else
            {
                //reference damage script
                BossDamage damageScript = other.gameObject.GetComponent<HunterCommandoStealthGen>().parent.GetComponent<BossDamage>();
                //apply damage
                damageScript.damageTaken += (int)(playerStats.baseDamage * damageMultiplier);
                //set damage text
                damageText.text = ((int)(playerStats.baseDamage * damageMultiplier)).ToString();
                //instantiate pop damage text
                Instantiate(damageText, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 2, other.gameObject.transform.position.z), Quaternion.identity);
            }
        }
        else if (other.gameObject.tag == "Boss")
        {
            //reference damage script
            BossDamage damageScript = other.gameObject.GetComponent<BossDamage>();
            //apply damage
            damageScript.damageTaken += (int)(playerStats.baseDamage * damageMultiplier);
            //set damage text
            damageText.text = ((int)(playerStats.baseDamage * damageMultiplier)).ToString();
            //instantiate pop damage text
            Instantiate(damageText, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 2, other.gameObject.transform.position.z), Quaternion.identity);
        }


        //destroy  this
        //Destroy(this.gameObject);
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }


}
