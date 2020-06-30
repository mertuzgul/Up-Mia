using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Coin : MonoBehaviour
{

    private ScoreManaging scoreManager;

    private Rigidbody2D rb;
    private bool flyToMia;
    private bool isPowered;
    private Vector2 direction;
    private GameObject player;
    private float timestamp;
    
    public AudioSource aScorce;
    public GameObject obj;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreManager = FindObjectOfType<ScoreManaging>();
        flyToMia = false;
        isPowered = false;
        obj = GameObject.Find("AudioObject");
        if (obj != null)
        {
            aScorce = obj.GetComponent<AudioSource>();
        }

    }


    // Update is called once per frame
    void Update()
    {
        if(isPowered == false)
        {
            flyToMia = false;
        }
        
        if(flyToMia)
        {
            
            direction = -(transform.position - player.transform.position).normalized;
            rb.velocity = new Vector2(direction.x,direction.y)*10f*(Time.time/timestamp);
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            //Increase Coin and Score
            aScorce.Play();
            scoreManager.increaseCoin();//+1
            scoreManager.increaseScore();//+10
        }
        if (collision.gameObject.tag == "Magnet")
        {
            if (isPowered)
            {
                player = collision.gameObject;
                timestamp = Time.time;
                flyToMia = true;
            }
        }
    }

    public void setPowerTrue()
    {
        isPowered = true;
    }
    public void setPowerFalse()
    {
        isPowered = false;
    }

   
}
