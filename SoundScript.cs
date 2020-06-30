using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour
{
    public AudioHandler musicHandler;
    public Toggle musicToggleButton;
    public Sprite musicOn;
    public Sprite musicOff;
    bool flag = false;
    
    public void Start()
    {
        musicHandler = GameObject.FindObjectOfType<AudioHandler>();

        bool MusicBool = (PlayerPrefs.GetInt("MusicOn") == 1) ? true : false;
        musicToggleButton.isOn = MusicBool;

        MusicBool = (PlayerPrefs.GetInt("MusicOff") == 0) ? true : false;
        musicToggleButton.isOn = MusicBool;

    }
    public void pauseMusic()
    {
        musicHandler.ToggleMusic();
        UpdateIcon();
    }
    

    public void UpdateIcon()
    {
        
       if (PlayerPrefs.GetInt("Muted",0)==0)
       {
            
            AudioListener.volume = 1;
            musicToggleButton.GetComponent<Image>().sprite = musicOn;
            
       }
       else
       {
            AudioListener.volume = 0;
            musicToggleButton.GetComponent<Image>().sprite = musicOff;
            
        }
    }
 
   
    
}
