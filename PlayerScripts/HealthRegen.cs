using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    //playerstats reference
    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        //get playerstats
        playerStats = GetComponent<PlayerStats>();
        //start hp regen
        StartCoroutine(HPRegen());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator HPRegen()
    {
        //if player is not at full hp
        if(playerStats.currentPlayerHealth < playerStats.maxPlayerHealth)
        {
            //set health regen amount
            float healthRegen = playerStats.healthRegen + playerStats.healthRegenPerLevel;
            
            //regen health
            if (playerStats.boostedRegen)
                playerStats.currentPlayerHealth += healthRegen * playerStats.regenBoost;
           
            else
                playerStats.currentPlayerHealth += healthRegen;

            //wait 1 second
            yield return new WaitForSeconds(1);
            //start hp regen again
            StartCoroutine(HPRegen());
        }
        else
        {
            if (playerStats.currentPlayerHealth > playerStats.maxPlayerHealth)
            {
                playerStats.currentPlayerHealth = playerStats.maxPlayerHealth;
            }
                //wait fro 1 second
                yield return new WaitForSeconds(1);
            //start hp regen
            StartCoroutine(HPRegen());
        }
        

    }
}
