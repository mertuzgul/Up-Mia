using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Power_MoreCoin : MonoBehaviour
{
    private ScoreManaging scoreManager;
    private bool isTaken;
    ObjectGenerator objectGenerator;
    Object_Coin objectCoin;

    private int delay;
    private DateTime start;
    private DateTime finish;
    public AudioSource doubleCoinEffect;
    public GameObject doubleCoinObject;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManaging>();
        objectGenerator = FindObjectOfType<ObjectGenerator>();
        objectCoin = FindObjectOfType<Object_Coin>();
        doubleCoinObject = GameObject.Find("doubleCoin");
        if (doubleCoinObject != null)
        {
            doubleCoinEffect = doubleCoinObject.GetComponent<AudioSource>();
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
            ThreadStart MultRef = new ThreadStart(CoinMult);
            Thread MultThread = new Thread(MultRef);
            MultThread.Start();
            doubleCoinEffect.Play();
            isTaken = true;
            if (objectGenerator.isFall)
            {
                MultThread.Abort();
            }
            gameObject.SetActive(false);

        }
    }

    private void CoinMult()
    {
        if (isTaken)
        {
            isTaken = false;
            scoreManager.coinMultTrue();
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

            scoreManager.coinMultFalse();


        }
    }
}
