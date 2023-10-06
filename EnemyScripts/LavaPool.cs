using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPool : MonoBehaviour
{
    private GameObject playerGameObject;
    public float lavaDamage;
    public float lavalPoolDuration;
    public bool canLava = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision other)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Player")
       // {
           // print("hitplayer");
           // playerGameObject = other.gameObject;
           // StartCoroutine(LavaDamage());
       // }
       
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && canLava)
        {

            print("hitplayer");
            playerGameObject = other.gameObject;
            if(canLava)
            {
                canLava = false;
                StartCoroutine(LavaDamage());

            }

        }
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

            canLava = true;
        }


    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lavalPoolDuration);

        Destroy(this.gameObject);
    }
}
