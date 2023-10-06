using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasDestroy : MonoBehaviour
{
    //player reference
    private GameObject playerGameObject;

    private void Awake()
    {
        //set player
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        //start coroutine
        StartCoroutine(DestroyThis());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DestroyThis()
    {
        //get gas trail script
        GasTrail gasTrail = playerGameObject.GetComponent<GasTrail>();
        //wait for gas trail duration
        yield return new WaitForSeconds(gasTrail.gasTrailDuration);
        transform.position = new Vector3(-1000, -10000, -1000);
        yield return new WaitForSeconds(0.1f);
        //destroy gas
        Destroy(this.gameObject);
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            print("GAS ATTACK");
            EnemyDamage enemyDamage = other.gameObject.GetComponent<EnemyDamage>();
            enemyDamage.health -= 20;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            print("GAS ATTACK");
            EnemyDamage enemyDamage = other.gameObject.GetComponent<EnemyDamage>();
            enemyDamage.health -= 20;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            print("GAS ATTACK");
            EnemyDamage enemyDamage = other.gameObject.GetComponent<EnemyDamage>();
            enemyDamage.health -= 20;
        }
    }
    */
}
