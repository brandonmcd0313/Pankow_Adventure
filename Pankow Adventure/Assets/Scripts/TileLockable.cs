using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;

public class TileLockable : MonoBehaviour
{

    //this tile can lock into recieve=ing tiles for drag and place levels
    public int[] preferredPositons;
    public int positon = -1;
    float radius;
    float xradius, yradius;
    bool locked = false;
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
        
        locked = false;

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
        //TODO: add check for if closest is already locked and a thing todo if it not
        //if there is a piece to lock to, lock to it

        this.transform.position = closest.transform.position;
        locked = true;
        //set position to the position of the reciever
        positon = closest.GetComponent<TileReciever>().getPosition();
        //make this a child of the other piece
        this.transform.parent = closest.transform;
    }
}
        
