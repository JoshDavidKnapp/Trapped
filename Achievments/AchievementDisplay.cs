using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementDisplay : MonoBehaviour
{
    public GameObject[] UnlockedAchievments;
    public GameObject[] LockedAchievments;
    private bool[] AchievmentProgress;
    private bool Checked;
    // Start is called before the first frame update
    void Start()
    {
        //Automatically sets up length for AchievmentProgress without having to edit it in the inspector
        AchievmentProgress = new bool[UnlockedAchievments.Length];
        //Checks for the current status of AchievmentProgress
        StatusCheck();
        Checked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Checked == false)
        {
            //Updates Status of Achievments
            StatusUpdate();
            Checked = true;
        }
        
    }

    //Function that updates Status of Achievments
    private void StatusUpdate()
    {
        //Goes through each Achievment
        for (var i = 0; i < AchievmentProgress.Length; i++)
        {
            //If the requirement for the achivement has not been completed, sets locked image active
            if (AchievmentProgress[i] == false)
            {
                UnlockedAchievments[i].SetActive(false);
                LockedAchievments[i].SetActive(true);
            }
            //If the requirement for the achivement has been completed, sets unlocked image active
            else
            {
                UnlockedAchievments[i].SetActive(true);
                LockedAchievments[i].SetActive(false);
            }

        }
    }

    //Function that checks for the current status of AchievmentProgress
    private void StatusCheck()
    {
        //Checks if there is a save file to load
        if (SaveSystem.LoadPickUpAchievmentProgress() != null)
        {
            print("Something to Load");
            //Sets all AchievmentProgress to true/false based on what Achievments have been completed based on the save file
            for (var i = 0; i < UnlockedAchievments.Length; i++)
            {
                AchievementData data = SaveSystem.LoadPickUpAchievmentProgress();
                AchievmentProgress[i] = data.AchievmentProgress[i];

            }
            
        }
        else
        {
            print("Nothing to Load");
            //Sets all AchievmentProgress to false if there is no save file to load
            for (var i = 0; i < UnlockedAchievments.Length; i++)
            {
                AchievmentProgress[i] = false;
            }

        }
        
    }
}
