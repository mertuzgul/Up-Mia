using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HWT_Buttons : MonoBehaviour
{
    public void GoDefinitions()
    {
        SceneManager.LoadScene(3);
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene(0);
    }
}
