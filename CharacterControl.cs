using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{
    private Vector2 startPos, endPos, direction;
    public float jumpForce =455f ;
    private bool onAir = true;
    private bool isUp = false;
    private bool faceSideLeft = true;
    private bool isBarrier = false;
    Rigidbody2D miaPhysics;
    SpriteRenderer miaSpriteRenderer;

    public GameObject shield;
    private Vector2 shieldVec;

    public List<SpriteRenderer> hatRenderer = new List<SpriteRenderer>();
    public List<GameObject> hatObject = new List<GameObject>();

    public int maxIceCream;
    public int taken;
    public Text TakeText;
    private string ice;
    private int wear;

    [SerializeField]
    private GameObject CoinMagnet;
    private Isplayed played;

    // Start is called before the first frame update
    void Start()
    {
        miaPhysics = GetComponent<Rigidbody2D>();
        miaSpriteRenderer = GetComponent<SpriteRenderer>();
        CoinMagnet = GameObject.FindGameObjectWithTag("Magnet");
        played = FindObjectOfType<Isplayed>();

        wear = PlayerPrefs.GetInt("WearTag");

        if (wear != 0)
        {
            hatObject[wear-1].SetActive(true);
        }
        else
        {
            wear = 1;
        }

        maxIceCream = 50;
        taken = 0;
        
    }

    void Update()
    {
        CoinMagnet.transform.position = new Vector2(transform.position.x, transform.position.y);

        if (taken == maxIceCream)
        {
            maxIceCream = maxIceCream + 10;
        }
        ice = taken.ToString() + "/" + maxIceCream.ToString();
        TakeText.text = ice;

        if (isBarrier)
        {
            shield.gameObject.SetActive(true);
        }
        else
        {
            shield.gameObject.SetActive(false);
        }


        #region Testing Inputs
        if (onAir == false && played.getcondition())
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                endPos = Input.mousePosition;

                direction = endPos - startPos;
                direction.Normalize();
                if (direction.x > 0)
                {
                    faceSideLeft = false;
                }
                else
                {
                    faceSideLeft = true;
                }

                if (endPos.y > startPos.y)
                {
                    isUp = true;
                }
                else
                {
                    isUp = false;
                }
            }
        }

        #endregion

        #region Mobile Inputs
        if (onAir == false && played.getcondition())
        {

            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    startPos = Input.mousePosition;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    endPos = Input.mousePosition;

                    direction = endPos - startPos;
                    direction.Normalize();
                    if (direction.x > 0)
                    {
                        faceSideLeft = false;
                    }
                    else
                    {
                        faceSideLeft = true;
                    }

                    if (endPos.y > startPos.y)
                    {
                        isUp = true;
                    }
                    else
                    {
                        isUp = false;
                    }
                }
            }
        }

        #endregion

        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        SpriteFinder();
        if (onAir==false && isUp)
        {
            miaPhysics.AddForce(direction * jumpForce);
            isUp = false;
        }

    }

    public void UpgradePowerJump()
    {
        jumpForce = jumpForce * 1.5f;
    }

    public void NormalPowerJump()
    {
        jumpForce = jumpForce / 1.5f;
    }

    public void BarrierOn()
    {
       
        isBarrier = true;
        
    }

    public void BarrierOff()
    {
        
        isBarrier = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (faceSideLeft)
            {
                faceSideLeft = false;
            } 
            else if(faceSideLeft == false)
            {
                faceSideLeft = true;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Icecream")
        {
            taken = taken + 1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Step")
        {
            onAir = true;
        }
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Step")
        {
            onAir = false;
        }
    }

    public bool getIsBarrier()
    {
        return isBarrier;
    }

    public void setIsBarrier(bool flag)
    {
        isBarrier = flag;
    }

    private void SpriteFinder()
    {
        if (onAir == false)
        {
            
            miaSpriteRenderer.sprite = (Sprite)Resources.Load<Sprite>("UpMiaGraphics/karakter") as Sprite;
            hatRenderer[wear - 1].flipX = true;
        }
        else if(onAir && faceSideLeft)
        {
           
            miaSpriteRenderer.sprite = (Sprite)Resources.Load<Sprite>("UpMiaGraphics/karakter_jump") as Sprite;
            hatRenderer[wear - 1].flipX = false;
        }
        else if(onAir == true && faceSideLeft == false)
        {
            
            miaSpriteRenderer.sprite = (Sprite)Resources.Load<Sprite>("UpMiaGraphics/karakter_jump_r") as Sprite;
            hatRenderer[wear - 1].flipX = true;
        }

    }

}


