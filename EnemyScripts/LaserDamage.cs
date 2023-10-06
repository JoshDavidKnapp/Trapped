using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    public float laserDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("hit by laser");
            other.gameObject.GetComponent<PlayerStats>().currentPlayerHealth -= laserDamage;
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("hit by laser");
            other.gameObject.GetComponent<PlayerStats>().currentPlayerHealth -= laserDamage;
            Destroy(this.gameObject);
        }
    }
}
