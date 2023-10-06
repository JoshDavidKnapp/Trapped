using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RocketCharacterData
{
    public int Skill1;
    public int Skill2;
    public int Skill3;
    public int Skill4;
    public int Skill5;
    public int Skill6;
    public int Skill7;
    public int Skill8;
    public int Skill9;
    public int Skill10;
    public int Skill11;

    public int TP;
    public int tp;

    public bool FR;

    //Function that determines what variables get recorded
    public RocketCharacterData(SkillTree skillrank)
    {
        Skill1 = skillrank.SkillPoints[0];
        Skill2 = skillrank.SkillPoints[1];
        Skill3 = skillrank.SkillPoints[2];
        Skill4 = skillrank.SkillPoints[3];
        Skill5 = skillrank.SkillPoints[4];
        Skill6 = skillrank.SkillPoints[5];
        Skill7 = skillrank.SkillPoints[6];
        Skill8 = skillrank.SkillPoints[7];
        Skill9 = skillrank.SkillPoints[8];
        Skill10 = skillrank.SkillPoints[9];
        Skill11 = skillrank.SkillPoints[10];

        TP = skillrank.TierPoints;
        tp = skillrank.t;

        FR = skillrank.FirstRun;
    }
}
