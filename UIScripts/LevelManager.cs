using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject PlayerUI;

    public ShieldUI shieldUI;

    public LevelLoader levelLoader;

    public SetCharacterValues setCharacterValues;

    public PlayerLocomotion playerLocomotion;

    public HealthUI playerHealthUI;

    public ExperienceUI playerXPUI;

    public PauseMenu pauseMenuController;

    public GameObject destroyUndestroyable;

    public GameObject sceneManager;
    
    public GameObject gameOverUI;

    public GameObject playerUI;

    public CameraManager cameraManager;

    public EnemyMaster currentEnemyMaster;

    public HudDisplay hud;

    public GameObject player;

    public GameObject teleporter;

    private GameObject newTeleport;

    private GameObject[] spawnPoints;

    private GameObject playerFollow;

    public string[] potentialMapsLvl1, potentialMapsLvl2, potentialMapsLvl3, potentialMapsLvl4;

    private string currentMap;

    public bool testSpecific;

    private bool dead = false, following = false;

    public int testNum;

    public int LevelCounter, totalLevelCounter, totalEnemies, difficulty;

    public int loopCounter;

    public bool canPause = false;


    public float totalDamage;

    public float runTimer;
    //every 60 seconds, I will increase minute counter by 60
    private float minuteCounter;


    public static LevelManager levelManager;

    public GameObject sprintReticle, crosshair;

    private void Awake()
    {
        levelManager = this;
        sceneManager = GameObject.Find("CharSelecter");
        gameOverUI.SetActive(false);
        playerUI.SetActive(true);

        SelectAndSpawnMap(potentialMapsLvl1);
        
        runTimer = 0;
        totalLevelCounter = LevelCounter = 1;
        loopCounter = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManage sceneManagerScript = sceneManager.GetComponent<SceneManage>();
        setCharacterValues = sceneManager.GetComponent<SetCharacterValues>();

        PickAndSpawn(sceneManagerScript);
        setCharacterValues.player = player;
        setCharacterValues.AddBuffToCharacter();

        cameraManager.FindPlayerAtStart();
        canPause = true;
       
        //Instantiate(sceneManagerScript.prefabToSpawn, new Vector3(0,1,-5), Quaternion.identity);
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerLocomotion.isSprinting)
        {
            sprintReticle.SetActive(true);
            crosshair.SetActive(false);
        }
        else
        {
            sprintReticle.SetActive(false);
            crosshair.SetActive(true);

        }


        runTimer += Time.deltaTime;
        /* Tristin's Add -- Enemy Level Up
         * Enemy level is calculated every minute, active spawners is increased 
         * by 1, and cooldowns of spawners are decreased by 1. 
         */
        if (currentEnemyMaster && (runTimer - minuteCounter >= 60))
        {
            minuteCounter += 60;
            currentEnemyMaster.LevelUp();
        }

        if (player.GetComponent<PlayerStats>().currentPlayerHealth <= 0&&!dead)
        {
            //player.GetComponent<PlayerLocomotion>().movement.Stop();
            canPause = false;
            dead = true;
            playerUI.SetActive(false);
            
            //levelLoader.levelName = "CharacterSelector";
            sceneManager.GetComponent<SceneManage>().totalTime = runTimer;
            totalEnemies += currentEnemyMaster.totalKills;
            totalDamage += currentEnemyMaster.totalDamage;
            //Saves Data - Trevor
            SaveData();
            playerUI.SetActive(false);
            gameOverUI.SetActive(true);
            RemoveDontDestroyOnLoad();
        }
        
        if (following&&playerFollow!=null)
        {
            player.transform.position = playerFollow.transform.position;
           

        }


    }
    
    public void RemoveDontDestroyOnLoad()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.GetComponent<PlayerLocomotion>().canMove = false;
       
        player.transform.parent = destroyUndestroyable.transform;
        //sceneManager.transform.parent = destroyUndestroyable.transform;
    }


    //choses what level map to be loaded
    private void SelectAndSpawnMap(string[] potentialMaps)
    {
        //test for maps
        if (potentialMaps == null)
        {
            Debug.Log("ERROR NO MAPS TO LOAD");

        }
        if (sceneManager.GetComponent<SceneManage>().debugMode)
        {
            switch (sceneManager.GetComponent<SceneManage>().level)
            {
                case DebugLevel.FrostedPeaks:
                    SceneManager.LoadScene("Frosted Peaks", LoadSceneMode.Additive);
                    currentMap = "Frosted Peaks";
                    break;
                case DebugLevel.IslandCity:
                    SceneManager.LoadScene("Island City", LoadSceneMode.Additive);
                    currentMap = "Island City";
                    break;
                case DebugLevel.MidnightSpires:
                    SceneManager.LoadScene("Midnight Spires", LoadSceneMode.Additive);
                    currentMap = "Midnight Spires";
                    break;
                case DebugLevel.OceanSanctuary:
                    SceneManager.LoadScene("Ocean Sanctuary", LoadSceneMode.Additive);
                    currentMap = "Ocean Sanctuary";
                    break;
                case DebugLevel.SwampsOfFear:
                    SceneManager.LoadScene("Swamp of Fear", LoadSceneMode.Additive);
                    currentMap = "Swamp of Fear";
                    break;
                default:
                    break;
            }

        }
        else
        {
            //find the amount of map choices
            int mapChoices = potentialMaps.Length;

            //if not testing a specific level pick random map
            if (!testSpecific)
            {
                int randomChoice = Random.Range(0, mapChoices);
                if (potentialMaps[randomChoice] == null)
                {
                    Debug.Log("ERROR NO MAP IN SLOT " + randomChoice.ToString() + " TO LOAD");
                }
                SceneManager.LoadScene(potentialMaps[randomChoice], LoadSceneMode.Additive);
                currentMap = potentialMaps[randomChoice];
                
            }
            //if testing Specific
            if (testSpecific)
            {
                if (testNum >= 0 && testNum <= mapChoices - 1)
                {
                    SceneManager.LoadScene(potentialMaps[testNum], LoadSceneMode.Additive);
                }
                else
                    Debug.Log("ERROR MAP NUMBER NOT FOUND");

            }
        }

        StartCoroutine(waitToSetActive(currentMap));

       
    }


    //picks random spawn location and spawns teleporter and player
    private void PickAndSpawn(SceneManage sceneManagerScript)
    {
        PlayerStats tempStatHolder;
        hud.FuelCellDisplay(0);
        spawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawn");
        Debug.Log(spawnPoints.Length);

        int randomChoice = Random.Range(0, spawnPoints.Length);
       
        if (totalLevelCounter <= 1)
        {
            player = Instantiate(sceneManagerScript.prefabToSpawn);
            hud.GetPlayerAbility(player);
            hud.GetPlayerPrimary(player);
           
        }
        tempStatHolder = player.GetComponent<PlayerStats>();
        shieldUI.playerStats = tempStatHolder;
        setCharacterValues.currentStats = player.GetComponent<PlayerStats>();
        playerLocomotion = player.GetComponent<PlayerLocomotion>();
        newTeleport = Instantiate(teleporter);

       
        
        newTeleport.transform.position = spawnPoints[randomChoice].transform.position;
        newTeleport.transform.rotation = spawnPoints[randomChoice].transform.rotation;
        player.transform.position = new Vector3(spawnPoints[randomChoice].transform.position.x,spawnPoints[randomChoice].transform.position.y+3,spawnPoints[randomChoice].transform.position.z);
        
        tempStatHolder.hud = hud;
        tempStatHolder.portal = newTeleport.GetComponentInChildren<PortalController>();
        playerHealthUI.HealthBarSetUp(player);
        playerXPUI.XpSetUp(player);
        currentEnemyMaster = GameObject.Find("EnemyMaster").GetComponent<EnemyMaster>();
        //ADD FOR PLAYER ICONS
        gameObject.GetComponent<AbilityIcons>().SetIcons();

        /* Tristin's Add -- Difficulty Buttons
         * Buttons [0, 5] Will Apply Buffs either through the enemies
         * or through the enemy master. 
         */
        //SUB ADD -- Difficulty Display During Run
        if (GameObject.Find("DifficultySelector"))
        {
            Color[] diffColors = { new Color(0, 1, 0), //0
                new Color(0.5f, 1, 0), //1
                new Color(1, 1, 0), //2
                new Color(1, 0.75f, 0), //3
                new Color(1, 0.5f, 0), //4
                new Color(1, 0, 0) }; //5
            difficulty = GameObject.Find("DifficultySelector").GetComponent<DifficultyButtons>().GetDifficulty();
            playerUI.transform.GetChild(9).transform.GetChild(0).gameObject.GetComponent<Image>().color = diffColors[difficulty];
            playerUI.transform.GetChild(9).transform.GetChild(1).gameObject.GetComponent<Text>().text = difficulty.ToString();

            Destroy(GameObject.Find("DifficultySelector"));
        }
        
        currentEnemyMaster.SetDifficulty(difficulty);
        /* Tristin's Addition -- Boss Text Elements
         * Arrival Text element spawns mid-upper screen moving upwards,
         * disappears to reveal Name Text element with child healthbar slider.
         * -- EnemyMaster initialization at the beginning of each level
         * The level would has been reset each level as a new enemyMaster is
         * used, restarting its clock and only retaining stageCompleted given
         * by the levelMaster just above.
         */
        currentEnemyMaster.bossArrived = playerUI.transform.GetChild(6).gameObject;
        currentEnemyMaster.bossName = playerUI.transform.GetChild(7).gameObject;
        currentEnemyMaster.levelUp = playerUI.transform.GetChild(8).gameObject;
        currentEnemyMaster.Initialize((int)(runTimer/60), totalLevelCounter - 1);


        
        setCharacterValues.currentMove = player.GetComponent<PlayerLocomotion>();
        
        
        hud.stageUpdate(totalLevelCounter);

        currentEnemyMaster.player = player;
        pauseMenuController.Player = player;

        //player intro
        playerFollow = GameObject.Find("INTROPLAYER");
        StartCoroutine(followIntro());




    }


    public void OnLevelComplete()
    {
       
        if (LevelCounter <= 4)
        {
            LevelCounter++;
        }
        if(LevelCounter >= 5)
        {
            LevelCounter = 1;
            loopCounter++;
        }
        totalLevelCounter++;
        totalEnemies += currentEnemyMaster.totalKills;
        totalDamage += currentEnemyMaster.totalDamage;
    }
   
    public void travereseNextLevel()
    {
        StartCoroutine(waitForUnload());

    }
    private void resetLevelStats()
    {
        PlayerStats playerStats = player.GetComponent<PlayerStats>();

        playerStats.allCellsCollected = false;
        playerStats.collectedCells = 0;
       
        playerStats.currentPlayerHealth = playerStats.maxPlayerHealth;
        playerStats.hud.PortalDisplaySwitch();

    }
    IEnumerator waitForUnload()
    {
        levelLoader.transition.SetTrigger("Start");
        OnLevelComplete();
        yield return new WaitForSeconds(1);
        SceneManager.UnloadSceneAsync(currentMap);
        string[] arrayOfMaps;
        Destroy(newTeleport);

        switch (LevelCounter)
        {
            case 1:
                arrayOfMaps = potentialMapsLvl1;
                break;
            case 2:
                arrayOfMaps = potentialMapsLvl2;
                break;
            case 3:
                arrayOfMaps = potentialMapsLvl3;
                break;
            case 4:
                arrayOfMaps = potentialMapsLvl4;
                break;
            default:
                arrayOfMaps = new string[0];
                Debug.Log("ERROR OUT OF RANGE");
                break;
        }
        SelectAndSpawnMap(arrayOfMaps);
        resetLevelStats();
        StartCoroutine(ReloadNew());

    }
IEnumerator ReloadNew()
    {
        yield return new WaitForSeconds(2);
        PickAndSpawn(sceneManager.GetComponent<SceneManage>());
        levelLoader.transition.SetTrigger("End"); 
    }

IEnumerator waitToSetActive(string levelName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelName));

    }

        
    //Save Function - Trevor
    public void SaveData()
    {
        SaveSystem.SaveLevelManagerPlayer(this);
        print("Saved Level");
    }


IEnumerator followIntro()
    {
        playerLocomotion.canMove = false;
        following = true;
        yield return new WaitForSeconds(3.6f);
        following = false;
        playerLocomotion.canMove = true;

    }

}
