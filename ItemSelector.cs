using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{

    public Text coinText;
    private float coin;

    public List<GameObject> imgs = new List<GameObject>();
    public List<Text> priceTags = new List<Text>();
    public Text WearTag;

    private List<int> imgSelected = new List<int>();
   
    private void Start()
    {
        /*
        for (int i = imgs.Count/2; i < imgs.Count; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), 0);
        }
        */
        //PlayerPrefs.SetInt("WearTag", 0);

        String wear = PlayerPrefs.GetInt("WearTag").ToString();
        WearTag.text = wear;

        if (PlayerPrefs.HasKey("Coin"))
        {
            coin = PlayerPrefs.GetFloat("Coin");
        }

        for (int i = 0; i < imgs.Count-1; i++)
        {
            if(i<4)
            {
                imgSelected.Add(0);
            }
           
            else
            {
                imgSelected.Add(PlayerPrefs.GetInt(i.ToString()));
                if(PlayerPrefs.GetInt(i.ToString()) ==1 )
                {
                    priceTags[i-4].text = "Sold!";
                }
            }
        }
        imgSelected.Add(0);//For No Hat

    }
    private void Update()
    {
        coinText.text = Mathf.Round(coin).ToString();
        int counter = 0;
        foreach (int i in imgSelected)
        {

            if (i == 1)
            {
                imgs[counter].SetActive(true);
            }
            else
            {
                imgs[counter].SetActive(false);
            }
            counter++;
        }
        wearHat();
    }

    public void wearHat()
    {
        for (int i = 0; i < (imgSelected.Count-1) / 2; i++)
        {
            if (imgSelected[i] == 1 && imgSelected[i + 4] == 1)
            {
                PlayerPrefs.SetInt("WearTag", i+1);
                WearTag.text = (i+1).ToString();
            }
        }
        if(imgSelected[8] == 1)
        {
            PlayerPrefs.SetInt("WearTag", 0);
            WearTag.text = "0";
        }
    }

    public void selected0()
    {
        imgSelected[0] = 1;
        imgSelected[1] = 0;
        imgSelected[2] = 0;
        imgSelected[3] = 0;
        imgSelected[8] = 0;
    }

    public void selected1()
    {
        imgSelected[0] = 0;
        imgSelected[1] = 1;
        imgSelected[2] = 0;
        imgSelected[3] = 0;
        imgSelected[8] = 0;
    }
    public void selected2()
    {
        imgSelected[0] = 0;
        imgSelected[1] = 0;
        imgSelected[2] = 1;
        imgSelected[3] = 0;
        imgSelected[8] = 0;
    }
    public void selected3()
    {
        imgSelected[0] = 0;
        imgSelected[1] = 0;
        imgSelected[2] = 0;
        imgSelected[3] = 1;
        imgSelected[8] = 0;
    }
    public void selectedNoHat()
    {
        imgSelected[0] = 0;
        imgSelected[1] = 0;
        imgSelected[2] = 0;
        imgSelected[3] = 0;
        imgSelected[8] = 1;
    }




    // Update is called once per frame
    public void ExitStore()
    {
        SceneManager.LoadScene(0);
    }

    public void Buy()
    {
        if(coin >= 100)
        {
            
            for(int i = 0;i<(imgSelected.Count-1)/2; i++)
            {
                if (imgSelected[i] == 1 && imgSelected[i + 4] == 0)
                {
                    coin = coin - 100;
                    PlayerPrefs.SetFloat("Coin", coin);

                    priceTags[i].text = "Sold!";
                    imgs[i+4].SetActive(true);
                    imgSelected[i+4] = 1;
                    PlayerPrefs.SetInt((i+4).ToString(), 1);
                }
            }
        }
    }


}
