using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewTile : MonoBehaviour
{

    //this tile can lock into recieve=ing tiles for drag and place levels
    public int preferredPositon;
    [Tooltip("left,right,assign,jump")]
    AudioSource aud;
    public AudioClip click;
    public string actionType;
    public int positon = -1;
    float xradius, yradius; float radius;
    bool locked = false;
    bool followMouse = false;
    Vector3 offset;
    GameObject reciverSpot;
    // public bool permalocked;

    void Start()
    {
        aud = Camera.main.GetComponent<AudioSource>();
        this.GetComponent<SpriteRenderer>().sortingOrder = 0;
        Cursor.lockState = CursorLockMode.Confined;
        //if x and y unset set them to radius
        //set radius to half  scale of the object
        xradius = transform.localScale.x / 2;
        yradius = transform.localScale.y / 2;
        radius = Mathf.Max(xradius, yradius);
    }
    void Update()
    {
        
        if (followMouse)
        {

            if (!Input.GetKey(KeyCode.Mouse0) && followMouse)
            {
                OnMouseUp();
            }
            if (followMouse)
            {
                Vector3 mousePo = Input.mousePosition;
                mousePo.z = 1; //Camera.main.nearClipPlane;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePo);
                transform.position = worldPos;
            }
            //if mouse goes off-screen, drop piece and snap to on-screen position
            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if ((mousePos.x > 1 || mousePos.y > 1) && followMouse)
            {
                followMouse = false;
                transform.position = new Vector3(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y), -0.5f);
            }
            if ((mousePos.x < 0 || mousePos.y < 0) && followMouse)
            {
                followMouse = false;
                transform.position = new Vector3(Mathf.Ceil(transform.position.x), Mathf.Ceil(transform.position.y), -0.5f);
            }
        }
    }

    private void OnMouseDown()
    {
        //set sorting layer to 3
        this.GetComponent<SpriteRenderer>().sortingOrder = 3;
        locked = false;
        if (reciverSpot != null)
        {
            reciverSpot.GetComponent<NewReciver>().unlockSpot();
        }

        //if has a parent, unparent it
        if (transform.parent != null)
        {
            transform.parent = null;
        }


        //set offset, follow mouse
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        followMouse = true;

    }


    private void OnMouseUp()
    {

        //click to nearest valid position, don't follow mouse
        followMouse = false;
        //lock to other game piece
        LockObject();
    }

    private void LockObject()
    {

        if (locked)
        {
            return;
        }

        //check if there is a piece to lock to within one unit
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius * 1.25f);
        //find closest collider
        GameObject closest = null;
        float closestDist = 1000;
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != this.gameObject && col.gameObject.GetComponent<NewReciver>() != null)
            {
                float dist = Vector3.Distance(transform.position, col.transform.position);
                if (dist < closestDist)
                {

                    closestDist = dist;
                    closest = col.gameObject;

                }
            }
        }

        if (closest == null)
        {
            positon = -1; 
            //if on right half of the screen unchild from main cam
            if (transform.position.x > Camera.main.transform.position.x)
            {
                this.transform.parent = null;
            }
            return;
        }
        //if there is a closest collider, lock to it
        reciverSpot = closest;

        if (reciverSpot.GetComponent<NewReciver>().locked)
        {
            reciverSpot = null;
            return;
        }
        //check if this object is denied to be here
        else if (reciverSpot.GetComponent<NewReciver>().denySpot == preferredPositon)
        {
            print("denied");
            reciverSpot = null;
            return;
        }
        else
        {
            aud.PlayOneShot(click);
            //set sorting layer to 3
            this.GetComponent<SpriteRenderer>().sortingOrder = 3;
            reciverSpot.GetComponent< NewReciver>().lockSpot(actionType);

            this.transform.position = closest.transform.position;
            locked = true;
            //set position to the position of the reciever
            positon = closest.GetComponent<NewReciver>().getPosition();
            //make this a child of the other piece
            this.transform.parent = closest.transform;
            
            //set to child of main camera
            this.transform.parent = Camera.main.transform;
        }

    }
}

