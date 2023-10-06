using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractRaycast : MonoBehaviour
{
    private GameObject chestContents;
    public GameObject pickupText;

    private int rotation;

    GameObject chest;
    int counter = 0;
    public GameObject player;

    public ParticleSystem absorbParticle;

    AugmentUI augmentUI;
    Augment augmentScript;

    [Header("Rare")]
    public Sprite damageLearningImage;
    public Sprite powerOscillationImage;
    public Sprite powerShieldingImage;

    [Header("Uncommon")]
    public Sprite spikeExplosionImage;
    public Sprite weaponRepeaterImage;
    public Sprite criticalResponseImage;
    public Sprite doubleJumpImage;

    [Header("Common")]
    public Sprite energyConservationImage;
    public Sprite lifeDrainImage;
    public Sprite magneticShieldingImage;
    public Sprite regenOverchargeImage;
    public Sprite safeRecoveryImage;
    public Sprite weaponDisplayImage;
    public Sprite weaponOverchargeImage;

    //trevors code
    //GameObject for notification ui
    public GameObject NotifyUI;
    //Bool slots for every augment
    public bool[] AugUnlocked = new bool[20];


    // Start is called before the first frame update
    void Start()
    {
        augmentUI = GameObject.Find("AugmentPanel").GetComponent<AugmentUI>();
        augmentScript = gameObject.GetComponentInParent<Augment>();

        //Searches for the Notification UI and attaches it to this script
        NotifyUI = GameObject.FindGameObjectWithTag("NotificationPopUp");
        //Sets AugUnlocked to false or true based on whether the achievement save file has listed them as unlocked or locked
        if (SaveSystem.LoadPickUpAchievmentProgress() != null)
        {
            AchievementData data = SaveSystem.LoadPickUpAchievmentProgress();

            for (var i = 0; i < AugUnlocked.Length; i++)
            {
                if (data.AchievmentProgress[i] == true)
                {
                    AugUnlocked[i] = true;
                }
                else
                {
                    AugUnlocked[i] = false;
                }
            }
        }
        else
        {
            for (var i = 0; i < AugUnlocked.Length; i++)
            {

                AugUnlocked[i] = false;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Raycast variable
        RaycastHit hit;


        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20f))
        {

            if (hit.transform.gameObject.tag != "RareChestAugment" || hit.transform.gameObject.tag != "UncommonChestAugment" || hit.transform.gameObject.tag != "CommonChestAugment" || hit.transform.gameObject != null)
            {
                pickupText.SetActive(false);

            }
            else
            {
                pickupText.SetActive(true);

            }


            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

            if(hit.transform.gameObject.tag == "Chest")
            {
                chest = hit.transform.gameObject; 

                if(counter == 0)
                {
                    counter++;
                    chest.GetComponentInChildren<Animator>().SetBool("ChestOpen", true);

                    chestContents = chest.GetComponent<ChestScript>().chestContents;

                    StartCoroutine(OpenChest());

                }

            }

            if(hit.transform.gameObject.tag == "RareChestAugment")
            {
                pickupText.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.transform.gameObject);

                    absorbParticle.startColor = Color.yellow;
                    absorbParticle.Play();


                    int augment = Random.Range(1, 4);
                    print(augment);
                    if (augment == 1)
                    {
                        print("damage learning");
                        player.GetComponent<Augment>().DamageLearning();
                        augmentUI.GetComponent<AugmentUI>().SetAugment(damageLearningImage, "DamageLearning");
                        augmentUI.damageLearning = true;
                        Notification(11);

                    }
                    if (augment == 2)
                    {
                        print("power shielding");

                        player.GetComponent<Augment>().PowerShielding();
                        augmentUI.GetComponent<AugmentUI>().SetAugment(powerShieldingImage, "PowerShielding");
                        augmentUI.powerShielding = true;
                        Notification(12);

                    }
                    if (augment == 3)
                    {
                        print("power oscillation");

                        StartCoroutine(player.GetComponent<Augment>().PowerOscillation());
                        augmentUI.GetComponent<AugmentUI>().SetAugment(powerOscillationImage, "PowerOscillation");
                        augmentUI.powerOscillation = true;
                        Notification(13);

                    }

                    pickupText.SetActive(false);
                    print("PICKED UP ITEM");
                }
            }



            if (hit.transform.gameObject.tag == "UncommonChestAugment")
            {
                pickupText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.transform.gameObject);

                    //uncommon

                    int chanceChest = Random.Range(0, 100);
                    if(chanceChest < 10)
                    {

                        absorbParticle.startColor = Color.yellow;
                        absorbParticle.Play();

                        int rareAugment = Random.Range(1, 4);
                        print(rareAugment);
                        if (rareAugment == 1)
                        {
                            print("damage learning");
                            player.GetComponent<Augment>().DamageLearning();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(damageLearningImage, "DamageLearning");
                            augmentUI.damageLearning = true;
                            Notification(11);

                        }
                        if (rareAugment == 2)
                        {
                            print("power shielding");

                            player.GetComponent<Augment>().PowerShielding();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(powerShieldingImage, "PowerShielding");
                            augmentUI.powerShielding = true;
                            Notification(12);

                        }
                        if (rareAugment == 3)
                        {
                            print("power oscillation");
                            StartCoroutine(player.GetComponent<Augment>().PowerOscillation());
                            augmentUI.GetComponent<AugmentUI>().SetAugment(powerOscillationImage, "PowerOscillation");
                            augmentUI.powerOscillation = true;
                            Notification(13);

                        }
                    }
                    else
                    {

                        absorbParticle.startColor = Color.magenta;
                        absorbParticle.Play();

                        int augment = Random.Range(1, 5);
                        print(augment);
                        if (augment == 1)
                        {
                            print("double jump");
                            player.GetComponent<Augment>().DoubleJump();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(doubleJumpImage, "DoubleJump");
                            augmentUI.doubleJump = true;
                            Notification(7);

                        }
                        if (augment == 2)
                        {
                            print("weapon repeats");
                            player.GetComponent<Augment>().WeaponRepeater();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(weaponRepeaterImage, "WeaponRepeater");
                            augmentUI.weaponRepeat = true;
                            Notification(8);


                        }
                        if (augment == 3)
                        {
                            print("critical response");
                            player.GetComponent<Augment>().isCriticalResponse = true;
                            augmentUI.GetComponent<AugmentUI>().SetAugment(criticalResponseImage, "CriticalResponse");
                            augmentUI.isCriticalResponse = true;
                            Notification(9);


                        }
                        if (augment == 4)
                        {
                            print("spike sxplosion");
                            player.GetComponent<Augment>().SpikedExplosion();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(spikeExplosionImage, "SpikedExplosion");
                            augmentUI.spikedExplosion = true;
                            Notification(10);

                        }
                    }


                    pickupText.SetActive(false);
                    print("PICKED UP ITEM");
                }


              
            }







            if (hit.transform.gameObject.tag == "CommonChestAugment")
            {
                pickupText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.transform.gameObject);

                    //common

                    int chanceChest = Random.Range(0, 100);
                    if (chanceChest == 0)
                    {

                        absorbParticle.startColor = Color.yellow;
                        absorbParticle.Play();

                        int rareAugment = Random.Range(1, 4);
                        print(rareAugment);
                        if (rareAugment == 1)
                        {
                            print("damage learning");
                            player.GetComponent<Augment>().DamageLearning();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(damageLearningImage, "DamageLearning");
                            augmentUI.damageLearning = true;
                            Notification(11);

                        }
                        if (rareAugment == 2)
                        {
                            print("power shielding");

                            player.GetComponent<Augment>().PowerShielding();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(powerShieldingImage, "PowerShielding");
                            augmentUI.powerShielding = true;
                            Notification(12);


                        }
                        if (rareAugment == 3)
                        {
                            print("power oscillation");
                            StartCoroutine(player.GetComponent<Augment>().PowerOscillation());
                            augmentUI.GetComponent<AugmentUI>().SetAugment(powerOscillationImage, "PowerOscillation");
                            augmentUI.powerOscillation = true;
                            Notification(13);
                        }
                    }
                    else if (chanceChest > 0 && chanceChest < 20)
                    {

                        absorbParticle.startColor = Color.magenta;
                        absorbParticle.Play();

                        int augment = Random.Range(1, 5);
                        print(augment);
                        if (augment == 1)
                        {
                            print("double jump");
                            player.GetComponent<Augment>().DoubleJump();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(doubleJumpImage, "DoubleJump");
                            augmentUI.doubleJump = true;
                            Notification(7);

                        }
                        if (augment == 2)
                        {
                            print("weapon repeats");
                            player.GetComponent<Augment>().WeaponRepeater();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(weaponRepeaterImage, "WeaponRepeater");
                            augmentUI.weaponRepeat = true;
                            Notification(8);

                        }
                        if (augment == 3)
                        {
                            print("critical response");
                            player.GetComponent<Augment>().isCriticalResponse = true;
                            augmentUI.GetComponent<AugmentUI>().SetAugment(criticalResponseImage, "CriticalResponse");
                            augmentUI.isCriticalResponse = true;
                            Notification(9);

                        }
                        if (augment == 4)
                        {
                            print("spike explosion");
                            player.GetComponent<Augment>().SpikedExplosion();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(spikeExplosionImage, "SpikedExplosion");
                            augmentUI.spikedExplosion = true;
                            Notification(10);

                        }
                    }
                    else if (chanceChest >= 20)
                    {

                        absorbParticle.startColor = Color.white;
                        absorbParticle.Play();
                        //normal augment

                        int augment = Random.Range(1, 8);
                        print(augment);
                        if (augment == 1)
                        {
                            print("magnetic shielding");
                            player.GetComponent<Augment>().MagneticShielding();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(magneticShieldingImage, "MagneticShielding");
                            augmentUI.magneticShielding = true;
                            Notification(0);

                        }
                        if (augment == 2)
                        {
                            print("energy conservation");
                            player.GetComponent<Augment>().EnergyConvservation();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(energyConservationImage, "EnergyConservation");
                            augmentUI.energyConservation = true;
                            Notification(1);

                        }
                        if (augment == 3)
                        {
                            print("weapon overcharge");
                            player.GetComponent<Augment>().WeaponOverCharge();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(weaponOverchargeImage, "WeaponOvercharge");
                            augmentUI.weaponOvercharge = true;
                            Notification(2);
                        }
                        if (augment == 4)
                        {
                            print("regen overcharge");
                            player.GetComponent<Augment>().RegenOvercharge();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(regenOverchargeImage, "RegenOvercharge");
                            augmentUI.regenOvercharge = true;
                            Notification(3);

                        }
                        if (augment == 5)
                        {
                            print("weapon display");
                            player.GetComponent<Augment>().WeaponDisplay();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(weaponDisplayImage, "WeaponDisplay");
                            augmentUI.weaponDisplay = true;
                            Notification(4);

                        }
                        if (augment == 6)
                        {
                            print("safe recovery");
                            player.GetComponent<Augment>().SafeRecovery();
                            augmentUI.GetComponent<AugmentUI>().SetAugment(safeRecoveryImage, "SafeRecovery");
                            augmentUI.safeRecovery = true;
                            Notification(5);

                        }
                        if (augment == 7)
                        {
                            print("life drain");
                            player.GetComponent<Augment>().canLifeDrain = true;
                            augmentUI.GetComponent<AugmentUI>().SetAugment(lifeDrainImage, "LifeDrain");
                            augmentUI.lifeDrain = true;
                            Notification(6);

                        }
                    }


                    pickupText.SetActive(false);
                    print("PICKED UP ITEM");
                }



            }


















        }



    }
    //Activates notification display
    private void Notification(int prog)
    {
        if (AugUnlocked[prog] == false)
        {
            NotifyUI.GetComponent<NotificationDisplay>().UnlockAchievement(prog);
            AugUnlocked[prog] = true;
            SaveSystem.SavePickUpAchievmentProgress(this);
            print("Saved Achvievement " + prog);
        }
        else
        {
            NotifyUI.GetComponent<NotificationDisplay>().UnlockAchievement(prog + 20);

            print("Load Aug " + (prog + 20));
        }
    }
    public IEnumerator OpenChest()
    {
        yield return new WaitForSeconds(1);

        chest.GetComponent<BoxCollider>().enabled = false;
        Instantiate(chestContents, chest.transform.position, Quaternion.Euler(0, 180, 0));
        counter = 0;
                
    }
}
    