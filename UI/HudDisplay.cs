using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudDisplay : MonoBehaviour
{
    //Public Plug-Ins
    //public GameObject DifficultySlider;
    //public GameObject DiffiSlide1;
    //public GameObject DiffiSlide2;
    //public GameObject DiffiSlide3;
    public LevelManager levelManager;

    public GameObject DifficultySlide;
    public Vector3 SlideSpawnPoint;

    public Text Loop;
    public Text Timer;
    public Text Portal;
    public Text fuelCellsPicked;

    
    /*public Image DiffiBar1;
    public Image DiffiBar2;
    public Image DiffiBar3;
    public Text DiffiText1;
    public Text DiffiText2;
    public Text DiffiText3;*/
    public Image PortColor;
    public GameObject Player;
    public Slider Ability1;
    public Slider Ability2;

    //Ability DisplayVariables
    private float Ability1Cooldown;
    private float Ability1recharge;
    private float Ability2Cooldown;
    private float Ability2recharge;

    //Difficulty variables
    public float NormalQue;
    public float HardQue;
    public float InfiniQue;
    public int CurrentQue;
    public int ColorQue;
    public byte DiffiColor1;
    public byte DiffiColor2;
    private float MarkX;
    private float MarkY;
    private float QueX;

    /*private byte DiffiFade1;
    private byte DiffiFade2;
    private byte DiffiFade3;
    private byte DiffiColor1;
    private byte DiffiColor2;
    public float InfiDuration;
    private float InfiStart;
    private float InfiRecharge;
    private bool InfiCharged;
    private int InfiCycle;
    private bool first;
    private bool InfiFade;*/


    //Timer Variables
    private float StartTime;
    private float counter;
    private float Bcounter;
    private int TimeString;
    private string Sec;
    private string Min;
    private string Hour;
    private string seconds;
    private string minutes;
    private string hours;
    private bool SecO;
    private bool MinO;
    private bool HouO;
    private int mincount;
    private bool minStop;
    private int houcount;
    private bool houStop;

    

    //Loop Variables
    private int CurrentLoop;

    //Portal Variables
    public bool PortalOpen;


    public Ability charAbility;
    public Primary charPrimary;
    // Start is called before the first frame update
    void Start()
    {
        
        Loop.text = "Stage: " + 1;
        StartTime = Time.time;
        TimeString = 1;
        CurrentQue = 1;
        //Sets portal display to offline
        PortalOpen = false;
        SecO = true;
        MinO = true;
        minStop = false;
        DiffiColor1 = 255;
        
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeDisplay();
        
        AbilityRecharge();
        PrimaryRecharge();
    }

    //Function that determines ability recharge rate
    private void AbilityRecharge()
    {
        //Checks if ability is available to use
        if (charAbility.canUse == false)
        {
            //ability recharge timer
            Ability2recharge += Time.deltaTime;
        }
        //Checks if ability recharge timer is less than cooldown value
        if (Ability2recharge < charAbility.cooldown && !charAbility.canUse)
        {
            //Sets value for the recharge display animation
            Ability2.value = (Ability2recharge / charAbility.cooldown);
        }
        //Checks if ability recharge is over
        if(Ability2recharge > charAbility.cooldown || charAbility.canUse)
        {
            //Sets timer back to zero
            Ability2recharge = 0;
            //Sets value for the recharge display animation to zero
            Ability2.value = 0;
        }

    }
    //Function that determines ability recharge rate
    private void PrimaryRecharge()
    {
        if (!charPrimary.hasCoolDown)
        {
            Ability1recharge = 0;
            Ability1.value = 0;
        }
        else
        {
            //Checks if ability is available to use
            if (charPrimary.canUse == false)
            {
                //ability recharge timer
                Ability1recharge += Time.deltaTime;
            }
            //Checks if ability recharge timer is less than cooldown value
            if (Ability1recharge < charPrimary.cooldown && !charPrimary.canUse)
            {
                //Sets value for the recharge display animation
                Ability1.value = (Ability1recharge / charPrimary.cooldown);
            }
            //Checks if ability recharge is over
            if (Ability1recharge > charPrimary.cooldown || charPrimary.canUse)
            {
                //Sets timer back to zero
                Ability1recharge = 0;
                //Sets value for the recharge display animation to zero
                Ability1.value = 0;
            }
        }
    }


    //Function that determines the appearence of the timer display
    private void TimeDisplay()
    {
        //The counter that the difficulty slider and timer are based on
        counter = levelManager.runTimer;

        //Controls the second numbers
        seconds = (counter % 60).ToString("f0");
        
        

        //Display for when the timer hasn't reached 1 minute
        if (TimeString == 1)
        {
            //Disables the tenth digit zero once the timer reaches 10 seconds
            if (seconds == "10")
            {
                SecO = false;
            }
            //Displays timer with the tenth digit zero
            if (SecO == true)
            {
                Timer.text = "00:00:0" + seconds;
            }
            //Displays timer without the tenth digit zero
            else
            {
                Timer.text = "00:00:" + seconds;
            }
            
            //Triggers the timer display to change
            if (seconds == "60")
            {
                TimeString = 2;
            }
        }
        //Display for when the timer hasn't reached 1 hour
        if (TimeString == 2)
        {
            //Disables the tenth digit zero once the timer reaches 10 seconds
            if (seconds == "10")
            {
                SecO = false;
            }
            //Enables the tenth digit zero once the timer reaches 60 seconds
            if (seconds == "60")
            {
                SecO = true;
                //prevents the 1:00 from being displayed as 1:60
                seconds = "0";
                //increases the minute section of the timer
                if (minStop == false)
                {
                    mincount += 1;
                    minStop = true;
                }
            }
            else
            {
                minStop = false;
            }
            //Disables the thousandth zero once the timer reaches 10 minutes
            if (minutes == "10")
            {
                MinO = false;
            }
            //Converts minCount into a string variable to be displayed in the minutes section
            minutes = mincount.ToString();
            //Checks if Minutes and Seconds equal zero
            if (SecO == true && MinO ==true)
            {
                Timer.text = "00:0" + minutes + ":" + "0" + seconds;
            }
            else
            {
                //Checks if minutes equal zero while seconds doesn't equal zero
                if (SecO == false && MinO == true)
                {
                    Timer.text = "00:0" + minutes + ":" + seconds;
                }
                //Checks if seconds equal zero while minutes doesn't equal zero
                if (SecO == true && MinO == false)
                {
                    Timer.text = "00:" + minutes + ":" + "0" + seconds;
                }
                //Checks if neither equal zero
                if (SecO == false && MinO == false)
                {
                    Timer.text = "00:" + minutes + ":" + seconds;
                }
            }
            
            //Triggers the timer display to change
            if (minutes == "60")
            {   
                TimeString = 3;
            }
        }
        //Displayer for when the timer has reached 1 hour
        if (TimeString == 3)
        {
            //Checks if seconds equal 10
            if (seconds == "10")
            {
                SecO = false;
            }
            //Checks if seconds equal 60
            if (seconds == "60")
            {
                SecO = true;
                seconds = "0";
                //Increases minute count by 1
                if (minStop == false)
                {
                    mincount += 1;
                    minStop = true;
                }
            }
            else
            {
               
                minStop = false;
            }
            //Checks if minutes equal 10
            if (minutes == "10")
            {
                MinO = false;
            }
            //Checks if minutes equal 60
            if (minutes == "60")
            {
                SecO = true;
                //Increases hour count by 1
                if (houStop == false)
                {
                    houcount += 1;
                    houStop = true;
                }
            }
            else
            {
                houStop = false;
            }
            //Converts mincount into string
            minutes = mincount.ToString();
            //Converts houcount into string
            hours = houcount.ToString();
            //Displays timer text
            Timer.text = hours + ":" + minutes + ":" + seconds;
            
            //Checks if all time bools equal zero 
            if (HouO == true && SecO == true && MinO == true)
            {
                Timer.text = "0" + hours + ":" + "0" + minutes + ":" + "0" + seconds;
            }
            else
            {
                //Checks if only the hour value equals zero
                if (HouO == true && SecO == false && MinO == false)
                {
                    Timer.text = "0" + hours + ":" + minutes + ":" + seconds;
                }
                //Checks if only the seconds value doesn't equal zero
                if (HouO == true && SecO == false && MinO == true)
                {
                    Timer.text = "0" + hours + ":" + minutes + ":" + "0" + seconds;
                }
                //Checks if only the minutes value doesn't equal zero
                if (HouO == true && SecO == true && MinO == false)
                {
                    Timer.text = "0" + hours + ":" + "0" + minutes + ":" + seconds;
                }
                //Checks if all time bools don't equal zero
                if (HouO == false && SecO == false && MinO == false)
                {
                    Timer.text = hours + ":" + minutes + ":" + seconds;
                }
                //Checks if only the seconds value equal zero
                if (HouO == false && SecO == true && MinO == false)
                {
                    Timer.text = hours + ":" + "0" + minutes + ":" + "0" + seconds;
                }
                //Checks if only the minutes value equal zero
                if (HouO == false && SecO == false && MinO == true)
                {
                    Timer.text = hours + ":" + minutes + ":" + "0" + seconds;
                }
            }
        }
    }

    //Spawns new slide
    public void NextSlide()
    {
        //CurrentQue += 1;
        if (ColorQue >= 3)
        {
            if (DiffiColor1 >= 20)
            {
                DiffiColor1 -= 25;
            }
            else
            {
                DiffiColor1 = 0;
            }
        }
        
        if (DiffiColor1 <= 140)
        {
            DiffiColor2 = 255;
        }
        else
        {
            DiffiColor2 = 0;
        }
        //Spawns new slide
        var newSlide = Instantiate(DifficultySlide, SlideSpawnPoint, Quaternion.identity);
        //Determines the new slide's transform
        newSlide.transform.parent = gameObject.transform;
    }

    //Displays how many fuel cells the player has collected
    public void FuelCellDisplay(int currentCells)
    {
        fuelCellsPicked.text = currentCells + "/4";

    }



    
    //Function that switches the portal display
    public void PortalDisplaySwitch()
    {
        PortalOpen = !PortalOpen;

        if (PortalOpen == false)
        {
            
            Portal.text = "Portal Offline";
            PortColor.GetComponent<Image>().color = new Color32(50, 50, 50, 255);
        }
        else
        {
            Portal.text = "Portal Online";
            PortColor.GetComponent<Image>().color = new Color32(0, 0, 250, 255);
        }
    }


    //finds the player's secondary ability
   public void GetPlayerAbility(GameObject playerAbility)
    {
        charAbility = playerAbility.GetComponent<Ability>();
        if(charAbility == null)
        {
            charAbility = playerAbility.GetComponentInChildren<Ability>();

        }

    }


    //updates stage text
    public void stageUpdate(int stageNum)
    {
        Loop.text = "Stage: " + stageNum;

    }

    //finds the players primary ability
    public void GetPlayerPrimary(GameObject playerPrimary)
    {
        charPrimary = playerPrimary.GetComponent<Primary>();
        if (charPrimary == null)
        {
            charPrimary = playerPrimary.GetComponentInChildren<Primary>();
            if(charPrimary == null)
            {
                GameObject WasteOfProcessingPower;
                WasteOfProcessingPower = GameObject.Find("CM vcam1");

                charPrimary = WasteOfProcessingPower.GetComponentInChildren<Primary>();
            }



        }

    }
}
