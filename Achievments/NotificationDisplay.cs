using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationDisplay : MonoBehaviour
{
    public Image[] Background;
    public Image[] Icon;
    public Text[] Title;
    public Text[] Description;

    private float Opacity;
    public float TotalDuration;
    public float FadeDuration;
    private float FadeValue;
    private float Linger;
    private bool Fading;

    public int AcievementUnlocked;
    public bool Display;
    private bool SomethingInQue;
    private int StorageSize;
    private int[] StoredNumber = new int[4];
    // Start is called before the first frame update
    void Start()
    {
        Opacity = 0;
        FadeValue = 0;
        Linger = 0;
        StorageSize = -1;
        TotalDuration = TotalDuration - (FadeDuration * 2);

        //Sets all displays' opacities to zero
        for(var i = 0; i < Background.Length; i++)
        {
            //Changes opacity for the background
            var TempBackColor = Background[i].color;
            TempBackColor.a = 0;
            Background[i].color = TempBackColor;

            //Changes opacity for the icon
            var TempIconColor = Icon[i].color;
            TempIconColor.a = 0;
            Icon[i].color = TempIconColor;

            //Changes opacity for the title
            var TempTitleColor = Title[i].color;
            TempTitleColor.a = 0;
            Title[i].color = TempTitleColor;

            //Changes opacity for the description
            var TempDescColor = Description[i].color;
            TempDescColor.a = 0;
            Description[i].color = TempDescColor;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Checks if something is supposed to be displayed
        if (Display == true)
        {
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
                    //Ends the display cycle
                    Display = false;
                    Fading = false;

                    //Checks if something is in que
                    if (SomethingInQue == true)
                    {
                        //Resets the display cycle if something is in que
                        AcievementUnlocked = StoredNumber[0];
                        
                        for (var i = 0; i < StorageSize; i++)
                        {
                            StoredNumber[i] = StoredNumber[i + 1];
                        }
                        StorageSize -= 1;
                        Display = true;
                        print("Display Stored Number");

                        //Checks if there is no longer anything in que
                        if (StorageSize == -1)
                        {
                            //Ends que cycle
                            SomethingInQue = false;
                        }
                        
                    }
                }
            }
        }
    }

   
    //Public function that other scripts reference to activate the display for a specific achievement notification
    public void UnlockAchievement(int Set)
    {
        
        if(Display == false)
        {
            AcievementUnlocked = Set;
            Display = true;
        }
        else
        {
            StorageSize += 1;
            StoredNumber[StorageSize] = Set;
            SomethingInQue = true;
        }
        
    }


    //Fades the notification display out
    private void FadeOut()
    {
        
        FadeValue = (Time.deltaTime / FadeDuration);

        Opacity -= FadeValue;

        OpacityChange();
    }

    //Fades the notification display in
    private void FadeIn()
    {
        FadeValue += (Time.deltaTime / FadeDuration);
        
        Opacity = FadeValue;

        OpacityChange();
    }

    //Changes the opacity for the notification display
    private void OpacityChange()
    {
        print("Achievement Loaded is " + AcievementUnlocked);
        if (AcievementUnlocked < 20)
        {
            //Changes opacity for the icon
            var TempIconColor = Icon[AcievementUnlocked].color;
            TempIconColor.a = Opacity;
            Icon[AcievementUnlocked].color = TempIconColor;

            //Changes opacity for the background
            var TempBackColor = Background[AcievementUnlocked].color;
            TempBackColor.a = Opacity;
            Background[AcievementUnlocked].color = TempBackColor;

            //Changes opacity for the title
            var TempTitleColor = Title[AcievementUnlocked].color;
            TempTitleColor.a = Opacity;
            Title[AcievementUnlocked].color = TempTitleColor;

            //Changes opacity for the description
            var TempDescColor = Description[AcievementUnlocked].color;
            TempDescColor.a = Opacity;
            Description[AcievementUnlocked].color = TempDescColor;

            //Description

            //Changes opacity for the background
            var TempBack2Color = Background[AcievementUnlocked + 20].color;
            TempBackColor.a = Opacity;
            Background[AcievementUnlocked + 20].color = TempBackColor;

            //Changes opacity for the title
            var TempTitle2Color = Title[AcievementUnlocked + 20].color;
            TempTitleColor.a = Opacity;
            Title[AcievementUnlocked + 20].color = TempTitleColor;

            //Changes opacity for the description
            var TempDesc2Color = Description[AcievementUnlocked +20].color;
            TempDescColor.a = Opacity;
            Description[AcievementUnlocked + 20].color = TempDescColor;


            //print("Achievement Unlocked " + AcievementUnlocked + " , Augment Picked Up " + (AcievementUnlocked + 20));
        }
        else
        {
            //Changes opacity for the background
            var TempBackColor = Background[AcievementUnlocked].color;
            TempBackColor.a = Opacity;
            Background[AcievementUnlocked].color = TempBackColor;

            //Changes opacity for the title
            var TempTitleColor = Title[AcievementUnlocked].color;
            TempTitleColor.a = Opacity;
            Title[AcievementUnlocked].color = TempTitleColor;

            //Changes opacity for the description
            var TempDescColor = Description[AcievementUnlocked].color;
            TempDescColor.a = Opacity;
            Description[AcievementUnlocked].color = TempDescColor;

            //print("Augment is " + AcievementUnlocked);
        }
        
    }
}
