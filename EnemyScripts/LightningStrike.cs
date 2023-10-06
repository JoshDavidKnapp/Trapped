using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    public float stunDuration;

    private GameObject playerGameObject;

    public CapsuleCollider collider;

    private ParticleSystem lightning;

    public float lightningDurtaion;

    public float lightningCooldown;

    public float lightningDamage;


    // Start is called before the first frame update
    void Start()
    {
        //collider = GetComponent<CapsuleCollider>();
        lightning = GetComponentInParent<ParticleSystem>();
        collider.enabled = false;
        StartCoroutine(Strike());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
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

    public IEnumerator Strike()
    {
        lightning.Play();
        collider.enabled = true;
        //collider.height = 22;

        yield return new WaitForSeconds(lightningDurtaion);

        //collider.height = 1;
        collider.enabled = false;

        yield return new WaitForSeconds(lightningCooldown);
        StartCoroutine(Strike());
    }
}
