using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public Transform target;
    public float smoothSpeed = 0.3f;
    public float cameraSpeed = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    private void Update()
    {
       
    }
    private void LateUpdate()
    {

        //sabit kamera hareketleri
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + cameraSpeed, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed);

        //
        if (target.position.y >= transform.position.y)
        {
            Vector3 newPos2 = new Vector3(transform.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos2,smoothSpeed);
        }

   
    }

    
   

}
