using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    public float vineDamagePerSec;
    public float vineKnockback;

    private GameObject playerGameObject;
    private EnemyMaster enemyMaster;
    public bool canDamage = true;
    // Start is called before the first frame update
    void Start()
    {
        enemyMaster = GameObject.Find("EnemyMaster").GetComponent<EnemyMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && canDamage)
        {
            print("hit player");
            playerGameObject = other.gameObject;
            canDamage = false;
            StartCoroutine(VineDamage());
        }
    }

    public IEnumerator VineDamage()
    {
        for (int i = 0; i < 5; i++)
        {
            playerGameObject.GetComponent<PlayerStats>().TakeDamage(vineDamagePerSec + enemyMaster.enemyLevel);
            yield return new WaitForSeconds(1);
            print(i);
        }

        canDamage = true;
        
    }
}
