using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class Power_JetBlock : MonoBehaviour
{
    private ScoreManaging scoreManager;
    private ObjectGenerator objectGenerator;
    private bool isTaken;
    

    private int delay;
    private DateTime start;
    private DateTime finish;
    private readonly object locker3 = new object();
    public AudioSource jetBlockEffect;
    public GameObject jetBlockObject;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManaging>();
        objectGenerator = FindObjectOfType<ObjectGenerator>();
        objectGenerator.jetLock = false;
        jetBlockObject = GameObject.Find("jetBlocks");
        if (jetBlockObject != null)
        {
            jetBlockEffect = jetBlockObject.GetComponent<AudioSource>();
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
            ThreadStart JetRef = new ThreadStart(JetBlock);
            Thread JetThread = new Thread(JetRef);
            JetThread.Start();
            jetBlockEffect.Play();
            isTaken = true;
            if (objectGenerator.isFall)
            {
                JetThread.Abort();
            }
            gameObject.SetActive(false);
        }
    }
    private void JetBlock()
    {
        lock (locker3)
        {
            if (isTaken)
            {
                isTaken = false;
                objectGenerator.JetBlockOn();
               

                delay = 7;
                start = DateTime.Now;
                finish = start.AddSeconds(delay);
                do
                {
                    if (objectGenerator.isFall)
                    {
                        break;
                    }
                } while (DateTime.Now < finish);

                objectGenerator.JetBlockOff();
                objectGenerator.jetLock = false;
            }

        }
    }
}
