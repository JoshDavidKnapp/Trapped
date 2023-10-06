using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public GameObject[] TierTexts;
    public GameObject Skill;
    public int SkillRelation;
    public float Xpos;
    public float Ypos;
    public int CurrentTier;

    public bool SkillTreeTooltips;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SkillTreeTooltips == true)
        {
            TierCheck();
        }

        //gameObject.transform.position = new Vector3(Input.mousePosition.x * Xpos, Input.mousePosition.y * Ypos, 0);
        gameObject.transform.position = new Vector3(Input.mousePosition.x + Xpos, Input.mousePosition.y - Ypos, 50);
    }

    //Function that switches the tier display of the tool tip
    public void TierSwitch()
    {
        //Sets all tiers to false
        if (CurrentTier < TierTexts.Length)
        {
            for (var i = 0; i < TierTexts.Length; i++)
            {
                TierTexts[i].SetActive(false);
            }
            
        }
        
    }

    private void TierCheck()
    {
        //Checks what the current tier is
        CurrentTier = Skill.GetComponent<SkillTree>().SkillPoints[SkillRelation];

        //Sets current tier to true
        TierTexts[CurrentTier].SetActive(true);
    }
}
