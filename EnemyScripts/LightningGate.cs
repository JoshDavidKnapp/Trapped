using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningGate : MonoBehaviour
{
    private GameObject playerGameObject;

    public float lightningDamage;

    public float stunDuration;

    public ParticleSystem lightning;

    public BoxCollider collider;

    public float lightningDuration;
    // Start is called before the first frame update
    void Start()
    {
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerGameObject = other.gameObject;
            StartCoroutine(Stun());
        }
    }

    public IEnumerator Stun()
    {
        playerGameObject.GetComponent<PlayerLocomotion>().canMove = false;

        lightningDamage = 50 + GameObject.Find("EnemyMaster").GetComponent<EnemyMaster>().enemyLevel;

        playerGameObject.GetComponent<PlayerStats>().TakeDamage(lightningDamage);

        yield return new WaitForSeconds(stunDuration);

        playerGameObject.GetComponent<PlayerLocomotion>().canMove = true;

    }

    public void StrikeIt()
    {
        StartCoroutine(Strike());
    }

    public IEnumerator Strike()
    {
        print("starting strike");
        lightning.Play();
        collider.enabled = true;

        yield return new WaitForSeconds(lightningDuration);

        lightning.Stop();
        collider.enabled = false;

    }
}
