using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Vector2 spawnPosition;
    public GameObject basamak;
    private GameObject[] basamaklar;
    private GameObject[] collectibles;
    public GameObject gameOverScreen;

    public bool firstStep;
    

    public GameObject[] ObjectList;
    float sizeList = 10f;
    Vector2 ObjectPosition;
    int randomChoice;

    private ScoreManaging scoreManager;
    private CameraControl cameraControl;
    private CharacterControl characterControl;

    public bool isFall;
    public bool isQuake;
    public bool isJetBlock;
    public bool jetLock;


    private Vector2[] basamakLocations;


    // Start is called before the first frame update
    void Start()
    {
        isFall = false;
        scoreManager = FindObjectOfType<ScoreManaging>();
        cameraControl = FindObjectOfType<CameraControl>();
        characterControl = FindObjectOfType<CharacterControl>();
        basamakLocations = new Vector2[20];
        basamaklar = new GameObject[20];
        collectibles = new GameObject[20];


        //rigidbody = GetComponent<Rigidbody2D>();
        spawnPosition = new Vector2(0f, -4.16f);

        basamakLocations[0] = spawnPosition;

        firstStep = true;
        basamaklar[0]=Instantiate(basamak, spawnPosition, Quaternion.identity);
        

        for (int i = 1; i < 20; i++)
        {
            spawnPosition.y += 3.5f;
            spawnPosition.x = Random.Range(-1.5f, 1.5f);
            //spawnPosition.x = Random.Range(-1f, 1f);

            basamakLocations[i] = spawnPosition;

            basamaklar[i] = Instantiate(basamak, spawnPosition, Quaternion.identity);

           
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Step")
        {
            spawnPosition.y += 3.5f;
            spawnPosition.x = Random.Range(-1.5f, 1.5f);
            VectorArrayLeftShift(basamakLocations, spawnPosition);
            ObjectArrayLeftShift(basamaklar, Instantiate(basamak, spawnPosition, Quaternion.identity));
            

    
            

            //Increase Score
            scoreManager.setScore(5f);//+5
            //Increase Camera Speed
            if(cameraControl.cameraSpeed < 0.05f)
            {
                cameraControl.cameraSpeed += 0.001f;
            }
            

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Coin")
            Destroy(collision.gameObject);
        if (collision.gameObject.tag == "Icecream")
            Destroy(collision.gameObject);


        if (collision.gameObject.tag == "Player")
        {
            if (characterControl.getIsBarrier())
            {
                //Barrier varsa
                characterControl.setIsBarrier(false);
                cameraControl.cameraSpeed = 0.001f;
                Vector2 cc = basamakLocations[0];

                cc.y += 1.5f;

                collision.gameObject.transform.position = cc;

            }
            else
            {
                //Kız ölünce olacaklar
                isFall = true;

                characterControl.maxIceCream = 50;
                characterControl.taken = 0;

                cameraControl.cameraSpeed = 0f;
                gameOverScreen.SetActive(true);
            }
            
            
        }
    }
    
    public void VectorArrayLeftShift(Vector2[] list, Vector2 lastElement)
    {  
        for(int i = 0; i < list.Length-1; i++)
        {
            list[i] = list[i + 1];
        }

        list[list.Length-1] = lastElement;
    }

    public void ObjectArrayLeftShift(GameObject[] list, GameObject lastElement)
    {
        for (int i = 0; i < list.Length - 1; i++)
        {
            list[i] = list[i + 1];
        }

        list[list.Length - 1] = lastElement;
    }
    
    public void QuakeOn()
    {
        isQuake = true;
    }

    public void QuakeOff()
    {
        isQuake = false;
    }
    public void JetBlockOn()
    {
        
        isJetBlock = true;
    }
    public void JetBlockOff()
    {
        isJetBlock = false;
    }
}
