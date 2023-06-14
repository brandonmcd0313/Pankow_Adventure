using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Tile : MonoBehaviour
{
    //this script is on the tile.
    //the tile can be clicked and dragged unless it is locked
    //when the tile is released if it is hitting another tiles collider it will snap to that tiles prefrence point
    //each tile has a prefrence point that is the refrence point for the tile it is hitting
    public float radius;
    public float xradius, yradius;
    public bool locked = false;
    bool followMouse = false;
    Vector3 offset = new Vector3(); //offset from piece to mouse pos
   // public bool permalocked;

    void Start()
    {
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
        //if(permalocked)
        //{
         //   return;
        //}
        locked = false;

        //if has a parent, unparent it
        if (transform.parent != null)
        {
            transform.parent = null;
        }

        //if this has a child unchild it
        while (transform.childCount > 0)
            {
                transform.GetChild(0).parent = null;
            }
            //set offset, follow mouse
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            followMouse = true;
       
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

        //transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), -0.5f);


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
        bool axis = false;
        bool direction = false;
        //false is left or down, true is right or up
        GameObject closest = null;
        float closestDist = 1000;
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != this.gameObject && col.gameObject.GetComponent<Tile>() != null)
            {
                float dist = Vector3.Distance(transform.position, col.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = col.gameObject;
                    //check if it is closer on the x axis or the y axis
                    if (Mathf.Abs(transform.position.x - col.transform.position.x) < Mathf.Abs(transform.position.y - col.transform.position.y))
                    {
                        axis = true; //y axis
                        //check if left or right
                        if ((transform.position.y - col.transform.position.y) > 0)
                        {
                            direction = true; //up
                        }
                        else
                        {
                            direction = false; //down
                        }
                    }

                    else
                    {
                        axis = false; // x axis
                                      //check if left or right
                        print(transform.position.x - col.transform.position.x);
                        if ((transform.position.x - col.transform.position.x) > 0)
                        {
                            direction = true; //right
                        }
                        else
                        {
                            direction = false; //left
                        }
                    }
                }
            }
        }

        if (closest == null)
        {
            return;
        }
        //TODO: add check for if closest is already locked and a thing todo if it not
        //if there is a piece to lock to, lock to it
        float Xoffset = closest.GetComponent<Tile>().xradius + xradius;
        float Yoffset = closest.GetComponent<Tile>().yradius + yradius;
        if (axis)
        {
            if (direction) //up
            {
                this.transform.position = new Vector3(this.transform.position.x, closest.transform.position.y + Yoffset);
            }
            else //down
            {
                this.transform.position = new Vector3(this.transform.position.x, closest.transform.position.y - Yoffset);
            }

        }
        else
        {
            if (direction)
            {
                this.transform.position = new Vector3(closest.transform.position.x + Xoffset, this.transform.position.y);
                print(" right");
            }
            else
            {
                this.transform.position = new Vector3(closest.transform.position.x - Xoffset, this.transform.position.y);
                print("left");
            }
        }


        locked = true;
        //make this a child of the other piece
        this.transform.parent = closest.transform;
    }
}
        
