using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSpout : MonoBehaviour
{
    public float forwardSpeed;
    public float upSpeed;
    public float lavaDamage;
    public GameObject lavaPool;
    private GameObject playerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
                
        rb.AddForce(transform.forward * forwardSpeed, ForceMode.Impulse);
        rb.AddForce(transform.up * upSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("hitplayer");
            playerGameObject = other.gameObject;
            StartCoroutine(LavaDamage());
        }
        if (other.gameObject.tag != "Player")
        {
            Instantiate(lavaPool, transform.position, Quaternion.Euler(90,0,0));
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        
    }

    public IEnumerator LavaDamage()
    {
        for (int i = 0; i < 4; i++)
        {
            if (playerGameObject.GetComponent<PlayerStats>().canTakeDamage)
            {
                playerGameObject.GetComponent<PlayerStats>().TakeDamage(lavaDamage);

            }
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);


    }
}
