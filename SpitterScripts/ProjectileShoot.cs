using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileShoot : MonoBehaviour
{
    public GameObject spikeExplosion;

    //Rigidbody reference
    Rigidbody projectileRB;

    //Speed of the bullet
    public float bulletSpeed;

    //Camera reference
    private Camera mainCamera;

    public TextMeshPro damageText;

    GameObject playerGameObject;

    //Called before Start
    private void Awake()
    {
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


        if (playerGameObject.GetComponent<Augment>().spikedExplosion == true)
        {
            int rand = Random.Range(0, 10);
            if (rand == 0)
            {

                if (other.transform.gameObject.GetComponent<EnemyDamage>())
                {
                    if(playerGameObject.GetComponent<Augment>().canLifeDrain)
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

        //      gameObject.GetComponent<Transform>().SetParent(other.collider.gameObject.transform);
        //      projectileRB.isKinematic = true;
        //      projectileRB.velocity = new Vector3(0, 0, 0);
        //      SphereCollider collider = this.gameObject.GetComponent<SphereCollider>();
        //      collider.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

   
}
