using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPRegen : MonoBehaviour
{
    //playerstats reference
    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        //get player stats
        playerStats = GetComponent<PlayerStats>();
        //start regen xp
        StartCoroutine(expRegen());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator expRegen()
    {

        
        //increase exp
        playerStats.currentPlayerExp += playerStats.expRegen;
        playerStats.GainEXP(0);
        //wait 1 second
        yield return new WaitForSeconds(1);
        //start coroutine over again
        StartCoroutine(expRegen());
    }
}
