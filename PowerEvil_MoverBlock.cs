using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PowerEvil_MoverBlock : MonoBehaviour
{

    private ObjectGenerator objectGenerator;
    private ScoreManaging scoreManager;
    private bool isTaken;

    private int delay;
    private DateTime start;
    private DateTime finish;

    public AudioSource movingBlockEffect;
    public GameObject movingBlockObject;


    private readonly object locker3 = new object();

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManaging>();
        objectGenerator = FindObjectOfType<ObjectGenerator>();
        movingBlockObject = GameObject.Find("movingBlocks");
        if (movingBlockObject != null)
        {
            movingBlockEffect = movingBlockObject.GetComponent<AudioSource>();
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
            ThreadStart QuakeRef = new ThreadStart(Quake);
            Thread QuakeThread = new Thread(QuakeRef);
            QuakeThread.Start();
            movingBlockEffect.Play();
            isTaken = true;
            if (objectGenerator.isFall)
            {
                QuakeThread.Abort();
            }
            gameObject.SetActive(false);

        }
    }

    private void Quake()
    {
        lock (locker3)
        {
            if (isTaken)
            {
                isTaken = false;
                objectGenerator.QuakeOn();

                delay = 15;
                start = DateTime.Now;
                finish = start.AddSeconds(delay);
                do
                {
                    if (objectGenerator.isFall)
                    {
                        break;
                    }
                } while (DateTime.Now < finish);

                objectGenerator.QuakeOff();
            }

        }
    }
}
