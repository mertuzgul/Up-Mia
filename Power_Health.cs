using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Power_Health : MonoBehaviour
{
    private ScoreManaging scoreManager;
    private CharacterControl characterControl;
    private ObjectGenerator objectGenerator;
    private bool isTaken;
    public AudioSource shieldEffect;
    public GameObject shieldObject;

    private int delay;
    private DateTime start;
    private DateTime finish;

    private readonly object locker2 = new object();

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManaging>();
        characterControl = FindObjectOfType<CharacterControl>();
        objectGenerator = FindObjectOfType<ObjectGenerator>();
        shieldObject = GameObject.Find("shield");
        if (shieldObject != null)
        {
            shieldEffect = shieldObject.GetComponent<AudioSource>();
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
            ThreadStart ShieldRef = new ThreadStart(ShieldUp);
            Thread ShieldThread = new Thread(ShieldRef);
            ShieldThread.Start();
            shieldEffect.Play();
            isTaken = true;
            if (objectGenerator.isFall)
            {
                ShieldThread.Abort();
            }
            gameObject.SetActive(false);
           
        }
    }

    private void ShieldUp()
    {
        
            if(isTaken)
            {
                isTaken = false;
                characterControl.BarrierOn();

                delay = 30;
                start = DateTime.Now;
                finish = start.AddSeconds(delay);
                do
                {
                    if (objectGenerator.isFall)
                    {
                        break;
                    }
                } while (DateTime.Now < finish);

                characterControl.BarrierOff();
            }
            
        

    }

   
}
