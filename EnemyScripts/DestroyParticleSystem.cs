using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DestroyParticleSystem : MonoBehaviour
{
    public float destroyTime;

    EnemyMovement enemyTakeDamage;
    private PlayerStats playerStats;
    public float chestRocketMultiplyer;

    public TextMeshPro damageText;

    private void Awake()
    {
        //get playerstats
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(DestroyThis());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {

        

        

        Destroy(this.gameObject);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //EnemyDamage enemyMovement = other.gameObject.GetComponent<EnemyDamage>();
            //enemyMovement.damageTaken -= (int)(playerStats.GetComponent<PlayerStats>().baseDamage * chestRocketMultiplyer);

            

           //Debug.Log("hit");
        }
    }

    public IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }
}
