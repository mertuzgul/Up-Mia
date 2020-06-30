using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading;

public class Power_HighJump : MonoBehaviour
{
    private CharacterControl characterControl;
    private ObjectGenerator objectGenerator;
    private bool isTaken;

    private int delay;
    private DateTime start;
    private DateTime finish;
    public AudioSource doubleJumpEffect;
    public GameObject doubleJumpObject;

    private static readonly object _locker = new object();


    // Start is called before the first frame update
    void Start()
    {
        characterControl = FindObjectOfType<CharacterControl>();
        objectGenerator = FindObjectOfType<ObjectGenerator>();
        isTaken = false;
        doubleJumpObject = GameObject.Find("doubleJump");
        if (doubleJumpObject != null)
        {
            doubleJumpEffect = doubleJumpObject.GetComponent<AudioSource>();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            ThreadStart jumpRef = new ThreadStart(ImproveJump);
            Thread jumpThread = new Thread(jumpRef);
            jumpThread.Start();
            doubleJumpEffect.Play();
            isTaken = true;
            if(objectGenerator.isFall)
            {
                jumpThread.Abort();
            }
        }
    }
    public void ImproveJump()
    {

        lock (_locker)
        {
            if (isTaken)
            {
                isTaken = false;
                //Upgrade Power Jump
                characterControl.UpgradePowerJump();

                //Wait for 10 seconds
                delay = 10;
                start = DateTime.Now;
                finish = start.AddSeconds(delay);
                do {
                    if (objectGenerator.isFall)
                    {
                        break;
                    }
                } while (DateTime.Now < finish);

                //Return Normal Jump
                characterControl.NormalPowerJump();
            }
        }
    }

   
}
