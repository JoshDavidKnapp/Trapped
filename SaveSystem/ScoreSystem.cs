using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public Text DifficultyInfo;
    public Text DifficultyScore;
    public Text TimeAliveInfo;
    public Text TimeAliveScore;
    public Text KillsInfo;
    public Text KillsScore;
    public Text StagesCompletedInfo;
    public Text StagesCompletedScore;
    public Text DamageDealtInfo;
    public Text DamageDealtScore;
    public Text HighestLevelInfo;
    public Text HighestLevelScore;
    public Text NormalItemsInfo;
    public Text NormalItemsScore;
    public Text UncommonItemsInfo;
    public Text UncommonItemsScore;
    public Text RareItemsInfo;
    public Text RareItemsScore;
    public Text EpicItemsInfo;
    public Text EpicItemsScore;
    public Text TotalScore;

    //Public values for the stat values
    public int DiffiBase;
    public int DiffiLevel;
    private int TA;
    private int KD;
    private int SC;
    private int DD;
    private int HL;
    public int NI;
    public int UI;
    public int RI;
    public int EI;
    
    //Score values for the stat values
    private int DiffiScore;
    private int TAS;
    private int KDS;
    private int SCS;
    private int DDS;
    private int TS;
    private int HLS;
    private int NIS;
    private int UIS;
    private int RIS;
    private int EIS;

    //Score modifier values for the stat valus
    public int Diffi_Mod;
    public int TA_Mod;
    public int KD_Mod;
    public int SC_Mod;
    public int DD_Mod;
    public int HL_Mod;
    public int NI_Mod;
    public int UI_Mod;
    public int RI_Mod;
    public int EI_Mod;


    public int EndRunPoints;

    // Start is called before the first frame update
    void Start()
    {
        LoadLevelManagerData();
        LoadPlayerStatsData();
       
    }

    // Update is called once per frame
    void Update()
    {
        DifficultyDisplay();
        TimeAliveDisplay();
        KillsDisplay();
        StagesCompletedDisplay();
        DamageDealtDisplay();
        HighestLevelDisplay();
        NormalItemsDisplay();
        UncommonItemsDisplay();
        RareItemsDisplay();
        EpicItemsDisplay();
        TotalScoreDisplay();
        GenerateEndRunPoints();
    }

    //Function that Loads PlayerStats Data
    private void LoadPlayerStatsData()
    {
        PlayerData data = SaveSystem.LoadPlayerStats();
        HL = data.Highest_Level;
    }
    //Function that Loads LevelManager Data
    private void LoadLevelManagerData()
    {
        PlayerData data = SaveSystem.LoadLevelManagerPlayer();
        TA = System.Convert.ToInt32(data.Time_Alive/60);
        SC = data.Stages_Completed - 1;
        DD = System.Convert.ToInt32(data.Damage_Dealt);
        KD = data.Enemies_Killed;
    }


    //Text display function for the difficulty texts
    private void DifficultyDisplay()
    {
        //Checks to see if the Base Difficulty Rank is set to Normal
        if (DiffiBase == 1)
        {
            //Displays base difficulty rank and the difficulty level modifier
            DifficultyInfo.text = "Difficulty: Normal(" + DiffiLevel + ")";
            //Calculates score
            DiffiScore = DiffiBase * DiffiLevel * Diffi_Mod;
            //Displays Score
            DifficultyScore.text = DiffiScore + "pts";
        }
    }

    //Text display function for the time alive texts
    private void TimeAliveDisplay()
    {
        //Checks to see if TA equals 1
        if (TA == 1)
        {
            //Displays Time Alive text with a non-pulral minute
            TimeAliveInfo.text = "Time Alive: " + TA + " minute";
        }
        else
        {
            //Displays Time Alive text with a pulral minute
            TimeAliveInfo.text = "Time Alive: " + TA + " minutes";

        }
        //Calculates score
        TAS = TA * TA_Mod;
        //Displays Score
        TimeAliveScore.text = TAS + "pts";
    }

    //Text display function for the kills texts
    private void KillsDisplay()
    {
        //Displays Kills text
        KillsInfo.text = "Kills: " + KD;
        //Calculates score
        KDS = KD * KD_Mod;
        //Displays Score
        KillsScore.text = KDS + "pts";
    }

    //Text display function for the stages complete texts
    private void StagesCompletedDisplay()
    {
        //Displays Stages Completed text
        StagesCompletedInfo.text = "Stages Completed: " + SC;
        //Calculates score
        SCS = SC * SC_Mod;
        //Displays Score
        StagesCompletedScore.text = SCS + "pts";
    }

    //Text display function for the damage dealt texts
    private void DamageDealtDisplay()
    {
        //Displays Damage Dealt text
        DamageDealtInfo.text = "Damage Dealt: " + DD;
        //Calculates score
        DDS = DD * DD_Mod;
        //Displays Score
        DamageDealtScore.text = DDS + "pts";
    }

    //Text display function for the highest level texts
    private void HighestLevelDisplay()
    {
        //Displays Highest Level text
        HighestLevelInfo.text = "Highest Level: " + HL;
        //Calculates score
        HLS = HL * HL_Mod;
        //Displays Score
        HighestLevelScore.text = HLS + "pts";
    }

    //Text display function for the normal items collected texts
    private void NormalItemsDisplay()
    {
        //Displays Normal Items text
        NormalItemsInfo.text = "Normal Augments Collected: " + NI;
        //Calculates score
        NIS = NI * NI_Mod;
        //Displays Score
        NormalItemsScore.text = NIS + "pts";
    }

    //Text display function for the uncommon items collected texts
    private void UncommonItemsDisplay()
    {
        //Displays Uncommon Items text
        UncommonItemsInfo.text = "Uncommon Augments Collected: " + UI;
        //Calculates score
        UIS = UI * UI_Mod;
        //Displays Score
        UncommonItemsScore.text = UIS + "pts";
    }

    //Text display function for the rare items collected texts
    private void RareItemsDisplay()
    {
        //Displays Rare Items text
        RareItemsInfo.text = "Rare Augments Collected: " + RI;
        //Calculates score
        RIS = RI * RI_Mod;
        //Displays Score
        RareItemsScore.text = RIS + "pts";
    }

    //Text display function for the epic items collected texts
    private void EpicItemsDisplay()
    {
        //Displays Epic Items text
        EpicItemsInfo.text = "Epic Augments Collected: " + EI;
        //Calculates score
        EIS = EI * EI_Mod;
        //Displays Score
        EpicItemsScore.text = EIS + "pts";
    }

    //Text display function for the total score texts
    private void TotalScoreDisplay()
    {
        //Calculates Total score
        TS = DiffiScore + TAS + KDS + SCS + DDS + HLS + NIS + UIS + RIS + EIS;
        //Displays Total Score
        TotalScore.text = "Score: " + TS + "pts";
    }

    //Function that generates end run points
    private void GenerateEndRunPoints()
    {
        //Calculates EndRunPoints
        EndRunPoints = TA * (DiffiBase * 5);
        //Saves EndRunPoints
        SaveSystem.SaveEndRunSupplyCharacter(this);
    }
}
