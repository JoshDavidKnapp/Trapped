using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    public GameObject playerGameObject;
    public bool isInRange = false;
    public bool canShoot = true;
    public float fireRate;
    public float bulletSpeed;
    public Transform firePoint;
    public bool canMove = true;
    public ParticleSystem laserShot;
    public Vector3 prevPoint;
    public GameObject laserShotObject;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Init());
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGameObject != null && isInRange)
        {
            if(!canMove)
            {
                transform.LookAt(prevPoint);
               
            }
            else if(canMove)
            {
                transform.LookAt(playerGameObject.transform);

            }

        }
    }

    public IEnumerator Prev()
    {
        prevPoint = playerGameObject.transform.position;

        yield return new WaitForSeconds(1f);

        StartCoroutine(Prev());
    }

    public IEnumerator Init()
    {
        if (playerGameObject == null)
        {
            yield return new WaitForSeconds(4);
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            canShoot = true;
            StartCoroutine(Prev());

        }

    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = true;
            StartCoroutine(LaserCharge());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = false;
            StopCoroutine(LaserCharge());
        }
    }
    public IEnumerator LaserCharge()
    {

        yield return new WaitForSeconds(1);
        laserShot.Play();

        if (isInRange)
        {
            
            canMove = false;
            //Instantiate(laserShotObject, firePoint.transform.position, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(1f);
            canMove = true;

        }
        if(isInRange)
        {
            StartCoroutine(LaserCharge());

        }

    }


}
