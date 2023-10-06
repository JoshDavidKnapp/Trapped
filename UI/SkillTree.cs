using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    //dictates the player character type
    public CharType charType;

    public Text[] Skills;
    public Image[] SkillBackgrounds;
    public GameObject[] Characters;
    public Text EndRunBank;
    public Text EndRunWarning;
   
    //0 = Rogue, 1 = Poison, 2 = main gunner, 3 = rocket soldier

    public GameObject[] DiffiTabs;
    public int[] MaxSkillPoints;

    public int[] SkillPoints;
    public int EndRunPoints;

    public int TierTwoRequirement;
    public int TierThreeRequirement;
    public int TierFourRequirement;
    public int TierFiveRequirement;

    public int TierOneBaseCost;
    public int TierTwoBaseCost;
    public int TierThreeBaseCost;
    public int TierFourBaseCost;
    public int TierFiveBaseCost;

    public int SkillButton;

    private bool NewTier;
    public int t;
    public int TierPoints;

    public bool CharacterScreen;
    public bool MainMenu;

    public bool FirstRun;
    // Start is called before the first frame update
    void Start()
    {
        if (!CharacterScreen)
        {
            charType = GameObject.Find("CharSelecter").GetComponent<SceneManage>().charType;
            EndRunPoints = (int)((GameObject.Find("CharSelecter").GetComponent<SceneManage>().totalTime/60)*5);
        }

        
            //checks if data is exsisting before loading, if not save empty data
            switch (charType)
            {
                case CharType.MPGuard:
                    if (SaveSystem.LoadRogueCharacter() != null)
                        LoadCharacter();
                    else
                        SaveCharacter();
                    break;
                case CharType.croc:
                    if (SaveSystem.LoadPoisonCharacter() != null)
                        LoadCharacter();
                    else
                        SaveCharacter();
                    break;
                case CharType.inmate001:
                    if (SaveSystem.LoadMainCharacter() != null)
                        LoadCharacter();
                    else
                        SaveCharacter();
                    break;
                case CharType.SLB:
                    if (SaveSystem.LoadRocketCharacter() != null)
                        LoadCharacter();
                    else
                        SaveCharacter();
                    break;
                default:
                    break;
            
           
            
        }

        //Checks if skill tree is active in either the character screen or main menu
        if (CharacterScreen == false && MainMenu == false)
        {
            //Loads saved data value for available endrun points
            PlayerData data = SaveSystem.LoadEndRunSupplyCharacter();
            //Makes local Endrun points equal to the endrun points in the save file
            EndRunPoints = data.EndRunPoints;
            //Displays EndRunPoints
            EndRunBank.text = "Data Fragments: " + EndRunPoints;
        }

        NewTier = true;

    }

    // Update is called once per frame
    void Update()
    {
        //Checks if skill tree is active in either the character screen or main menu
        if (CharacterScreen == false && MainMenu == false)
        {
            //Displays EndRunPoints
            EndRunBank.text = "Data Fragments: " + EndRunPoints;
            //Displays EndRunPoints in the warning message
            EndRunWarning.text = "You Only Have " + EndRunPoints + " Data Fragments Left";
        }

        //Checks if a new tier has been unlocked
        if (NewTier == true)
        {
            //Goes through each SkillBackground object and switches its color
            for (var i = 0; i < SkillBackgrounds.Length; i++)
            {
                if (i < t || i < 3)
                {
                    //Turns unlocked skill backgrounds blue
                    //SkillBackgrounds[i].color = new Color32(0, 0, 150, 255);

                   // print("I turned skill " + (i + 1) + " blue");
                }
                else
                {
                    //Turns locked skill backgrounds grey
                    //SkillBackgrounds[i].color = new Color32(100, 100, 100, 255);
                }
            }
            NewTier = false;
        }
        
    }


    public void SkillButtonCheck(int Skill)
    {
        SkillButton = Skill;
    }

    //Function that increases skill point value of a skill
    public void SkillIncrease()
    {
        //Checks if the selected skill is at its maximum value
        if (SkillPoints[SkillButton] < MaxSkillPoints[SkillButton])
        {
            //Checks if the selected skill is part of the first tier
            if(SkillButton < 3)
            {
                //Checks if skill is at its second tier
                if (SkillPoints[SkillButton] == 2)
                {
                    //Checks if player has enough endrun points
                    if (EndRunPoints >= TierOneBaseCost * 4)
                    {
                        //Increases skillpoint by 1
                        SkillPoints[SkillButton] += 1;
                        //Decreases amount of endrun points available
                        EndRunPoints -= TierOneBaseCost * 4;
                        //Increases the total tier-ranking
                        TierPoints += 1;
                    }
                }
                //Checks if skill is at its first tier
                if (SkillPoints[SkillButton] == 1)
                {
                    //Checks if player has enough endrun points
                    if (EndRunPoints >= TierOneBaseCost * 2)
                    {
                        //Increases skillpoint by 1
                        SkillPoints[SkillButton] += 1;
                        //Decreases amount of endrun points available
                        EndRunPoints -= TierOneBaseCost * 2;
                        //Increases the total tier-ranking
                        TierPoints += 1;
                    }
                }
                //Checks if skill has not been upgraded yet
                if (SkillPoints[SkillButton] == 0)
                {
                    //Checks if player has enough endrun points
                    if (EndRunPoints >= TierOneBaseCost)
                    {
                        //Increases skillpoint by 1
                        SkillPoints[SkillButton] += 1;
                        //Decreases amount of endrun points available
                        EndRunPoints -= TierOneBaseCost;
                        //Increases the total tier-ranking
                        TierPoints += 1;
                    }
                }
                
                

            }
            else
            {
                //Checks if skillbutton is a part of the second tier of skills
                if (SkillButton < 5)
                {
                    //Checks if tier two skills have been unlocked
                    if (TierPoints >= TierTwoRequirement)
                    {
                        //Checks if skill on its first tier rank
                        if (SkillPoints[SkillButton] == 1)
                        {
                            //Checks if player has enough endrun points
                            if (EndRunPoints >= TierTwoBaseCost * 2)
                            {
                                //Increases skillpoint by 1
                                SkillPoints[SkillButton] += 1;
                                //Decreases amount of endrun points available
                                EndRunPoints -= TierTwoBaseCost * 2;
                                //Increases the total tier-ranking
                                TierPoints += 1;
                            }
                        }
                        //Checks if skill has yet to be upgraded
                        if (SkillPoints[SkillButton] == 0)
                        {
                            //Checks if player has enough endrun points
                            if (EndRunPoints >= TierTwoBaseCost)
                            {
                                //Increases skillpoint by 1
                                SkillPoints[SkillButton] += 1;
                                //Decreases amount of endrun points available
                                EndRunPoints -= TierTwoBaseCost;
                                //Increases the total tier-ranking
                                TierPoints += 1;
                            }
                        }
                    }
                    
                }
                else
                {
                    //Checks if skillbutton is a part of the third tier skills
                    if (SkillButton < 8)
                    {
                        //Checks if tier three skills have been unlocked
                        if (TierPoints >= TierThreeRequirement)
                        {
                            //Checks if skill is at its second tier rank
                            if (SkillPoints[SkillButton] == 2)
                            {
                                //Checks if player has enough endrun points
                                if (EndRunPoints >= TierThreeBaseCost * 4)
                                {
                                    //Increases skillpoint by 1
                                    SkillPoints[SkillButton] += 1;
                                    //Decreases amount of endrun points available
                                    EndRunPoints -= TierThreeBaseCost * 4;
                                    //Increases the total tier-ranking
                                    TierPoints += 1;
                                }
                            }
                            //Checks if skill is at its first tier rank
                            if (SkillPoints[SkillButton] == 1)
                            {
                                //Checks if player has enough endrun points
                                if (EndRunPoints >= TierThreeBaseCost * 2)
                                {
                                    //Increases skillpoint by 1
                                    SkillPoints[SkillButton] += 1;
                                    //Decreases amount of endrun points available
                                    EndRunPoints -= TierThreeBaseCost * 2;
                                    //Increases the total tier-ranking
                                    TierPoints += 1;
                                }
                            }
                            //Checks if skill has yet to be upgraded
                            if (SkillPoints[SkillButton] == 0)
                            {
                                //Checks if player has enough endrun points
                                if (EndRunPoints >= TierThreeBaseCost)
                                {
                                    //Increases skillpoint by 1
                                    SkillPoints[SkillButton] += 1;
                                    //Decreases amount of endrun points available
                                    EndRunPoints -= TierThreeBaseCost;
                                    //Increases the total tier-ranking
                                    TierPoints += 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        //Checks if skill button is a part of the fourth tier of skills
                        if (SkillButton < 10)
                        {
                            //Checks if tier four skills are unlocked
                            if (TierPoints >= TierFourRequirement)
                            {
                                //Checks if skill is at its first tier rank
                                if (SkillPoints[SkillButton] == 1)
                                {
                                    //Checks if player has enough endrun points
                                    if (EndRunPoints >= TierFourBaseCost * 2)
                                    {
                                        //Increases skillpoint by 1
                                        SkillPoints[SkillButton] += 1;
                                        //Decreases amount of endrun points available
                                        EndRunPoints -= TierFourBaseCost * 2;
                                        //Increases the total tier-ranking
                                        TierPoints += 1;
                                    }
                                }
                                //Checks if skill has yet to be upgraded
                                if (SkillPoints[SkillButton] == 0)
                                {
                                    //Checks if player has enough endrun points
                                    if (EndRunPoints >= TierFourBaseCost)
                                    {
                                        //Increases skillpoint by 1
                                        SkillPoints[SkillButton] += 1;
                                        //Decreases amount of endrun points available
                                        EndRunPoints -= TierFourBaseCost;
                                        //Increases the total tier-ranking
                                        TierPoints += 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Checks if the selected skill button is a part of the fifth tier
                            if (SkillButton == 10)
                            {
                                //Checks if the fifth tier has been unlocked
                                if (TierPoints >= TierFiveRequirement)
                                {
                                    //Checks if skill has been upgraded yet
                                    if (SkillPoints[SkillButton] == 0)
                                    {
                                        //Checks if player has enough endrun points
                                        if (EndRunPoints >= TierFiveBaseCost)
                                        {
                                            //Increases skillpoint by 1
                                            SkillPoints[SkillButton] += 1;
                                            //Decreases amount of endrun points available
                                            EndRunPoints -= TierFiveBaseCost;
                                            //Increases the total tier-ranking
                                            TierPoints += 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //Unlocks the next level of difficulty skill tier
            if (TierPoints == 4 && t < 4)
            {
                t = 5;
                DiffiTabs[0].SetActive(true);
                NewTier = true;
            }
            //Unlocks the next level of difficulty skill tier
            if (TierPoints == 8 && t < 7)
            {
                t = 8;
                DiffiTabs[0].SetActive(true);
                DiffiTabs[1].SetActive(true);
                NewTier = true;
            }
            //Unlocks the next level of difficulty skill tier
            if (TierPoints == 12 && t < 9)
            {
                t = 10;
                DiffiTabs[0].SetActive(true);

                DiffiTabs[1].SetActive(true);
                DiffiTabs[2].SetActive(true);
                NewTier = true;
            }
            //Unlocks the next level of difficulty skill tier
            if (TierPoints == 16 && t < 11)
            {
                t = 11;
                DiffiTabs[0].SetActive(true);

                DiffiTabs[1].SetActive(true);
                DiffiTabs[2].SetActive(true);
                DiffiTabs[3].SetActive(true);
                NewTier = true;
            }
            //Unlocks the next level of difficulty skill tier
            if (TierPoints == 20 && t < 12)
            {
                t = 12;
                DiffiTabs[0].SetActive(true);

                DiffiTabs[1].SetActive(true);
                DiffiTabs[2].SetActive(true);
                DiffiTabs[3].SetActive(true);
                DiffiTabs[4].SetActive(true);
                NewTier = true;
            }

        }

        //Updates the text display for the skill points
        Skills[SkillButton].text = SkillPoints[SkillButton] + "/" + MaxSkillPoints[SkillButton];
    }

    //Clears skillpoint data for a character
    public void ClearData()
    {
        //Sets all data points to zero
        SkillPoints[0] = 0;
        SkillPoints[1] = 0;
        SkillPoints[2] = 0;
        SkillPoints[3] = 0;
        SkillPoints[4] = 0;
        SkillPoints[5] = 0;
        SkillPoints[6] = 0;
        SkillPoints[7] = 0;
        SkillPoints[8] = 0;
        SkillPoints[9] = 0;
        SkillPoints[10] = 0;
        TierPoints = 0;
        t = 3;

        //Checks if the first difficulty tier increase is active
        if (TierPoints > 4)
        {
            //Sets the first difficulty tier's display to be active
            DiffiTabs[0].SetActive(true);
            //Checks if the second difficulty tier increase is active
            if (TierPoints > 8)
            {
                //Sets the second difficulty tier's display to be active
                DiffiTabs[1].SetActive(true);
                //Checks if the third difficulty tier increase is active
                if (TierPoints > 12)
                {
                    //Sets the third difficulty tier's display to be active
                    DiffiTabs[2].SetActive(true);
                    //Checks if the fourth difficulty tier increase is active
                    if (TierPoints > 16)
                    {
                        //Sets the fourth difficulty tier's display to be active
                        DiffiTabs[3].SetActive(true);
                        //Checks if the fifth difficulty tier increase is active
                        if (TierPoints > 20)
                        {
                            //Sets the fifth difficulty tier's display to be active
                            DiffiTabs[4].SetActive(true);
                        }
                        else
                        {
                            //Sets the fifth difficulty tier's display to be inactive
                            DiffiTabs[4].SetActive(false);
                        }
                    }
                    else
                    {
                        //Sets the fourth and fifth difficulty tier's display to be inactive
                        DiffiTabs[4].SetActive(false);
                        DiffiTabs[3].SetActive(false);
                    }
                }
                else
                {
                    //Sets the third, fourth, and fifth difficulty tier's display to be inactive
                    DiffiTabs[4].SetActive(false);
                    DiffiTabs[3].SetActive(false);
                    DiffiTabs[2].SetActive(false);
                }
            }
            else
            {
                //Sets the second, third, fourth, and fifth difficulty tier's display to be inactive
                DiffiTabs[4].SetActive(false);
                DiffiTabs[3].SetActive(false);
                DiffiTabs[2].SetActive(false);
                DiffiTabs[1].SetActive(false);
            }
        }
        else
        {
            //Sets the all difficulty tier's displays to be inactive
            DiffiTabs[4].SetActive(false);
            DiffiTabs[3].SetActive(false);
            DiffiTabs[2].SetActive(false);
            DiffiTabs[1].SetActive(false);
            DiffiTabs[0].SetActive(false);
        }

        //Changes the color of the skillbutton backgrounds
        for (var i = 0; i < SkillBackgrounds.Length; i++)
        {
            if (i < t)
            {
                SkillBackgrounds[i].color = new Color32(0, 0, 150, 255);
            }
            else
            {
                SkillBackgrounds[i].color = new Color32(100, 100, 100, 255);
            }
        }

        //Changes the text display for all skills
        for (var i = 0; i < SkillPoints.Length; i++)
        {
            Skills[i].text = SkillPoints[i] + "/" + MaxSkillPoints[i];

        }
    }

    //Function that saves character data
    public void SaveCharacter()
    {
        //Sets first run bool to false
        FirstRun = false;


        if (charType == CharType.MPGuard)
        {
            SaveSystem.SaveRogueCharacter(this);
        }

        if (charType == CharType.croc)
        {
            SaveSystem.SavePoisonCharacter(this);
        }

        if (charType == CharType.inmate001)
        {
            SaveSystem.SaveMainCharacter(this);
        }

        if (charType == CharType.SLB)
        {
            SaveSystem.SaveRocketCharacter(this);
        }

        print("Saved");
    }

    //Function that saves character skill data
    public void SaveSelection()
    {
        SaveSystem.SaveSelectedCharacter(this);
        
    }

    //Function that loads selection data
    public void LoadSelection()
    {
        PlayerData data = SaveSystem.LoadSelectedCharacter();
        //SelectedCharacter = data.SelectedCharacter;
    }

    //Function that loads character skill data
    public void LoadCharacter()
    {
        //Checks if MPGuard is selected
        if (charType == CharType.MPGuard)
        {
            RogueCharacterData data = SaveSystem.LoadRogueCharacter();
            SkillPoints[0] = data.Skill1;
            SkillPoints[1] = data.Skill2;
            SkillPoints[2] = data.Skill3;
            SkillPoints[3] = data.Skill4;
            SkillPoints[4] = data.Skill5;
            SkillPoints[5] = data.Skill6;
            SkillPoints[6] = data.Skill7;
            SkillPoints[7] = data.Skill8;
            SkillPoints[8] = data.Skill9;
            SkillPoints[9] = data.Skill10;
            SkillPoints[10] = data.Skill11;
            TierPoints = data.TP;
            t = data.tp;

            //Checks if character screen and mainmenu is false
            if (CharacterScreen == false && MainMenu == false)
            {
               //Sets MPGuard's model active
                Characters[0].SetActive(true);
            }
            //Determines which DiffiTabs are set to active/inactive based on how many TierPoints the player has
            if (TierPoints >= 4)
            {
                DiffiTabs[0].SetActive(true);
                if (TierPoints >= 8)
                {
                    DiffiTabs[1].SetActive(true);
                    if (TierPoints >= 12)
                    {
                        DiffiTabs[2].SetActive(true);
                        if (TierPoints >= 16)
                        {
                            DiffiTabs[3].SetActive(true);
                            if (TierPoints >= 20)
                            {
                                DiffiTabs[4].SetActive(true);
                            }
                            else
                            {
                                DiffiTabs[4].SetActive(false);
                            }
                        }
                        else
                        {
                            DiffiTabs[3].SetActive(false);
                        }
                    }
                    else
                    {
                        DiffiTabs[2].SetActive(false);
                    }
                }
                else
                {
                    DiffiTabs[1].SetActive(false);
                }
            }
            else
            {
                DiffiTabs[0].SetActive(false);
            }
            //Determines what color the skill backgrounds should have based on how many skills have been unlocked
            for (var i = 0; i < SkillBackgrounds.Length; i++)
            {
                if (i < t)
                {
                    //Changes background color to blue
                    //SkillBackgrounds[i].color = new Color32(0, 0, 150, 255);
                    SkillBackgrounds[i].sprite = gameObject.GetComponent<PrisonGuardTreeIcons>().prisonGuardOn[i];
                }
                else
                {
                    //Changes background color to grey
                    //SkillBackgrounds[i].color = new Color32(100, 100, 100, 255);
                    SkillBackgrounds[i].sprite = gameObject.GetComponent<PrisonGuardTreeIcons>().prisonGuardOff[i];
                }
            }
            //Determines text display for each skill's skillpoint display
            for (var i = 0; i < SkillPoints.Length; i++)
            {
                Skills[i].text = SkillPoints[i] + "/" + MaxSkillPoints[i];

            }
        }
        //Checks if croc is selected
        if (charType == CharType.croc)
        {
            PoisonCharacterData data = SaveSystem.LoadPoisonCharacter();
            SkillPoints[0] = data.Skill1;
            SkillPoints[1] = data.Skill2;
            SkillPoints[2] = data.Skill3;
            SkillPoints[3] = data.Skill4;
            SkillPoints[4] = data.Skill5;
            SkillPoints[5] = data.Skill6;
            SkillPoints[6] = data.Skill7;
            SkillPoints[7] = data.Skill8;
            SkillPoints[8] = data.Skill9;
            SkillPoints[9] = data.Skill10;
            SkillPoints[10] = data.Skill11;
            TierPoints = data.TP;
            t = data.tp;
            //Checks if character screen and mainmenu is false
            if (CharacterScreen == false && MainMenu == false)
            {
                //Sets Croc's model active
                Characters[1].SetActive(true);
            }

            //Determines which DiffiTabs are set to active/inactive based on how many TierPoints the player has
            if (TierPoints >= 4)
            {
                DiffiTabs[0].SetActive(true);
                if (TierPoints >= 8)
                {
                    DiffiTabs[1].SetActive(true);
                    if (TierPoints >= 12)
                    {
                        DiffiTabs[2].SetActive(true);
                        if (TierPoints >= 16)
                        {
                            DiffiTabs[3].SetActive(true);
                            if (TierPoints >= 20)
                            {
                                DiffiTabs[4].SetActive(true);
                            }
                            else
                            {
                                DiffiTabs[4].SetActive(false);
                            }
                        }
                        else
                        {
                            DiffiTabs[3].SetActive(false);
                        }
                    }
                    else
                    {
                        DiffiTabs[2].SetActive(false);
                    }
                }
                else
                {
                    DiffiTabs[1].SetActive(false);
                }
            }
            else
            {
                DiffiTabs[0].SetActive(false);
            }
            //Determines what color the skill backgrounds should have based on how many skills have been unlocked
            for (var i = 0; i < SkillBackgrounds.Length; i++)
            {
                if (i < t)
                {
                    //Changes background color to blue
                    //SkillBackgrounds[i].color = new Color32(0, 0, 150, 255);
                    SkillBackgrounds[i].sprite = gameObject.GetComponent<CrocTreeIcons>().crocOn[i];
                }
                else
                {
                    //Changes background color to grey
                    //SkillBackgrounds[i].color = new Color32(100, 100, 100, 255);
                    SkillBackgrounds[i].sprite = gameObject.GetComponent<CrocTreeIcons>().crocOff[i];
                }
            }
            //Determines text display for each skill's skillpoint display
            for (var i = 0; i < SkillPoints.Length; i++)
            {
                Skills[i].text = SkillPoints[i] + "/" + MaxSkillPoints[i];

            }
        }
        //Checks if inmate001 is selected
        if (charType == CharType.inmate001)
        {
            MainCharacterData data = SaveSystem.LoadMainCharacter();
            SkillPoints[0] = data.Skill1;
            SkillPoints[1] = data.Skill2;
            SkillPoints[2] = data.Skill3;
            SkillPoints[3] = data.Skill4;
            SkillPoints[4] = data.Skill5;
            SkillPoints[5] = data.Skill6;
            SkillPoints[6] = data.Skill7;
            SkillPoints[7] = data.Skill8;
            SkillPoints[8] = data.Skill9;
            SkillPoints[9] = data.Skill10;
            SkillPoints[10] = data.Skill11;
            TierPoints = data.TP;
            t = data.tp;
            //Checks if character screen and mainmenu is false
            if (CharacterScreen == false && MainMenu == false)
            {
                //Sets Inmate's model active
                Characters[2].SetActive(true);
            }

            //Determines which DiffiTabs are set to active/inactive based on how many TierPoints the player has
            if (TierPoints >= 4)
            {
                DiffiTabs[0].SetActive(true);
                if (TierPoints >= 8)
                {
                    DiffiTabs[1].SetActive(true);
                    if (TierPoints >= 12)
                    {
                        DiffiTabs[2].SetActive(true);
                        if (TierPoints >= 16)
                        {
                            DiffiTabs[3].SetActive(true);
                            if (TierPoints >= 20)
                            {
                                DiffiTabs[4].SetActive(true);
                            }
                            else
                            {
                                DiffiTabs[4].SetActive(false);
                            }
                        }
                        else
                        {
                            DiffiTabs[3].SetActive(false);
                        }
                    }
                    else
                    {
                        DiffiTabs[2].SetActive(false);
                    }
                }
                else
                {
                    DiffiTabs[1].SetActive(false);
                }
            }
            else
            {
                DiffiTabs[0].SetActive(false);
            }
            //Determines what color the skill backgrounds should have based on how many skills have been unlocked
            for (var i = 0; i < SkillBackgrounds.Length; i++)
            {
                if (i < t)
                {
                    //Changes background color to blue
                    //SkillBackgrounds[i].color = new Color32(0, 0, 150, 255);
                    SkillBackgrounds[i].sprite = gameObject.GetComponent<InmateTreeIcons>().inmateOn[i];
                }
                else
                {
                    //Changes background color to grey
                    //SkillBackgrounds[i].color = new Color32(100, 100, 100, 255);
                    SkillBackgrounds[i].sprite = gameObject.GetComponent<InmateTreeIcons>().inmateOff[i];
                }
            }
            //Determines text display for each skill's skillpoint display
            for (var i = 0; i < SkillPoints.Length; i++)
            {
                Skills[i].text = SkillPoints[i] + "/" + MaxSkillPoints[i];

            }
        }
        //Checks if SLB is selected
        if (charType == CharType.SLB)
        {
            RocketCharacterData data = SaveSystem.LoadRocketCharacter();
            SkillPoints[0] = data.Skill1;
            SkillPoints[1] = data.Skill2;
            SkillPoints[2] = data.Skill3;
            SkillPoints[3] = data.Skill4;
            SkillPoints[4] = data.Skill5;
            SkillPoints[5] = data.Skill6;
            SkillPoints[6] = data.Skill7;
            SkillPoints[7] = data.Skill8;
            SkillPoints[8] = data.Skill9;
            SkillPoints[9] = data.Skill10;
            SkillPoints[10] = data.Skill11;
            TierPoints = data.TP;
            t = data.tp;
            //Checks if character screen and mainmenu is false
            if (CharacterScreen == false && MainMenu == false)
            {
                //Sets SLB's model active
                Characters[3].SetActive(true);
            }

            //Determines which DiffiTabs are set to active/inactive based on how many TierPoints the player has
            if (TierPoints >= 4)
            {
                DiffiTabs[0].SetActive(true);
                if (TierPoints >= 8)
                {
                    DiffiTabs[1].SetActive(true);
                    if (TierPoints >= 12)
                    {
                        DiffiTabs[2].SetActive(true);
                        if (TierPoints >= 16)
                        {
                            DiffiTabs[3].SetActive(true);
                            if (TierPoints >= 20)
                            {
                                DiffiTabs[4].SetActive(true);
                            }
                            else
                            {
                                DiffiTabs[4].SetActive(false);
                            }
                        }
                        else
                        {
                            DiffiTabs[3].SetActive(false);
                        }
                    }
                    else
                    {
                        DiffiTabs[2].SetActive(false);
                    }
                }
                else
                {
                    DiffiTabs[1].SetActive(false);
                }
            }
            else
            {
                DiffiTabs[0].SetActive(false);
            }
            //Determines what color the skill backgrounds should have based on how many skills have been unlocked
            for (var i = 0; i < SkillBackgrounds.Length; i++)
            {
                if (i < t)
                {
                    //Changes background color to blue
                    //SkillBackgrounds[i].color = new Color32(0, 0, 150, 255);
                    SkillBackgrounds[i].sprite = gameObject.GetComponent<SBLTreeIcons>().rocketOn[i];
                }
                else
                {
                    //Changes background color to grey
                    //SkillBackgrounds[i].color = new Color32(100, 100, 100, 255);
                    SkillBackgrounds[i].sprite = gameObject.GetComponent<SBLTreeIcons>().rocketOff[i];
                }
            }
            //Determines text display for each skill's skillpoint display
            for (var i = 0; i < SkillPoints.Length; i++)
            {
                Skills[i].text = SkillPoints[i] + "/" + MaxSkillPoints[i];

            }
        }

    }

   

}
