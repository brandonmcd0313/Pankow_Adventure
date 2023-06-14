using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCamController : MonoBehaviour
{
   public float playerEffect;
  public  float zoomScale;
 public   float maxSize, minSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        //allow the player to also move the object ON CLICK ONLY
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += new Vector3(playerEffect, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += new Vector3(-playerEffect, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position += new Vector3(0, playerEffect, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position += new Vector3(0, -playerEffect, 0);
        }
        
    
        
        //zoom stuff
        //use scrollwheel to scale up and down the object
      

        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
                this.transform.localScale += new Vector3(zoomScale * Input.mouseScrollDelta.y, zoomScale * Input.mouseScrollDelta.y, 0);
        }

        //bounds
        
        if (this.transform.localScale.x > maxSize)
        {
            this.transform.localScale = new Vector3(maxSize, maxSize, 0);
        }

        if (this.transform.localScale.x < minSize)
        {
            this.transform.localScale = new Vector3(minSize, minSize, 0);
        }
    }
}
