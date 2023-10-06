using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldUI : MonoBehaviour
{
    public Image shieldBar;
    public PlayerStats playerStats;
    void Start()
    {
        shieldBar.fillAmount = 0;
        if (playerStats.shielded)
        {
            shieldBar.fillAmount = playerStats.shieldHP / playerStats.maxShieldHP;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.shielded)
        {
            shieldBar.fillAmount = playerStats.shieldHP / playerStats.maxShieldHP;
        }

    }
}
