using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int SelectedCharacter = 1;

    public float Time_Alive = 0;
    public int EndRunPoints = 0;
    public float Damage_Dealt = 0;
    public int Enemies_Killed = 0;
    public int Highest_Level = 0;
    public int Stages_Completed = 0;
    public int Augments_Collected = 0;
    public int Difficulty = 1;
    public int Normal_Augments = 0;
    public int Uncommon_Augments = 0;
    public int Rare_Augments = 0;
    public int Epic_Augments = 0;
    public int HighScore = 0;

    public bool TutorialCompletion = false;

    //Collects data from the SkillTree script
    public PlayerData(SkillTree character)
    {
        //SelectedCharacter = character.SelectedCharacter;
    }

    //Collects data from the ScoreSystem script
    public PlayerData(ScoreSystem End)
    {
        EndRunPoints = End.EndRunPoints;
    }

    //Collects data from the LevelManager script
    public PlayerData(LevelManager Character)
    {
        Time_Alive = Character.runTimer;
        Stages_Completed = Character.totalLevelCounter;
        Damage_Dealt = Character.totalDamage;
        Enemies_Killed = Character.totalEnemies;
    }

   
    //Collects data from the PlayerStats script
    public PlayerData(PlayerStats Character)
    {
        Highest_Level = Character.playerLevel;
    }


    //Collects data from the PlayerStats script
    public PlayerData(TutorialNotifications Character)
    {
        TutorialCompletion = Character.TutorialCompletion;
    }


}
