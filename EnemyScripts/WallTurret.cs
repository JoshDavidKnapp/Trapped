using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTurret : MonoBehaviour
{
    public GameObject playerGameObject;
    public bool isInRange = false;
    public bool canShoot = true;
    public GameObject bulletPrefab;
    public float fireRate;
    public float bulletSpeed;
    public Transform firePoint;
       
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Init());
    }

    // Update is called once per frame
    void Update()
    {
        if(playerGameObject != null && isInRange)
        {
            transform.LookAt(playerGameObject.transform);

            if(canShoot)
            {
                print("canshoot");
                StartCoroutine(Shoot());
            }
        }
    }

    public IEnumerator Init()
    {
        if (playerGameObject == null)
        {
            yield return new WaitForSeconds(4);
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            canShoot = true;
        }
    }

    public IEnumerator Shoot()
    {
        canShoot = false;
        Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.Euler(0,0,0));
        bulletPrefab.transform.LookAt(playerGameObject.transform);
        yield return new WaitForSeconds(fireRate);
        canShoot = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = false;
        }
    }
}
