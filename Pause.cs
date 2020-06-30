using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public GameObject PauseScreen;
    // Start is called before the first frame update


    public void Click()
    {
        PauseScreen.SetActive(true);
    }
}
