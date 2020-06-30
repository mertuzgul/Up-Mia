using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsScreen;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene(2);
    }
    public void Settings()
    {
        settingsScreen.SetActive(true);
        
    }

    public void Shop()
    {
        SceneManager.LoadScene(4);
    }
}
