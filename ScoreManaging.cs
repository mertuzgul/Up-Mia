using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManaging : MonoBehaviour
{
    public float points;

    public Text scoreText;
    public Text highScoreText;
    public Text coinText;

    private float score;
    private float highScore;
    private float coin;
    public bool isMult;

    private Object_Coin[] objectCoins;
    
    // Start is called before the first frame update
    void Start()
    {
        objectCoins = FindObjectsOfType<Object_Coin>();
        //PlayerPrefs.SetFloat("HighScore", 0);
        //PlayerPrefs.SetFloat("Coin", 200);

        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetFloat("HighScore");
        }
        if (PlayerPrefs.HasKey("Coin"))
        {
            coin = PlayerPrefs.GetFloat("Coin");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }

        scoreText.text = Mathf.Round(score).ToString();
        highScoreText.text = Mathf.Round(highScore).ToString();
        
        coinText.text = Mathf.Round(coin).ToString();
        PlayerPrefs.SetFloat("Coin", coin);
        
    }

    public void increaseCoin()
    {
        if (isMult)
        {
            coin += 2f;
        }
        else
        {
            coin += 1f;
        }
        
        
        
    }
    public void increaseScore()
    {
        score += 10f;
    }
    public void setScore(float value)
    {
        score += value;
    }

    public void coinMultTrue()
    {
        isMult = true;
    }
    public void coinMultFalse()
    {
        isMult = false;
    }
}
