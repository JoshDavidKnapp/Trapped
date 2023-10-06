using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Temporary Script to test out Achievment Tracking

public class TemporaryAchievmentTracker : MonoBehaviour
{
    public bool[] Progress;

    public bool Save;

    private void Update()
    {
        if (Save == true)
        {
            //SaveSystem.SaveAchievmentProgress(this);
        }
    }
}
