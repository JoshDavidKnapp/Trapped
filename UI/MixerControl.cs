using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerControl : MonoBehaviour
{
    public AudioMixer Mixer;

    public Slider Master;
    public Slider SFX;
    public Slider Music;

    public float StartingMaster;
    public float StartingSFX;
    public float StartingMusic;

    private void Start()
    {
        if(SaveSystem.LoadAudioData() != null)
        {
            LoadAudioData();
            
        }
    }

    private void LoadAudioData()
    {
        GraphicsData data = SaveSystem.LoadAudioData();
        print("SFX Level " + data.SFXVol);
        StartingMaster = data.MasterVol;
        StartingSFX = data.SFXVol;
        StartingMusic = data.MusicVol;
        if (Master != null)
        {
            Master.value = StartingMaster;
            SFX.value = StartingSFX;
            Music.value = StartingMusic;
        }
        SetMasterVolume(data.MasterVol);
        SetSFXVolume(data.SFXVol);
        SetMusicVolume(data.MusicVol);
        
    }

    //Controls Master volume levels
    public void SetMasterVolume(float volume)
    {
        Mixer.SetFloat("MasterExposedParam", Mathf.Log10(volume) * 20);
        StartingMaster = volume;
        SaveSystem.SaveAudioData(this);
    }

    //Controls Music volume levels
    public void SetMusicVolume(float volume)
    {
        Mixer.SetFloat("MusicExposedParam", Mathf.Log10(volume) * 20);
        StartingMusic = volume;
        SaveSystem.SaveAudioData(this);
    }

    //Controls SFX volume levels
    public void SetSFXVolume(float volume)
    {
        Mixer.SetFloat("SFXExposedParam", Mathf.Log10(volume) * 20);
        StartingSFX = volume;
        SaveSystem.SaveAudioData(this);
        print("Saved Audio " + volume);
    }
}
