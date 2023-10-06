using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Augment : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    private GameObject enemyMaster;
    private float tempVariable;
    private float tempDamage1;

    public float damageMOD;

    [Header("Rare Augments")]
    public bool powerOscillation;
    public bool damageLearning;
    public bool powerShielding;

    [Header("Uncommon Augments")]
    public bool doubleJump;
    public bool spikedExplosion;
    public bool isCriticalResponse;
    public bool canCriticalResponse;
    public bool weaponRepeat;
    public bool canStartWeaaponRepeat;

    [Header("Common Augments")]
    public bool magneticShielding;
    public bool canMagneticShield;
    public bool energyConservation;
    public bool weaponOvercharge;
    public bool regenOvercharge;
    public bool weaponDisplay;
    public bool safeRecovery;
    public bool lifeDrain;
    public bool canLifeDrain;


    private float tempRegen;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();

               
        enemyMaster = GameObject.Find("EnemyMaster");

        damageMOD = 1;

        tempVariable = playerStats.baseDamage;


        //SpikedExplosion();

        //WeaponRepeater();

        //DoubleJump();

        //StartCoroutine(PowerOscillation());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PowerOscillation()
    {
        powerOscillation = true;

        playerStats.baseDamage = tempVariable;
        playerStats.baseDamageMOD = playerStats.baseDamage;
        playerStats.baseDamage = playerStats.baseDamageMOD * 1.3f;

        damageMOD = 1;

        yield return new WaitForSeconds(5);

        damageMOD = .8f;

        playerStats.baseDamage = tempVariable;
        playerStats.baseDamageMOD = playerStats.baseDamage;
        playerStats.baseDamage = playerStats.baseDamageMOD * 1f;

        yield return new WaitForSeconds(5);
        StartCoroutine(PowerOscillation());
    }

    public void DamageLearning()
    {
        damageLearning = true;

        playerStats.baseDamage = playerStats.baseDamage + playerStats.baseDamage * .01f;
        damageMOD = damageMOD + enemyMaster.GetComponent<EnemyMaster>().totalKills * .02f;

        print("base damage = " + playerStats.baseDamage);

    }

    public void PowerShielding()
    {
        playerStats.maxShieldHP = playerStats.maxPlayerHealth * 2;
        playerStats.shieldRecharge = 7;
        playerStats.shieldPercent = 100;
        playerStats.shielded = true;
    }

    public void DoubleJump()
    {
        doubleJump = true;
        playerStats.gameObject.GetComponent<PlayerLocomotion>().canDoubleJump = true;
        playerStats.gameObject.GetComponent<PlayerLocomotion>().doubleJumpActivated = true;

    }

    public void SpikedExplosion()
    {
        spikedExplosion = true;
    }

    public IEnumerator CriticalResponse()
    {
        canCriticalResponse = false;

        float tempHealthRegen = GetComponent<PlayerStats>().healthRegen;

        GetComponent<PlayerStats>().healthRegen = GetComponent<PlayerStats>().healthRegen * 3;

        print(GetComponent<PlayerStats>().healthRegen);

        yield return new WaitForSeconds(10);

        GetComponent<PlayerStats>().healthRegen = tempHealthRegen;

        print(GetComponent<PlayerStats>().healthRegen);

        canCriticalResponse = true;

    }

    public void WeaponRepeater()
    {
        canStartWeaaponRepeat = true;

        int repeats = Random.Range(1, 11);
        if(repeats == 1)
        {
            weaponRepeat = true;
            print("WEAPON REPEATS");
        }
        else
        {
            weaponRepeat = false;
            print("WEAPON DOESNT REPEAT");

        }

        print(repeats);
    }

    public void MagneticShielding()
    {
        canMagneticShield = true;
        int blocks = Random.Range(1, 11);
        if(blocks == 1)
        {
            magneticShielding = true;
        }
       

    }

    public void EnergyConvservation()
    {
        energyConservation = true;
        GetComponent<PlayerLocomotion>().movementSpeed += GetComponent<PlayerLocomotion>().movementSpeed * 0.05f;
    }

    public void WeaponOverCharge()
    {
        weaponOvercharge = true;
        GetComponent<PlayerStats>().baseDamage += GetComponent<PlayerStats>().baseDamage * 0.05f;
    }

    public void RegenOvercharge()
    {
        regenOvercharge = true;
        GetComponent<PlayerStats>().healthRegen += GetComponent<PlayerStats>().healthRegen * .05f;
    }

    public void WeaponDisplay()
    {
        weaponDisplay = true;
        if(GetComponent<RaycastShoot>() != null)
        {
            GetComponent<RaycastShoot>().critChance = 10;

        }

        if (GetComponent<ConsecutiveShots>() != null)
        {
            GetComponent<ConsecutiveShots>().critChance = 10;

        }

        if (GetComponent<Projectile>() != null)
        {
            GetComponent<Projectile>().critChance = 10;

        }

        if (GetComponent<RocketAttack>() != null)
        {
            GetComponent<RocketAttack>().critChance = 10;

        }

    }

    public void SafeRecovery()
    {
        safeRecovery = true;
        if(GetComponent<PlayerStats>().isCombat == false)
        {
            tempRegen = GetComponent<PlayerStats>().healthRegen;
            GetComponent<PlayerStats>().healthRegen += GetComponent<PlayerStats>().healthRegen;

        }
        else
        {
            GetComponent<PlayerStats>().healthRegen = tempRegen;
        }

    }

    public void LifeDrain()
    {
        lifeDrain = true;
        GetComponent<PlayerStats>().currentPlayerHealth = GetComponent<PlayerStats>().currentPlayerHealth + 1;
    }
        
        


}
