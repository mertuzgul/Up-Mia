using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButtons : MonoBehaviour
{
    public GameObject PauseScreen;
    private CameraControl cameraControl;
    private float CameraSpeed;
    private Isplayed played;

    void Update()
    {
        played = FindObjectOfType<Isplayed>();
        played.Setcondition(false);

        cameraControl = FindObjectOfType<CameraControl>();
        if(cameraControl.cameraSpeed != 0)
        {
            CameraSpeed = cameraControl.cameraSpeed;
            cameraControl.cameraSpeed = 0f;
        }
        Time.timeScale = 0;

    }

    public void ContinueGame()
    {
        played.Setcondition(true);
        cameraControl.cameraSpeed = CameraSpeed;
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
    }

    public void ExitGame()
    {
        played.Setcondition(true);
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
        SceneManager.LoadScene(0);
    }

    
}
