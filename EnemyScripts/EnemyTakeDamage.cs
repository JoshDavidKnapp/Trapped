using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyTakeDamage : MonoBehaviour
{
    //health variable
    public float health = 100;
    public float expValue = 10;
    //if they can take damage form gas
    public bool canBeGassed = true;
    public bool canTakeSpikeDamage = true;
    //player reference
    private GameObject playerGameObject;

    public TextMeshPro damageText;

    private PlayerStats playerStats;
    private void Awake()
    {
        //set player
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerStats = playerGameObject.GetComponent<PlayerStats>();

        //this.gameObject.transform.childCount
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            EnemyDeath();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if enemy stays in gas
        if(other.gameObject.tag == "Gas")
        {
            //start coroutine
            StartCoroutine(PoisonGas());
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        //if hit by a spike
        if (other.gameObject.tag == "Spike")
        {
           // StartCoroutine(SpikeDamage(0));
            GetChildren();
        }
    }

    public int GetChildren()
    {
        //get number of spikes
        int spikes = 0;

        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            spikes++;

            if (spikes >= 5)
            {
                spikes = 5;
            }

            Debug.Log(spikes);
        }

       // StartCoroutine(SpikeDamage(spikes));

        return spikes;
    }



    public IEnumerator PoisonGas()
    {
        //if enemy can be gassed
        if(canBeGassed)
        {
            //cant be gassed
            canBeGassed = false;
            //get gas trail script
            GasTrail gasTrail = playerGameObject.GetComponent<GasTrail>();
            //enemy health decrease 
            health -= gasTrail.gasDamage;

            damageText.text = ((int)(playerStats.baseDamage * gasTrail.gasDamageMultiplyer)).ToString();

            Instantiate(damageText, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);

            //wait for damage tick
            yield return new WaitForSeconds(gasTrail.gasDamageTick);
            //can be gassed again
            canBeGassed = true;
        }

       
    }

   // public IEnumerator SpikeDamage(int spikes)
    //{
        //if enemy can take spike damage
       // if(canTakeSpikeDamage)
       /// {
            //cant take damage
            //canTakeSpikeDamage = false;

            //get how many spikes are in enemy
          //  spikes = GetChildren();

            //sets projectile script
           // Projectile projectile = playerGameObject.GetComponentInChildren<Projectile>();
            
            //subtract hp depending on # of spikes
           // health -= spikes * projectile.spikeDamage;

         //   damageText.text = ((int)(playerStats.baseDamage * projectile.spikeDamage)).ToString();

          //  Instantiate(damageText, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);

            //wait 
         //   yield return new WaitForSeconds(projectile.spikeDamageTick);

            //can take damage again
          //  canTakeSpikeDamage = true;

            //check for more spikes
           // StartCoroutine(SpikeDamage(spikes));

       // }

 //   }

    public void EnemyDeath()
    {
       // PlayerStats playerStats = playerGameObject.GetComponent<PlayerStats>();
      //  playerStats.currentPlayerExp += expValue;
       // Destroy(this.gameObject);
    }
}
