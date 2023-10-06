using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    //Player reference
    private GameObject playerGameObject;
    //Hp bar image reference
    private Image hpImage;
    //hp text reference
    private TextMeshProUGUI healthText;

    private void Awake()
    {
        //Get Health text
        healthText = GetComponentInChildren<TextMeshProUGUI>();
        //Get Hp bar image
        hpImage = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set player
        //playerGameObject = GameObject.FindGameObjectWithTag("Player");
        //PlayerStats playerStats = playerGameObject.GetComponent<PlayerStats>();

        //Set HP
       // hpImage.fillAmount = (float)(playerStats.currentPlayerHealth / playerStats.maxPlayerHealth);
       // healthText.text = (playerStats.currentPlayerHealth + "/" + playerStats.maxPlayerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
    }
    


    public void UpdateHealthUI()
    {
        //Get player
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        PlayerStats playerStats = playerGameObject.GetComponent<PlayerStats>();

        //Update health text
        healthText.text = ((int)playerStats.currentPlayerHealth + "/" + (int)playerStats.maxPlayerHealth);

        //health bar % full
        float hpFloat = (playerStats.currentPlayerHealth / playerStats.maxPlayerHealth);

        //Health bar move smoothly
        if (hpImage.fillAmount != hpFloat)
        {
            hpImage.fillAmount = Mathf.Lerp(hpImage.fillAmount, hpFloat, Time.deltaTime * 2);
        }
                       
        //Change Color of health bar based on hp
        if(hpFloat > .5f)
        {
            hpImage.color = new Color32(0 ,225, 0, 255);
        }
        else if(hpFloat <= .5f && hpFloat > .25f)
        {
            hpImage.color = new Color32(225, 162, 0, 255);

        }
        else if (hpFloat <= .25f)
        {
            hpImage.color = new Color32(225, 0, 0, 255);

        }

    }

    public void HealthBarSetUp(GameObject currentPlayer)
    {
        //Set player
        playerGameObject = currentPlayer;
        PlayerStats playerStats = playerGameObject.GetComponent<PlayerStats>();

        //Set HP
        hpImage.fillAmount = (float)(playerStats.currentPlayerHealth / playerStats.maxPlayerHealth);
        healthText.text = (playerStats.currentPlayerHealth + "/" + playerStats.maxPlayerHealth);

    }


}
