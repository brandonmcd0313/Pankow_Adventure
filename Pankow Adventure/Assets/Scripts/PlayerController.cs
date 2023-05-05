using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d; //reference to Rigidbody2D component
    Animator anim; //reference to Animator component
    SpriteRenderer sr; Sprite idleUp, idleDown, idleSide;
    bool right; bool up = true; bool axis = true;
    public static bool canMove = true;
    public float speed = 8f;

    Quaternion original;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //hook up rb2d
        anim = GetComponent<Animator>(); //hook up anim
        sr = GetComponent<SpriteRenderer>(); //hook up sr
        original = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //make sure it has same rotation
        transform.rotation = original;
        if (!canMove)
        {
            return;
        }

        //left/right movement
        if (Input.GetAxisRaw("Horizontal") > 0) //right
        {
            transform.position +=
                new Vector3(speed * Time.deltaTime, 0);

            right = true;

        }
        if (Input.GetAxisRaw("Horizontal") < 0) //left
        {
            transform.position -=
                new Vector3(speed * Time.deltaTime, 0);

            right = false;
        }

        //flipping right
        if (right)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }

        //up down
        if (Input.GetAxisRaw("Vertical") > 0) //up
        {
            transform.position +=
                new Vector3(0, speed * Time.deltaTime);

            up = true;
        }
        if (Input.GetAxisRaw("Vertical") < 0) //down
        {
            transform.position -=
                new Vector3(0, speed * Time.deltaTime);

            up = false;
        }

        //animation stuff
       
        //if not moving
        if (Input.GetAxisRaw("Horizontal") == 0 &&
            Input.GetAxisRaw("Vertical") == 0)
        {
            if (axis) //up/down
            {
                if (up)
                {
                    anim.Play("idleUp");
                }
                else
                {

                    anim.Play("idleDown");
                }
            }
            else //right/left
            {
                anim.Play("idleRight");
            }
            return;
        }
        //IF MOVING
        else
        {
            anim.enabled = true;
            print("axis: " + axis);
            if (axis) //up/down
            {

                if (up)
                {
                    anim.Play("forward");
                }
                else
                {
                    anim.Play("back");
                }

            }
            else //right/left
            {
                anim.Play("right");
            }

        }
        //check axis based on which abs value is large
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) >
            Mathf.Abs(Input.GetAxisRaw("Vertical")))
        {
            axis = false; //right/left
        }
        else
        {
            axis = true; //up/down
        }

    }
} 


    

