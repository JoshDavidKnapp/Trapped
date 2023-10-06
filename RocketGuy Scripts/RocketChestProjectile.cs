using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RocketChestProjectile : MonoBehaviour
{
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
    EnemyMovement enemyTakeDamage;

    public float chestRocketMultiplyer;

    //chest explosion sound
    public AudioClip chestExplosion;



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
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<TrailRenderer>().forceRenderingOff = true;

        //play audio clip
        AudioSource.PlayClipAtPoint(chestExplosion, gameObject.transform.position, 0.75f);

        //spawn explosion
        Instantiate(explosion, this.gameObject.transform.position, Quaternion.identity);

        //if enemy hit
        if (other.gameObject.tag == "Enemy")
        {
            //find hp script
            //enemyTakeDamage = other.gameObject.GetComponent<EnemyMovement>();
            //reduce hp
            //enemyTakeDamage.health -= playerStats.baseDamage * chestRocketMultiplyer;

          //  Debug.Log("HIT an enemy");
            

        }
        //destroy  this
        //Destroy(this.gameObject);
        Destroy(gameObject, 5f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            

            //Debug.Log("HIT an enemy trigger");


        }
    }
}
