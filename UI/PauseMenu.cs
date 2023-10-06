using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject PauseUi;
    public bool Paused,charSelect;
    public GameObject Player;
    public GameObject Loader;

    public GameObject charSelector;


    public LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        Paused = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (levelManager != null)
        {
            //Activates when player presses escape
            if (Input.GetKeyDown("escape") && levelManager.canPause)
            {
                //Checks to see if game is paused
                if (Paused == false)
                {
                    //Turns on Pausemenu UI
                    PauseUi.SetActive(true);

                    Player.GetComponent<PlayerLocomotion>().canMove = false;

                    //Pauses all functions relying on time variables
                    Time.timeScale = 0f;

                    //Tells scripts that game is paused
                    Paused = true;

                    //Makes Cursor visible
                    Cursor.visible = true;

                    //Unlocks cursor lockstate
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    //Turns off Pausemenu UI
                    if (PauseUi.activeSelf)
                    {
                        PauseUi.SetActive(false);
                    }
                    else
                    {
                        settings.SetActive(false);
                    }
                    //Unpauses all functions relying on time variables
                    Time.timeScale = 1f;

                    Player.GetComponent<PlayerLocomotion>().canMove = true;

                    //Tells scripts that game is unpaused
                    Paused = false;

                    //Makes Cursor invisible
                    Cursor.visible = false;

                    Cursor.lockState = CursorLockMode.Confined;
                }

            }
        }
        else if(charSelect&& Input.GetKeyDown("escape"))
        {
            if (Paused == false)
            {
                //Turns on Pausemenu UI
                PauseUi.SetActive(true);

              

                //Pauses all functions relying on time variables
                Time.timeScale = 0f;

                //Tells scripts that game is paused
                Paused = true;

                
            }
            else
            {
                //Turns off Pausemenu UI
                if (PauseUi.activeSelf)
                {
                    PauseUi.SetActive(false);
                }
                else
                {
                    settings.SetActive(false);
                }
                //Unpauses all functions relying on time variables
                Time.timeScale = 1f;

               

                //Tells scripts that game is unpaused
                Paused = false;
             
            }
        }
    }

    //Function to resume game
    public void ResumeFunction()
    {
       
        //Turns off Pausemenu UI
        PauseUi.SetActive(false);

        //Unpauses all functions relying on time variables
        Time.timeScale = 1f;

        //Tells scripts that game is unpaused
        Paused = false;

        if (!charSelect)
        {
            Player.GetComponent<PlayerLocomotion>().canMove = true;
            //Makes Cursor invisible
            Cursor.visible = false;

            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    //Function that closes the game
    public void Quit()
    {
       
        Loader.GetComponent<LevelLoader>().Quit();
    }

    //Function that opens settings menu
    public void OpenSettings()
    {
        //disables the main pause menu ui
        PauseUi.SetActive(false);
        //enables the setting pause menu ui
        settings.SetActive(true);
    }

    //Function that switches over to the main menu
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        if (!charSelect)
        {
            levelManager.RemoveDontDestroyOnLoad();
            
        }
        if (charSelect)
        {
            Destroy(charSelector);
        }
        Loader.GetComponent<LevelLoader>().LoadMainMenu();
    }
}
