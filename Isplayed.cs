using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isplayed : MonoBehaviour
{
    private bool isPlayed;
    // Start is called before the first frame update
    void Start()
    {
        isPlayed = true;
    }

    public bool getcondition()
    {
        return isPlayed;
    }
    public void Setcondition(bool cond)
    {
        isPlayed = cond;
    }
}
