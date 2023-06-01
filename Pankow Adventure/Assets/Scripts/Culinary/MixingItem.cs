using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class MixingItem : MonoBehaviour
{
    public Sprite mixedSprite;
     float radius; float speed;
    float xradius, yradius; bool stirring, cooling;
    bool followMouse = false;
    Vector3 offset = new Vector3(); //offset from piece to mouse pos
    
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
      
        followMouse = false;

        //check if in the mixing area
        //check if there is a piece to lock to within one unit
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius*1.1f);
        //find closest collider
        GameObject closest = null;
        float closestDist = 1000;
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != this.gameObject && col.gameObject.GetComponent<MixingReciver>() != null)
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
           return;
        }
        else
        {
            //add to mixing area
            closest.GetComponent<MixingReciver>().addItem(this.gameObject);
        }
    }

    IEnumerator stir(float lowSpeed, float highSpeed, float goal)
    {

        if (stirring)
        {
            if (speed < highSpeed * 2)
            {
                speed *= 1.25f;
            }
            yield break;
        }
        if (!cooling)
        {
            speed = UnityEngine.Random.Range(lowSpeed, highSpeed);
        }
        stirring = true;
        //rotate object slowly and a random amount for 2 - 3 seconds
        float time = 0;
       
        float rotationZ = transform.rotation.eulerAngles.z; // Store the initial z-axis rotation
        while (time < goal)
        {
            //as time goes on, slow rotation
            
            rotationZ += (speed * Time.deltaTime);
            //speeds up at start, slows at end.
            time += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, rotationZ);
            print((speed * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
        stirring = false;
        StartCoroutine(cooldown(lowSpeed, highSpeed));
    }

    IEnumerator cooldown(float lowSpeed, float highSpeed)
    {
        //just wait before reassining speed
        cooling = true;
        yield return new WaitForSeconds(1f);
        if (!stirring)
        {

            speed = UnityEngine.Random.Range(lowSpeed, highSpeed);
        }
        cooling = false;

    }

    public void stirObj(float ls, float hs, float g)
    {
        StartCoroutine(stir(ls,hs,g));
    }
}
