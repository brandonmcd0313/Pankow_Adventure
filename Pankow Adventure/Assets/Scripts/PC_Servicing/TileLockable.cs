using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileLockable : MonoBehaviour
{
    GameObject end;
    //this tile can lock into recieve=ing tiles for drag and place levels
   AudioSource aud;
    public AudioClip click;
    public int[] preferredPositons;
    public int positon = -1;
    float radius; 
    float xradius, yradius;
    bool locked = false;
    bool followMouse = false;
    Vector3 offset = new Vector3(); //offset from piece to mouse pos
    GameObject reciverSpot;
   // public bool permalocked;

    void Start()
    {
        //aud is on main camera
        aud = Camera.main.GetComponent<AudioSource>();
        end = GameObject.Find("EndGame");
        end.GetComponent<EndingGame>().possible = 6;
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
        
        locked = false;
        if(reciverSpot != null)
        {
            reciverSpot.GetComponent<TileReciever>().unlockSpot();
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
            if (col.gameObject != this.gameObject && col.gameObject.GetComponent<TileReciever>() != null)
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
            positon = -1; return; 
        }
        //if there is a closest collider, lock to it
        reciverSpot = closest;

        if (reciverSpot.GetComponent<TileReciever>().locked)
        {
            reciverSpot = null;
            return;
        }
        //check if this object is denied to be here
        else if(reciverSpot.GetComponent<TileReciever>().denySpot == preferredPositons[0])
        {
            print("denied");
            reciverSpot = null;
            return;
        }
        else
        {
            reciverSpot.GetComponent<TileReciever>().lockSpot();
            aud.PlayOneShot(click);
            this.transform.position = closest.transform.position;
            locked = true;
            //set position to the position of the reciever
            positon = closest.GetComponent<TileReciever>().getPosition();
            //make this a child of the other piece
            this.transform.parent = closest.transform;
        }

        //check every object to see if they are all locked
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("TileR");
        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<TileReciever>().locked == false)
            {
                return;
            }
        }
        //if made it here game is over!
        foreach (GameObject obj in allObjects)
        {
            //get the score
            foreach (int pos in obj.transform.GetChild(0).GetComponent<TileLockable>().preferredPositons)
            {
                if (pos == obj.GetComponent<TileReciever>().getPosition())
                {
                    end.GetComponent<EndingGame>().score++;
                }
            }

        }
        if (end.GetComponent<EndingGame>().score != end.GetComponent<EndingGame>().possible)
        {
            end.GetComponent<EndingGame>().grade = 0;
            end.GetComponent<EndingGame>().intro = "The PC doesn't work, how did you mess that up./nYou get GRADE%";
        }
        else
        {
            end.GetComponent<EndingGame>().grade = 100;
            end.GetComponent<EndingGame>().intro = "Nice Job! The PC works!/n You get GRADE%";
        }
         end.GetComponent<EndingGame>().EndGame();
    }
}
        
