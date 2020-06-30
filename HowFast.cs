using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowFast : MonoBehaviour
{

    public Text FastText;
    private CameraControl cameraControl;
    private float CameraSpeed;
    private string score;

    public GameObject PauseScreen;

    void Update()
    {
        if (PauseScreen.activeSelf)
        {
            score = "X" + CameraSpeed.ToString();
            FastText.text = score;
        }
        else
        {
            cameraControl = FindObjectOfType<CameraControl>();
            CameraSpeed = cameraControl.cameraSpeed * 1000;
            score = "X" + CameraSpeed.ToString();
            FastText.text = score;
        }


    }
}
