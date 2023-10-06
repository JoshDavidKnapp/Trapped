using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNotifications : MonoBehaviour
{
    public bool TutorialCompletion;
    public Image[] Background;
    public Text[] Description;
    public GameObject[] ToggleButtons;
    private int TutorialStage;

    private bool Display;
    private float Opacity;
    public float StartDelay;
    public float TotalDuration;
    public float FadeDuration;
    private float FadeValue;
    private float Linger;
    private bool Fading;
    public bool Settings;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveSystem.LoadTutorialStatus() != null)
        {
            LoadPlayerStatsData();

        }
        else
        {
            TutorialCompletion = false;
        }
        if (Settings == false)
        {
            Opacity = 0;
            Linger = 0;
            Display = true;
            Fading = false;
            TotalDuration = TotalDuration - (FadeDuration * 2);

            for (var i = 0; i < Background.Length; i++)
            {
                //Changes opacity for the background
                var TempBackColor = Background[i].color;
                TempBackColor.a = Opacity;
                Background[i].color = TempBackColor;

                //Changes opacity for the Description
                var TempDescColor = Description[i].color;
                TempDescColor.a = Opacity;
                Description[i].color = TempDescColor;
            }
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the tutorial is complete and the gamebobject isn't meant for settings
        if (TutorialCompletion == false && Settings == false)
        {
            //Checks if the start delay is over
            if (StartDelay > 0)
            {
                StartDelay -= Time.deltaTime;
            }
            else
            {
                //Activates the fade function
                FadeFunction();
            }
        }
        else
        {
            //Checks if the gameobject is from the settings menu
            if (Settings == true)
            {
                //Activates the setting display function
                SettingsDisplay();
            }
            else
            {
                //Disables the gameobject
                gameObject.SetActive(false);
            }
        }
        
        

    }

    //Function that displays the toggle buttons
    private void SettingsDisplay()
    {
        if (TutorialCompletion == true)
        {
            ToggleButtons[0].SetActive(true);
            ToggleButtons[1].SetActive(false);
        }
        else
        {
            ToggleButtons[1].SetActive(true);
            ToggleButtons[0].SetActive(false);
        }
    }
    
    //Function that causes the gameobject to fade in
    private void FadeIn()
    {
        FadeValue += (Time.deltaTime / FadeDuration);

        Opacity = FadeValue;

        OpacityChange();
    }

    //Function that causes the gameobject to fade out
    private void FadeOut()
    {
        FadeValue -= (Time.deltaTime / FadeDuration);

        Opacity = FadeValue;

        OpacityChange();
    }

    //Function that's responsible for the opacity change
    private void OpacityChange()
    {
        //Changes opacity for the background
        var TempBackColor = Background[TutorialStage].color;
        TempBackColor.a = Opacity;
        Background[TutorialStage].color = TempBackColor;

        //Changes opacity for the Description
        var TempDescColor = Description[TutorialStage].color;
        TempDescColor.a = Opacity;
        Description[TutorialStage].color = TempDescColor;
    }

    //Function that controls the entire fading cycle
    private void FadeFunction()
    {
        //Checks if something is supposed to be displayed
        if (Display == true)
        {
            print("Display");
            //Checks if the notification display isn't meant to fading out yet
            if (Fading == false)
            {
                //Checks if the notification display isn't finished fading in yet
                if (Opacity < 1)
                {
                    //Activates the FadeIn function
                    FadeIn();
                }
                else
                {
                    //Checks if the linger timer has surpassed the TotalDuration value
                    if (Linger < TotalDuration)
                    {
                        Linger += Time.deltaTime;
                    }
                    else
                    {
                        //Resets linger back to zero
                        Linger = 0;
                        //Begins the fading out process
                        Fading = true;
                    }
                }
            }
            else
            {
                //Checks if the notification display isn't finished fading out yet
                if (Opacity > 0)
                {
                    //Activates the FadeOut function
                    FadeOut();
                }
                else
                {
                    //Checks if TutorialStage
                    if (TutorialStage < Background.Length - 1)
                    {
                        //Resets the display cycle for the next tutorial display
                        Fading = false;
                        TutorialStage += 1;
                    }
                    else
                    {
                        //Ends the display cycle
                        Display = false;
                        print("End Display");
                    }

                }
            }
        }
    }

    //Loads saved data
    private void LoadPlayerStatsData()
    {
        PlayerData data = SaveSystem.LoadTutorialStatus();
        TutorialCompletion = data.TutorialCompletion;
        print("Tutorial Completion is " + TutorialCompletion);
    }

    //Saves the tutorial being completed
    public void SavePlayerStatsData()
    {
        TutorialCompletion = true;
        SaveSystem.SaveTutorialStatus(this);
        print("SaveData");
    }

    //Changes the tutorial complet
    public void ChangeCompletionStatus()
    {
        //Sets tutorial completion to false
        if (TutorialCompletion == true)
        {
            TutorialCompletion = false;
        }
        else
        {
            //Sets tutorial completion to true
            if (TutorialCompletion == false)
            {
                TutorialCompletion = true;
            }
        }

        
        //Saves change
        SaveSystem.SaveTutorialStatus(this);
        print("Tutorial Completion is " + TutorialCompletion);
    }
}
