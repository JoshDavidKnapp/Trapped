using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharType
{
    inmate001,
    croc,
    SLB,
    MPGuard
}
public class SetCharacterValues : MonoBehaviour
{

    public CharType charType;

    public GameObject player;

    public SceneManage currentManager;

    public PlayerStats currentStats;

    public PlayerLocomotion currentMove;

    private MainCharacterData inmate001;

    private PoisonCharacterData croc;

    private RocketCharacterData SLB;

    private RogueCharacterData MPGuard;


    //croc skill tree special interactions
    public float enemiesSlowed; //if not 0, enemies are slowed by given percent
    public float lashBack;      //if not 0, enemies recieve damage when hitting the player
    public int additionalEnemiesperSpike;   //if not 0, applies spike to nearby enemies
    public float executeThresh; //if not 0, enemies will instantly die when hit by a spike while under given percent of health
    public bool doubleSpike;    //if true, enemies take 2 spikes per spike collision

    //SBL skill tree special interaction
    //public float enemyStunTime; //if not 0, rockets stun enemies for given fraction of a second



    public void PickLoadData()
    {
        switch (currentManager.charType)
        {
            case CharType.inmate001:
                charType = CharType.inmate001;
                inmate001 = SaveSystem.LoadMainCharacter();
                Debug.Log("Loaded");
                break;
            case CharType.croc:
                charType = CharType.croc;
                croc = SaveSystem.LoadPoisonCharacter();
                Debug.Log("Loaded");
                break;
            case CharType.SLB:
                charType = CharType.SLB;
                SLB = SaveSystem.LoadRocketCharacter();
                Debug.Log("Loaded");
                break;
            case CharType.MPGuard:
                MPGuard = SaveSystem.LoadRogueCharacter();
                charType = CharType.MPGuard;
                Debug.Log("Loaded");
                break;
            default:
                break;
        }

    }

    public void AddBuffToCharacter()
    {
       

        switch (charType)
        {
            case CharType.inmate001:
                if (inmate001 == null)
                {
                    Debug.Log("no Data to Load");
                }
                else
                {

                    SlideAbility slide = player.GetComponent<SlideAbility>();
                    RaycastShoot weapon = player.transform.Find("CM vcam1").GetComponentInChildren<RaycastShoot>();
                    switch (inmate001.Skill1)
                    {
                        case 1:
                            currentStats.maxHealthIncreasePerLevel += 2;
                            break;
                        case 2:
                            currentStats.maxHealthIncreasePerLevel += 6;
                            break;
                        case 3:
                            currentStats.maxHealthIncreasePerLevel += 8;
                            break;
                        default:
                            break;
                    }
                    switch (inmate001.Skill2)
                    {
                        case 1:
                            currentMove.movementSpeedModifier = .02f;
                            break;
                        case 2:
                            currentMove.movementSpeedModifier = .04f;
                            break;
                        case 3:
                            currentMove.movementSpeedModifier = .08f;
                            break;
                        default:
                            break;
                    }
                    switch (inmate001.Skill3)
                    {
                        case 1:
                            currentStats.healthRegenPerLevel += .1f;
                            break;
                        case 2:
                            currentStats.healthRegenPerLevel += .2f;
                            break;
                        case 3:
                            currentStats.healthRegenPerLevel += .4f;
                            break;
                        default:
                            break;
                    }
                    switch (inmate001.Skill4)
                    {
                        case 1:
                            slide.slideChargeTotal += 1;
                            break;
                        case 2:
                            slide.slideChargeTotal += 2;
                            break;
                        default:
                            break;
                    }
                    switch (inmate001.Skill5)
                    {
                        case 1:
                            currentStats.shielded = true;
                            currentStats.shieldPercent = .07f;
                            currentStats.shieldHP = currentStats.maxShieldHP = currentStats.shieldPercent * currentStats.maxPlayerHealth;

                            break;
                        case 2:
                            currentStats.shielded = true;
                            currentStats.shieldPercent = .15f;
                            currentStats.shieldHP = currentStats.maxShieldHP = currentStats.shieldPercent * currentStats.maxPlayerHealth;
                            break;
                        default:
                            break;
                    }
                    switch (inmate001.Skill6)
                    {
                        case 1:
                            currentStats.baseDamageIncreasePerLevel += 1;

                            break;
                        case 2:
                            currentStats.baseDamageIncreasePerLevel += 2;
                            break;
                        case 3:
                            currentStats.baseDamageIncreasePerLevel += 3;
                            break;
                        default:
                            break;
                    }
                    switch (inmate001.Skill7)
                    {
                        case 1:
                            weapon.damageMultiplier += .03f;

                            break;
                        case 2:
                            weapon.damageMultiplier += .06f;
                            break;
                        case 3:
                            weapon.damageMultiplier += .12f;
                            break;
                        default:
                            break;
                    }
                    switch (inmate001.Skill8)
                    {
                        case 1:
                            weapon.criticalStrikeChance = .1f;

                            break;
                        case 2:
                            weapon.criticalStrikeChance = .2f;
                            break;
                        case 3:
                            weapon.criticalStrikeChance = .3f;
                            break;
                        default:
                            break;
                    }
                    switch (inmate001.Skill9)
                    {
                        case 1:
                            weapon.criticalStrikeDamage = 1.25f;

                            break;
                        case 2:
                            weapon.criticalStrikeDamage = 1.50f;
                            break;
                        default:
                            break;
                    }
                    switch (inmate001.Skill10)
                    {
                        case 1:
                            currentStats.damageResist = .05f;

                            break;
                        case 2:
                            currentStats.damageResist = .1f;
                            break;
                        default:
                            break;
                    }
                    if (inmate001.Skill11 >= 1)
                    {
                        slide.confidentSlide = true;
                    }





                }

                break;
            case CharType.croc:
                if (croc == null)
                {
                    Debug.Log("no Data to Load");
                }
                else
                {
                    GasTrail gasTrail = player.GetComponent<GasTrail>();

                    switch (croc.Skill1)
                    {
                        case 1:
                            currentStats.maxHealthIncreasePerLevel += 4;
                            break;
                        case 2:
                            currentStats.maxHealthIncreasePerLevel += 8;
                            break;
                        case 3:
                            currentStats.maxHealthIncreasePerLevel += 16;
                            break;
                        default:
                            break;
                    }
                    switch (croc.Skill2)
                    {
                        case 1:
                            currentMove.movementSpeedModifier = .02f;
                            break;
                        case 2:
                            currentMove.movementSpeedModifier = .04f;
                            break;
                        case 3:
                            currentMove.movementSpeedModifier = .08f;
                            break;
                        default:
                            break;
                    }
                    switch (croc.Skill3)
                    {
                        case 1:
                            currentStats.healthRegenPerLevel += .3f;
                            break;
                        case 2:
                            currentStats.healthRegenPerLevel += .6f;
                            break;
                        case 3:
                            currentStats.healthRegenPerLevel += 1.2f;
                            break;
                        default:
                            break;
                    }
                    switch (croc.Skill4)
                    {
                        case 1:
                            gasTrail.gasTrailDuration += 2;
                            break;
                        case 2:
                            gasTrail.gasTrailDuration += 3;
                            break;
                        default:
                            break;
                    }
                    switch (croc.Skill5)
                    {
                        case 1:
                            gasTrail.speedMod = .2f;
                            break;
                        case 2:
                            gasTrail.speedMod = .3f;
                             break;
                        default:
                            break;
                    }
                    //need help for the rest
                    //TRISTIN Fulfilling these
                    switch (croc.Skill6)
                    {
                        case 1: //done, simple increase per level
                            currentStats.baseDamageIncreasePerLevel += 0.3f;
                            break;
                        case 2:
                            currentStats.baseDamageIncreasePerLevel += 0.6f;
                            break;
                        case 3:
                            currentStats.baseDamageIncreasePerLevel += 0.9f;
                            break;
                        default:
                            break;
                    }
                    switch (croc.Skill7)    //enemies change movement speed
                    {
                        //come back to this later
                        case 1: //done, applied on enemies
                            enemiesSlowed = 0.05f;
                            break;
                        case 2:
                            enemiesSlowed = 0.1f;
                            break;
                        case 3:
                            enemiesSlowed = 0.15f;
                            break;
                        default:
                            break;
                    }
                    switch (croc.Skill8)    //enemies take damage when dealing it
                    {
                        case 1:
                            lashBack = 0.05f;
                            break;
                        case 2:
                            lashBack = 0.1f;
                            break;
                        case 3:
                            lashBack = 0.2f;
                            break;
                        default:
                            break;
                    }
                    switch (croc.Skill9)
                    {
                        case 1: //done, searches nearby enemies for the closest 1/2 and applies spikes
                            additionalEnemiesperSpike = 1;
                            break;
                        case 2:
                            additionalEnemiesperSpike = 2;
                            break;
                        default:
                            break;
                    }
                    switch (croc.Skill10)
                    {
                        //WILL BE IMPLEMENTED
                        case 1:
                            executeThresh = 0.1f;
                            break;
                        case 2:
                            executeThresh = 0.15f;
                            break;
                        default:
                            break;
                    }
                    if (croc.Skill11 >= 1)
                    {   //done, simple enemies add another spike on collision if true
                        doubleSpike = true;
                    }
                }
                break;
            case CharType.SLB:
                if (SLB == null)
                {
                    Debug.Log("no Data to Load");
                }
                else
                {
                    RocketAttack weapon = player.GetComponentInChildren<RocketAttack>();
                    ChestRocket chestRocket = player.GetComponentInChildren<ChestRocket>();
                    switch (SLB.Skill1)
                    {
                        case 1:
                            currentStats.maxHealthIncreasePerLevel += 4;
                            break;
                        case 2:
                            currentStats.maxHealthIncreasePerLevel += 8;
                            break;
                        case 3:
                            currentStats.maxHealthIncreasePerLevel += 16;
                            break;
                        default:
                            break;
                    }
                    switch (SLB.Skill2)
                    {
                        case 1:
                            currentMove.movementSpeedModifier = .04f;
                            break;
                        case 2:
                            currentMove.movementSpeedModifier = .08f;
                            break;
                        case 3:
                            currentMove.movementSpeedModifier = .16f;
                            break;
                        default:
                            break;
                    }
                    switch (SLB.Skill3)
                    {
                        case 1:
                            currentStats.healthRegenPerLevel += .3f;
                            break;
                        case 2:
                            currentStats.healthRegenPerLevel += .6f;
                            break;
                        case 3:
                            currentStats.healthRegenPerLevel += 1.2f;
                            break;
                        default:
                            break;
                    }
                    switch (SLB.Skill4)
                    {
                        case 1:
                            chestRocket.cooldown -= 2;
                            break;
                        case 2:
                            chestRocket.cooldown -= 4;
                            break;
                        default:
                            break;
                    }
                    switch (SLB.Skill5)
                    {
                        case 1:
                            chestRocket.projectilePrefab.GetComponent<RocketChestProjectile>().explosion.GetComponent<DestroyParticleSystem>().chestRocketMultiplyer = 4;

                            break;
                        case 2:
                            chestRocket.projectilePrefab.GetComponent<RocketChestProjectile>().explosion.GetComponent<DestroyParticleSystem>().chestRocketMultiplyer = 6;
                            break;
                        default:
                            break;
                    }
                    switch (SLB.Skill6)
                    {
                        case 1:
                            currentStats.baseDamageIncreasePerLevel += 2;

                            break;
                        case 2:
                            currentStats.baseDamageIncreasePerLevel += 3;
                            break;
                        case 3:
                            currentStats.baseDamageIncreasePerLevel += 4;
                            break;
                        default:
                            break;
                    }
                    switch (SLB.Skill7) //changed from mini stun to ---- SET OFF UNTIL FURTHER NOTICE
                    {
                        //come back to this later
                        case 1:
                            //enemyStunTime = 0.1f;
                            break;
                        case 2:
                            //enemyStunTime = 0.2f;
                            break;
                        case 3:
                            //enemyStunTime = 0.4f;
                            break;
                        default:
                            break;
                    }
                    switch (SLB.Skill8)
                    {
                        case 1:
                            weapon.criticalStrikeChance = .05f;

                            break;
                        case 2:
                            weapon.criticalStrikeChance = .1f;
                            break;
                        case 3:
                            weapon.criticalStrikeChance = .15f;
                            break;
                        default:
                            break;
                    }
                    switch (SLB.Skill9)
                    {
                        case 1:
                            currentStats.damageResist += .2f;

                            break;
                        case 2:
                            currentStats.damageResist += .3f;
                            break;
                        default:
                            break;
                    }
                    switch (SLB.Skill10)
                    {
                        case 1:
                            currentMove.sprintSpeedModifier = .2f;

                            break;
                        case 2:
                            currentMove.sprintSpeedModifier = .4f;
                            break;
                        default:
                            break;
                    }
                    if (SLB.Skill11 >= 1)
                    {
                        currentStats.speedyRecovery = true;
                    }
                }
                break;
            case CharType.MPGuard:
                if (MPGuard == null)
                {
                    Debug.Log("no Data to Load");
                }
                else
                {
                    StealthGenerator generator = player.GetComponent<StealthGenerator>();
                    ConsecutiveShots weapon = player.transform.Find("CM vcam1").GetComponentInChildren<ConsecutiveShots>();
                    switch (MPGuard.Skill1)
                    {
                        case 1:
                            currentStats.maxHealthIncreasePerLevel += 1;
                            break;
                        case 2:
                            currentStats.maxHealthIncreasePerLevel += 2;
                            break;
                        case 3:
                            currentStats.maxHealthIncreasePerLevel += 3;
                            break;
                        default:
                            break;
                    }
                    switch (MPGuard.Skill2)
                    {
                        case 1:
                            currentMove.movementSpeedModifier = .04f;
                            break;
                        case 2:
                            currentMove.movementSpeedModifier = .08f;
                            break;
                        case 3:
                            currentMove.movementSpeedModifier = .16f;
                            break;
                        default:
                            break;
                    }
                    switch (MPGuard.Skill3)
                    {
                        case 1:
                            currentStats.healthRegenPerLevel += .2f;
                            break;
                        case 2:
                            currentStats.healthRegenPerLevel += .4f;
                            break;
                        case 3:
                            currentStats.healthRegenPerLevel += .6f;
                            break;
                        default:
                            break;
                    }
                    switch (MPGuard.Skill4)
                    {
                        case 1:
                            generator.cooldown -= 1;
                            break;
                        case 2:
                            generator.cooldown -= 2;
                            break;
                        default:
                            break;
                    }
                    switch (MPGuard.Skill5)
                    {
                        case 1:
                            generator.speedMult = .2f;

                            break;
                        case 2:
                            generator.speedMult = .4f;
                            break;
                        default:
                            break;
                    }
                    switch (MPGuard.Skill6)
                    {
                        case 1:
                            currentStats.baseDamageIncreasePerLevel += 3;

                            break;
                        case 2:
                            currentStats.baseDamageIncreasePerLevel += 6;
                            break;
                        case 3:
                            currentStats.baseDamageIncreasePerLevel += 12;
                            break;
                        default:
                            break;
                    }
                    switch (MPGuard.Skill7)
                    {
                        case 1:
                            weapon.damageMultiplier += .02f;

                            break;
                        case 2:
                            weapon.damageMultiplier += .04f;
                            break;
                        case 3:
                            weapon.damageMultiplier += .08f;
                            break;
                        default:
                            break;
                    }
                    switch (MPGuard.Skill8)
                    {
                        case 1:
                            weapon.criticalStrikeChance = .05f;

                            break;
                        case 2:
                            weapon.criticalStrikeChance = .1f;
                            break;
                        case 3:
                            weapon.criticalStrikeChance = .2f;
                            break;
                        default:
                            break;
                    }
                    switch (MPGuard.Skill9)
                    {
                        case 1:
                            weapon.shotsFired += 1;

                            break;
                        case 2:
                            weapon.shotsFired += 2;
                            break;
                        default:
                            break;
                    }
                    switch (MPGuard.Skill10)
                    {
                        case 1:
                            weapon.cooldown -= .4f;

                            break;
                        case 2:
                            weapon.cooldown -= .8f;
                            break;
                        default:
                            break;
                    }
                    if (MPGuard.Skill11 >= 1)
                    {
                        generator.emergeFromShadow = true;
                    }
                }
                break;
            default:
                break;
        }



    }






 
}
