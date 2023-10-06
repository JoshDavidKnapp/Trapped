using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Audio;
public class PlayerStats : MonoBehaviour
{
    [Header("Level Up Feedback")]
    public AudioSource levelUp;
    public ParticleSystem level_1;
    public ParticleSystem level_2;

    [Header("HUD")]
    //player info
    public HudDisplay hud;

    //portal refrence
    public PortalController portal;

    [Header("Health")]
    //HealthUI script
    public HealthUI healthUI;

    [Header("Shield")]
    //shield UI
    public ShieldUI shieldUI;

    //time it takes to recharge shield
    public float shieldRecharge = 7f,shieldRechargeCounter=0;

    //shield HP
    public float maxShieldHP,shieldHP;

    //percent amount of health
    public float shieldPercent;

    //if the player has a shield
    public bool shielded;

    //if the shield has been damaged recently
    private bool shieldDamaged;

    //Health of the player
    public float currentPlayerHealth;
    //Max player health
    public float maxPlayerHealth;
    //max hp increased every level
    public float maxHealthIncreasePerLevel;

    public float healthRegen;
    public float healthRegenPerLevel;

    //damage resistance
    public float damageResist = 0;

    [Header("Experience")]
    //Player level
    public int playerLevel;
    //Current player EXP
    public float currentPlayerExp;
    //max player Exp
    public float maxPlayerExp;
    //exp regen/sec
    public float expRegen;

    [Header("Damage")]
    //base damage of player
    public float baseDamage;
    [System.NonSerialized]
    public float baseDamageMOD;
    //base damage increased per level
    public float baseDamageIncreasePerLevel;

    [Header("Bool")]
    //if player is able to take damage
    public bool canTakeDamage = true;

    //checks if this is for selector data
    public bool isSelectorData;

    public bool allCellsCollected;

    [Header("Fuel Cells Collected")]
    public float collectedCells;

    [Header("Temp")]
    //Temp variable for gaining exp
    public float orbEXP = 5;
    //Temp variable for taking damage
    public float boxDamage = 5;

    //for rouge bot special
    public bool speedyRecovery = false,boostedRegen;
    private bool recovering;
    private float recoveryTime = 9, currentRecoveryTime = 0;
    public float regenBoost = 3;

    //Player reference
    private GameObject playerGameObject;

    public GameObject cameraController;
    public CinemachineVirtualCamera introCam;

    public bool isCombat;

    private void Update()
    {
        //handles shield recharching after taking damage
        ShieldWaitAndRecharge();
        Recovery();

        if (currentPlayerHealth == 0)
        {
            currentPlayerHealth = 0;
            healthRegen = 0;
        }
    }

    private void Awake()
    {

        //keeps the gameobject from persiting if on the character select menu
        if (!isSelectorData)
        {
            DontDestroyOnLoad(gameObject);
            //sets player
            playerGameObject = this.gameObject;

            allCellsCollected = false;



           // GetComponent<MeshRenderer>().enabled = false;

            StartCoroutine(WaitForStart());
        }
        //Saves Data - Trevor
        SaveData();

    }
       
    private void OnCollisionEnter(Collision other)
    {
        //if colliding with a box
        if(other.gameObject.tag == "Box")
        {
            //take damage
            TakeDamage(boxDamage);

        }

    }

    public void GainEXP(float expGained)
    {
        //If the player isnt at max exp
        if(currentPlayerExp < maxPlayerExp)
        {
            //increase exp
            currentPlayerExp += expGained;
        }
       
        //if player is at max exp
        if(currentPlayerExp >= maxPlayerExp)
        {
            levelUp.Play();
            level_1.Play();
            level_2.Play();
            //increase level
            playerLevel++;
            //carry over extra exp gained
            currentPlayerExp = currentPlayerExp % maxPlayerExp;
            //increase exp needed to reach next level
            maxPlayerExp = (int)( Mathf.Pow(playerLevel,2)/1.5f +playerLevel/.15f + 50);
            //increase max health
            maxPlayerHealth += maxHealthIncreasePerLevel;
            //add new hp to player
            currentPlayerHealth+= maxHealthIncreasePerLevel;
            //increase hp regen
            healthRegen += healthRegenPerLevel;
            //increase damage
            baseDamage += baseDamageIncreasePerLevel;
            //Saves Data - Trevor
            SaveData();
            //update shield percent
            if (shielded)
            {
                shieldHP = maxShieldHP = shieldPercent * maxPlayerHealth;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        isCombat = true;
        StartCoroutine(CombatCheck());
       
        //if the player can tage damage
        if(canTakeDamage && currentPlayerHealth > 0)
        {
            if(GetComponent<Augment>().canMagneticShield)
            {
                GetComponent<Augment>().MagneticShielding();
            }
            if(GetComponent<Augment>().isCriticalResponse && GetComponent<Augment>().canCriticalResponse)
            {
                StartCoroutine(GetComponent<Augment>().CriticalResponse());
            }

            recovering = false;
            if (shielded && shieldHP > 0)
            {
                //shield takes damage with shield reduction 10%
                shieldHP -= (damage - .1f * damage);
                //shows sheild has been damaged recently to start recharge in update
                shieldDamaged = true;
                if (shieldHP < 0)
                {
                    //shield hp overflow
                    //currentPlayerHealth += shieldHP;
                    shieldHP = 0;
                }

            }
            else
            //take damage
            if(GetComponent<Augment>().magneticShielding == true)
            {
                damage = 0;
            }
                currentPlayerHealth -= (damage-damageResist * damage) * (GetComponent<Augment>().damageMOD);
        }

        
    }

    public IEnumerator CombatCheck()
    {
        yield return new WaitForSeconds(5);
        isCombat = false;
    }

    //called when fuel cell is picked up to see if max fuel cells has been reached
    public void fuelCheck()
    {
       
        collectedCells++;
        hud.FuelCellDisplay((int)collectedCells);
        if (collectedCells >= 4)
        {
            allCellsCollected = true;
            hud.PortalDisplaySwitch();
            portal.switchOnPortal();
        }


    }

    //TEST function to text EXP gain
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EXP")
        {
            Destroy(other.gameObject);
            GainEXP(orbEXP);
        }
    }

    public IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(3.5f);
        //GetComponent<MeshRenderer>().enabled = true;
    }



    //handles shield recharge as well as wait between recharge
    public void ShieldWaitAndRecharge()
    {
        //if the shield has taken damage
        if (shieldDamaged)
        {
            shieldDamaged = false;
            shieldRechargeCounter = 0;
        }
        //start waiting for recharge time
        if (!shieldDamaged)
        {
            
            shieldRechargeCounter += Time.deltaTime;
            //start recharchging shield 
            if (shieldRechargeCounter >= shieldRecharge)
            {
                if (shieldHP < maxShieldHP)
                {
                    shieldHP += Time.deltaTime * 10;
                }
                if (shieldHP > maxShieldHP)
                {
                    shieldHP = maxShieldHP;
                }

            }

        }
    }
   public void Recovery()
    {
        if (speedyRecovery)
        {
            if (!recovering)
            {
                recovering = true;
                currentRecoveryTime = 0;
                boostedRegen = false;

            }
            else
            {
                currentRecoveryTime += Time.deltaTime;
                if (recovering && currentRecoveryTime >= recoveryTime)
                {
                    boostedRegen = true;
                }
            }

        }
    }

    //Save Function - Trevor
    public void SaveData()
    {
        SaveSystem.SavePlayerStats(this);
    }
}

