using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    static AudioHandler instance = null; //Duplicate audio issues

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
   
    public void ToggleMusic()
    {
        if(PlayerPrefs.GetInt("Muted",0) == 0)
        {
            PlayerPrefs.SetInt("Muted",1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
        }
    }
}
