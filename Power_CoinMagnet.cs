using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading;

public class Power_CoinMagnet : MonoBehaviour
{
    private Object_Coin[] objectCoins;
    private ObjectGenerator objectGenerator;

    private int delay;
    private DateTime start;
    private DateTime finish;
    public AudioSource magnetEffect;
    public GameObject magnetObject;


    private readonly object locker1 = new object();

    // Start is called before the first frame update
    void Start()
    {
        objectCoins = FindObjectsOfType<Object_Coin>();
        objectGenerator = FindObjectOfType<ObjectGenerator>();
        magnetObject = GameObject.Find("coinMagnet");
        if (magnetObject != null)
        {
            magnetEffect = magnetObject.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        objectCoins = FindObjectsOfType<Object_Coin>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //gameObject.SetActive(false);
            
            ThreadStart MagnetRef = new ThreadStart(CoinMagnet);
            Thread MagnetThread = new Thread(MagnetRef);
            MagnetThread.Start();
            magnetEffect.Play();
            if (objectGenerator.isFall)
            {
                MagnetThread.Abort();
            }
            gameObject.SetActive(false);
        }
    }
    
    
    public void CoinMagnet()
    {

        lock (locker1)
        {

            //Do Power for 10 seconds
            delay = 10;
            start = DateTime.Now;
            finish = start.AddSeconds(delay);
            do
            {
                for (int i = 0; i < objectCoins.Length; i++)
                {
                    //Power Up for each coin
                    objectCoins[i].setPowerTrue();
                }
                if (objectGenerator.isFall)
                {
                    break;
                }

            } while (DateTime.Now < finish);

            for (int i = 0; i < objectCoins.Length; i++)
            {
                //Power Down for each coin
                objectCoins[i].setPowerFalse();
            }
        }
    }
    
}
