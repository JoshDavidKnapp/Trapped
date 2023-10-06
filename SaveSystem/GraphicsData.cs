using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class GraphicsData 
{
    public float MasterVol;
    public float SFXVol;
    public float MusicVol;

    public GraphicsData(MixerControl vol)
    {
        MasterVol = vol.StartingMaster;
        SFXVol = vol.StartingSFX;
        MusicVol = vol.StartingMusic;
    }


}
