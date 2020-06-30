using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButtons : MonoBehaviour
{
    public GameObject settingsScreen;
    public void ReturnHome()
    {
        SceneManager.LoadScene(0);
        settingsScreen.SetActive(false);
    }
}
