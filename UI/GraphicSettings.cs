using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphicSettings : MonoBehaviour
{
    public Image FullButton;
    public Image WindowButton;
    public Dropdown GraphicLevel;

    // Start is called before the first frame update
    void Start()
    {
        DropDownMenu();
    }

    // Update is called once per frame
    void Update()
    {
        FullTint();
    }

    public void DropDownMenu()
    {
        GraphicLevel.value = QualitySettings.GetQualityLevel();
    }


    public void HandleInputData(int val)
    {
        QualitySettings.SetQualityLevel(val);
        Debug.Log("Graphics set to " + val);
        
    }

    private void FullTint()
    {
        if(Screen.fullScreen == true)
        {
            FullButton.color = new Color32(0, 252, 255, 255);
            WindowButton.color = new Color32(255, 255, 255, 255);


        }
        else
        {
            FullButton.color = new Color32(255, 255, 255, 255);
            WindowButton.color = new Color32(0, 252, 255, 255);
            
        }
    }

    public void ToggleFullOn()
    {
        
        if (Screen.fullScreen == false)
        {
            Screen.fullScreen = !Screen.fullScreen;
            
        }
    }

    public void ToggleFullOff()
    {
        if (Screen.fullScreen == true)
        {
            Screen.fullScreen = !Screen.fullScreen;
            
        }

    }
}
