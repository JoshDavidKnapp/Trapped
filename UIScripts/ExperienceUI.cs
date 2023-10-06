using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ExperienceUI : MonoBehaviour
{

    //Player reference
    private GameObject playerGameObject;
    //Hp bar image reference
    private Image expImage;

    private TextMeshProUGUI levelText;

    private void Awake()
    {
        //Get exp bar image
        expImage = GetComponent<Image>();
        levelText = GetComponentInChildren<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Set player
       // playerGameObject = GameObject.FindGameObjectWithTag("Player");
       // PlayerStats playerStats = playerGameObject.GetComponent<PlayerStats>();

      //  expImage.fillAmount = (float)(playerStats.currentPlayerExp / playerStats.maxPlayerExp);
       // levelText.text = "Lv. " + playerStats.playerLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateExpUI();
    }

    public void UpdateExpUI()
    {
        //Set player
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        PlayerStats playerStats = playerGameObject.GetComponent<PlayerStats>();

        levelText.text = "Lv. " + playerStats.playerLevel.ToString();

        float expFloat = (playerStats.currentPlayerExp / playerStats.maxPlayerExp);

        //exp bar move smoothly
        if (expImage.fillAmount != expFloat)
        {
            expImage.fillAmount = Mathf.Lerp(expImage.fillAmount, expFloat, Time.deltaTime * 2);
        }
    }

    public void XpSetUp(GameObject currentPlayer)
    {
        //Set player
        playerGameObject = currentPlayer;
        PlayerStats playerStats = currentPlayer.GetComponent<PlayerStats>();

        expImage.fillAmount = (float)(playerStats.currentPlayerExp / playerStats.maxPlayerExp);
        levelText.text = "Lv. " + playerStats.playerLevel.ToString();

    }

   
}
