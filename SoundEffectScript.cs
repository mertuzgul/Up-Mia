using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectScript : MonoBehaviour
{
    public AudioHandler soundHandler;
    public Toggle soundToggleButton;
    public Sprite soundOn;
    public Sprite soundOff;
    bool flag = false;


    public void Start()
    {
        soundHandler = GameObject.FindObjectOfType<AudioHandler>();

        bool SoundBool = (PlayerPrefs.GetInt("SoundOn") == 1) ? true : false;
        soundToggleButton.isOn = SoundBool;

        SoundBool = (PlayerPrefs.GetInt("SoundOff") == 0) ? true : false;
        soundToggleButton.isOn = SoundBool;
    }
   



}
