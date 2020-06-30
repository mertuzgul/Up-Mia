using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basamak : MonoBehaviour
{
    public bool sa;
    public bool stepDir; // true means left
    private ObjectGenerator objectGenerator;
    private Power_JetBlock[] power_JetBlocks;

    public GameObject[] ObjectList;
    int randomChoice;
    float sizeList = 10f;
    Vector2 ObjectPosition;
    public bool hasObject;
    GameObject ItemOn;
    public float newSpeed = 0.01f;
    private bool jetBlockUp;
    // Start is called before the first frame update
    void Start()
    {
        sa = false;
        jetBlockUp = false;
        objectGenerator = FindObjectOfType<ObjectGenerator>();
        power_JetBlocks = FindObjectsOfType<Power_JetBlock>();

        int i = UnityEngine.Random.Range(0, 2);

        

        if(i == 0)
        {
            stepDir = true;
        }
        else
        {
            stepDir = false;
        }
        if (objectGenerator.firstStep == false)
        {
            randomChoice = (int)UnityEngine.Random.Range(0f, 50f);
            if (randomChoice >= 30) { /*DO NOTHING*/ hasObject = false; }

            else
            {
                ObjectPosition.x = transform.position.x;
                ObjectPosition.y = transform.position.y + 1.0f;
                ItemOn = Instantiate(ObjectList[randomChoice], ObjectPosition, Quaternion.identity);
                hasObject = true;
            }
        }
        objectGenerator.firstStep = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectGenerator.isQuake )
        {
           
            if (stepDir)
            {
                Vector3 newPos = new Vector3(transform.position.x - 0.01f, transform.position.y, 0);
                transform.position = Vector3.Lerp(transform.position, newPos, 0.3f);

            }
            else
            {
                Vector3 newPos = new Vector3(transform.position.x + 0.01f, transform.position.y, 0);
                transform.position = Vector3.Lerp(transform.position, newPos, 0.3f);
             
            }

            if(transform.position.x < -2f || transform.position.x > 2f)
            {
                if (stepDir)
                {
                    stepDir = false;
                }
                else
                {
                    stepDir = true;
                }
            }
            
        }

        if (objectGenerator.isJetBlock == false)
        {
            jetBlockUp = false;
        }

        if (jetBlockUp)
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, 0.3f);
            //JetBlockSpeedChanger();
        }
            
        
        
    }

    private void JetBlockSpeedChanger()
    {
        newSpeed = 0.01f;
    }

    private void LateUpdate()
    {
        if (objectGenerator.isQuake)
        {
            if (hasObject)
            {
                if (ItemOn != null)
                {
                    Vector3 newPos2 = new Vector3(transform.position.x + 0.01f, ItemOn.gameObject.transform.position.y, 0);
                    ItemOn.gameObject.transform.position = Vector3.Lerp(ItemOn.gameObject.transform.position, newPos2, 0.3f);
                }

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            sa = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            sa = true;
        }
        if (collision.gameObject.tag == "Player")
        {
            if (objectGenerator.jetLock==false)
            {
                if (objectGenerator.isJetBlock)
                {
                    jetBlockUp = true;
                    objectGenerator.jetLock = true;
                }
               
            }
        }


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    public bool getSa()
    {
        return sa;
    }

}
