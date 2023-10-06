using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySlide : MonoBehaviour
{
    public GameObject Player;
    public Image SlidePrefab;
    public Text SlideText;
    private float Timer;
    private float Pretimer;
    public float TotalDistance;
    public int SpeedPhase;
    private float speed;
    private float t;
    private bool HalfTime;
    private bool NextTime;
    private byte DiffiFade;
    private byte R;
    private byte B;
    private byte G;
    private float FadeTime;
    public bool ShowLocation;
    private float Adjustspeed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Sets oppacity to full at start
        DiffiFade = 255;

        //Links to Player gameObject
        Player = GameObject.FindGameObjectWithTag("Hud");

        //Sets Slide to easy
        if (Player.GetComponent<HudDisplay>().ColorQue == 0)
        {
            R = 0;
            B = 0;
            G = 255;
            SlideText.text = "Easy";
        }

        //Sets Slide to normal
        if (Player.GetComponent<HudDisplay>().ColorQue == 1)
        {
            DiffiFade = 0;
            R = 255;
            B = 0;
            G = 255;
            SlideText.text = "Normal";
        }

        //Sets Slide to hard
        if (Player.GetComponent<HudDisplay>().ColorQue >= 2)
        {
            DiffiFade = 0;
            R = Player.GetComponent<HudDisplay>().DiffiColor1;
            B = 0;
            G = 0;
            SlideText.text = "Hard";
        }

        //Checks to see if the game is on easy difficulty
        if (Player.GetComponent<HudDisplay>().CurrentQue == 1)
        {
            Pretimer = Player.GetComponent<HudDisplay>().NormalQue;
            Timer = Player.GetComponent<HudDisplay>().NormalQue;
            
        }

        //Checks to see if the game is on normal difficulty
        if (Player.GetComponent<HudDisplay>().CurrentQue == 2)
        {
            Pretimer = Player.GetComponent<HudDisplay>().HardQue;
        }

        //Checks to see if the game is on hard difficulty
        if (Player.GetComponent<HudDisplay>().CurrentQue >= 3)
        {
            Pretimer = Player.GetComponent<HudDisplay>().InfiniQue;
        }
        NextTime = false;
        HalfTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the opacity of the slide color
        SlidePrefab.color = new Color32(R, G, B, DiffiFade);

        //Updates the opacity of the slide text
        SlideText.color = new Color32(Player.GetComponent<HudDisplay>().DiffiColor2, 0, 0, DiffiFade);

        //Checks if difficulty slider has moved into hard level
        if (Player.GetComponent<HudDisplay>().CurrentQue == 2)
        {
            
            Timer = Player.GetComponent<HudDisplay>().HardQue;
            if (NextTime == false)
            {
                NextTime = true;
            }

        }

        //Checks if difficulty slider has moved into the infinite cycle levels
        if (Player.GetComponent<HudDisplay>().CurrentQue >= 3)
        {
            Timer = Player.GetComponent<HudDisplay>().InfiniQue;
        }

        //Updates time value
        t += Time.deltaTime;

        //Speed of slide when it spawns
        if (SpeedPhase == 1)
        {
            //If Opacity isn't at maximum value
            if (DiffiFade <= 244)
            {
                //Increases the Opacity Value
                DiffiFade += 1;
            }
            else
            {

                //Sets Opacity to the maximum value
                DiffiFade = 255;
            }
           
            //Determines location modifier
            speed = (t / (Pretimer / 2)) * (460 - 415);

            //Determines when the slider should change speed phase 
            if (speed >= (460 - 415))
            {
                t = 0;
                SpeedPhase = 2;
            }

            //Determines location
            transform.localPosition = new Vector3(460 - speed, 190, 0);
        }

        
        //Speed of slide when it reaches the time indicator
        if (SpeedPhase == 2)
        {
            //Determines location modifier
            speed = (t / Timer) * (415 - 325);


            //Determines when the slider should spawn the next slide
            if (speed >= ((415 - 325)/2) && HalfTime == false)
            {
                Player.GetComponent<HudDisplay>().ColorQue += 1;
                Player.GetComponent<HudDisplay>().NextSlide();
                HalfTime = true;
                
            }
            
            //Determines when the slider should change speed phase 
            if (speed >= (415 - 325))
            {
                t = 0;
                SpeedPhase = 3;
                NextTime = false;
                Player.GetComponent<HudDisplay>().CurrentQue += 1;
                
            }

            //Determines location
            transform.localPosition = new Vector3(415 - speed, 190, 0);
        }

        //Speed of slide when it leaves the time indicator
        if (SpeedPhase == 3)
        {
            //Determines location modifier
            speed = (t / (Timer / 2)) * (325-280);
            FadeTime = Timer / 2;
            if (speed >= 40)
            {
                //If Opacity isn't at lowest value
                if (DiffiFade >= 2)
                {
                    //Decreases opacity value
                    DiffiFade -= 1;
                }
                else
                {
                    //Sets opacity value to 0
                    DiffiFade = 0;
                }
            }

            //Checks if Slide has reached its final destination
            if (speed >= (345 - 280))
            {
                //Destroys Slide
                Destroy(gameObject);
            }

            //Determines location
            transform.localPosition = new Vector3(325 - speed, 190, 0);
        }
        
        //Checks if Slide has reached its final destination
        if (Adjustspeed >= (TotalDistance-speed))
        {
            //Destroys Slide
            Destroy(gameObject);
        }
    }
}
