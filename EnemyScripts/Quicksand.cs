using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quicksand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnCollisionStay(Collision other)
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        }

        if(other.gameObject.tag == "PlayerHead")
        {
            PlayerStats playerStats = other.gameObject.GetComponentInParent<PlayerStats>();
            playerStats.currentPlayerHealth = 0;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(0, 90, 0);
        }


        if (other.gameObject.tag == "PlayerHead")
        {
            PlayerStats playerStats = other.gameObject.GetComponentInParent<PlayerStats>();
            playerStats.currentPlayerHealth = 0;
        }
    }
}
