using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject HoverUiRogue;
    public GameObject HoverUiPoison;
    public GameObject HoverUiMain;
    public GameObject HoverUiRocket;
    public GameObject Skilltree;

    public bool SkillTreeToolTips;
    public GameObject RegularToolTip;

    
   

    
    //Function that activates when the mouse cursor has entered a box collider
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SkillTreeToolTips == true)
        {
            SkillTreeActive();
        }
        else
        {
            RegularToolTip.SetActive(true);
        }
        
    }


    //Function that activates when the mouse cursor has exited a box collider
    public void OnPointerExit(PointerEventData eventData)
    {
        if (SkillTreeToolTips == true)
        {
            SkillTreeInactive();
        }
        else
        {
            RegularToolTip.SetActive(false);
        }
        //Cursor.visible = true;
    }


    private void SkillTreeActive()
    {
        //Sets the hovertext for MPGuard to true
        if (Skilltree.GetComponent<SkillTree>().charType == CharType.MPGuard)
        {
            print("Rogue Active");
            HoverUiRogue.SetActive(true);
            //print(Skilltree.GetComponent<SkillTree>().SelectedCharacter);
        }
        //Sets the hovertext for Croc to true
        if (Skilltree.GetComponent<SkillTree>().charType == CharType.croc)
        {
            print("Poison Active");
            HoverUiPoison.SetActive(true);
        }
        //Sets the hovertext for Inmate to true
        if (Skilltree.GetComponent<SkillTree>().charType == CharType.inmate001)
        {
            print("Main Active");
            HoverUiMain.SetActive(true);
        }
        //Sets the hovertext for SLB to true
        if (Skilltree.GetComponent<SkillTree>().charType == CharType.SLB)
        {
            print("Rocket Active");
            HoverUiRocket.SetActive(true);
        }
    }


    private void SkillTreeInactive()
    {
        //Sets the hovertext for MPGuard to false
        if (Skilltree.GetComponent<SkillTree>().charType == CharType.MPGuard)
        {
            HoverUiRogue.SetActive(false);
        }
        //Sets the hovertext for Croc to false
        if (Skilltree.GetComponent<SkillTree>().charType == CharType.croc)
        {
            HoverUiPoison.SetActive(false);
        }
        //Sets the hovertext for Inmate to false
        if (Skilltree.GetComponent<SkillTree>().charType == CharType.inmate001)
        {
            HoverUiMain.SetActive(false);
        }
        //Sets the hovertext for SLB to false
        if (Skilltree.GetComponent<SkillTree>().charType == CharType.SLB)
        {
            HoverUiRocket.SetActive(false);
        }
    }
}
