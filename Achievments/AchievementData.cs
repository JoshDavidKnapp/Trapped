using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AchievementData
{
    public bool[] AchievmentProgress = new bool[20];
    
    /*Achievemnts
     * 0 = Magnetic Shielding
     * 1 = Energy Conservation
     * 2 = Weapon Overcharge
     * 3 = Regen Overcharge
     * 4 = Weapon Display
     * 5 = Safe Recovery
     * 6 = Life Drain
     * 7 = Double Jump
     * 8 = Weapon Repeats
     * 9 = Critical Response
     * 10 = Spike Explosion
     * 11 = Damage Learning
     * 12 = Power Shielding
     * 13 = Power Oscillation
     * 14 = Energy Overload
     * 15 = Molecular Shielding
     * 16 = God's Wrath
     * 17 = To be filled in later
     * 18 = To be filled in later
     * 19 = To be filled in later
     */
     
    public AchievementData(InteractRaycast Tracker)
    {
        for (var i = 0; i < AchievmentProgress.Length; i++)
        {
           
           AchievmentProgress[i] = Tracker.AugUnlocked[i];
        }
    }
}
