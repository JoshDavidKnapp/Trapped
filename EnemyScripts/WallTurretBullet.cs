using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTurretBullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDamage;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(bulletDamage);
        }
        Destroy(this.gameObject);
    }
}
