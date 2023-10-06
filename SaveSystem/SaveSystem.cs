using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

//Filepath for save files is /AppData/LocalLow/DefaultCompany/Trapped 1_2/


public static class SaveSystem 
{

    //Function that saves the rogue's character skill tree
    public static void SaveRogueCharacter (SkillTree skillrank)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.rogue";
        FileStream stream = new FileStream(path, FileMode.Create);

        RogueCharacterData data = new RogueCharacterData(skillrank);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Function that loads the rogues character skill tree
    public static RogueCharacterData LoadRogueCharacter()
    {
        string path = Application.persistentDataPath + "/player.rogue";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            RogueCharacterData data = formatter.Deserialize(stream) as RogueCharacterData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);

            return null;
        }
    }


    //Function that saves the croc's character skill tree
    public static void SavePoisonCharacter(SkillTree skillrank)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PoisonCharacterData data = new PoisonCharacterData(skillrank);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Function that loads the croc's character skill tree
    public static PoisonCharacterData LoadPoisonCharacter()
    {
        string path = Application.persistentDataPath + "/player.fun";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PoisonCharacterData data = formatter.Deserialize(stream) as PoisonCharacterData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

    //Function that saves the main gunner's character skill tree
    public static void SaveMainCharacter(SkillTree skillrank)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.main";
        FileStream stream = new FileStream(path, FileMode.Create);

        MainCharacterData data = new MainCharacterData(skillrank);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Function that loads the main gunner's character skill tree
    public static MainCharacterData LoadMainCharacter()
    {
        string path = Application.persistentDataPath + "/player.main";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MainCharacterData data = formatter.Deserialize(stream) as MainCharacterData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            
            return null;
        }
    }

    //Function that saves the rocket character's skill tree
    public static void SaveRocketCharacter(SkillTree skillrank)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.rocket";
        FileStream stream = new FileStream(path, FileMode.Create);

        RocketCharacterData data = new RocketCharacterData(skillrank);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Function that loads the rocket character's skill tree
    public static RocketCharacterData LoadRocketCharacter()
    {
        string path = Application.persistentDataPath + "/player.rocket";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            RocketCharacterData data = formatter.Deserialize(stream) as RocketCharacterData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }




    //Function that saves what character the player has selected
    public static void SaveSelectedCharacter(SkillTree character)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.general";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(character);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Function that loads what character the player has selected
    public static PlayerData LoadSelectedCharacter()
    {
        string path = Application.persistentDataPath + "/player.general";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

    //Function that saves the endrun points
    public static void SaveEndRunSupplyCharacter(ScoreSystem End)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.end";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(End);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Function that loads the endrun points
    public static PlayerData LoadEndRunSupplyCharacter()
    {
        string path = Application.persistentDataPath + "/player.end";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

    //Function that saves player data from the levelmanager script
    public static void SaveLevelManagerPlayer(LevelManager character)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.levelstats";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(character);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Function that loads player data from the levelmanager script
    public static PlayerData LoadLevelManagerPlayer()
    {
        string path = Application.persistentDataPath + "/player.levelstats";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

    

    //Function that loads player data from the enemymaster script
    public static PlayerData LoadEnemyMasterPlayer()
    {
        string path = Application.persistentDataPath + "/player.masterstats";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

    //Function that saves player data from the playerstats script
    public static void SavePlayerStats(PlayerStats character)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.playerstats";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(character);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Function that loads player data from the playerstats script
    public static PlayerData LoadPlayerStats()
    {
        string path = Application.persistentDataPath + "/player.playerstats";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }


    //Function that saves tutorial status
    public static void SaveTutorialStatus(TutorialNotifications character)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.tutorial";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(character);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Function that loads tutorial status
    public static PlayerData LoadTutorialStatus()
    {
        string path = Application.persistentDataPath + "/player.tutorial";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

    //Function that saves Achievment data from the TemporaryTracker script
    public static void SavePickUpAchievmentProgress(InteractRaycast Tracker)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Achievments";
        FileStream stream = new FileStream(path, FileMode.Create);

        AchievementData data = new AchievementData(Tracker);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Function that loads Achievment data from the TemporaryTracker script
    public static AchievementData LoadPickUpAchievmentProgress()
    {
        string path = Application.persistentDataPath + "/Achievments";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            AchievementData data = formatter.Deserialize(stream) as AchievementData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

    //Function that saves audio data from the mixercontrol script
    public static void SaveAudioData(MixerControl vol)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Audio";
        FileStream stream = new FileStream(path, FileMode.Create);

        GraphicsData data = new GraphicsData(vol);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Function that loads audi data from the mixercontrol script
    public static GraphicsData LoadAudioData()
    {
        string path = Application.persistentDataPath + "/Audio";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GraphicsData data = formatter.Deserialize(stream) as GraphicsData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

    //Deletes all save files
    public static void SeriouslyDeleteAllSaveFiles()
    {
        string[] Save = new string[11];
        Save[0] = Application.persistentDataPath + "/Achievments";
        Save[1] = Application.persistentDataPath + "/player.tutorial";
        Save[2] = Application.persistentDataPath + "/player.playerstats";
        Save[3] = Application.persistentDataPath + "/player.masterstats";
        Save[4] = Application.persistentDataPath + "/player.levelstats";
        Save[5] = Application.persistentDataPath + "/player.end";
        Save[6] = Application.persistentDataPath + "/player.general";
        Save[7] = Application.persistentDataPath + "/player.rocket";
        Save[8] = Application.persistentDataPath + "/player.main";
        Save[9] = Application.persistentDataPath + "/player.fun";
        Save[10] = Application.persistentDataPath + "/player.rogue";

        

         
        for (var i = 0; i < Save.Length; i++)
        {
            string path = Save[i];
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                Debug.LogError("Save File not found in " + path);
            }
        }

        
        

    }
}
