using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerControllerB : MonoBehaviour
{
    Rigidbody2D rb2d; //reference to Rigidbody2D component
    Animator anim; //reference to Animator component
    public float speed = 8f;
    public float jumpForce = 450f;
    bool canJump = false, canMove = true;
    public GameObject j;
    public bool facingRight = true;

    public UnityEvent d, a, space, start;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        j = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //C:\Users\bamcd\OneDrive\Desktop\Pankow_Adventure\Pankow Adventure\Assets\Sprites

        //lock object rotation
        transform.rotation = Quaternion.Euler(0, 0, 0);
        //left/right movement
        if (Input.GetKey(KeyCode.D)) //right
        {
            d.Invoke();
        }
        if (Input.GetKey(KeyCode.A)) //left
        {
            a.Invoke();
        }

        //flip left/right
        //if facing right and press left, flip
        if (facingRight && Input.GetKeyDown(KeyCode.A))
        {
            facingRight = false;
            //flip the x, leave the y and z alone
            transform.localScale = new Vector3(
                -transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z);
        }

        //if not facing right and press right, flip
        else if (!facingRight && Input.GetKeyDown(KeyCode.D))
        {
            facingRight = true;
            //flip the x, leave the y and z alone
            transform.localScale = new Vector3(
                -transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z);
        }


        //jump
        canJump = false;
        //check if j is contained in a collider with Platfrom tag
        Collider2D[] colliders = Physics2D.OverlapCircleAll(j.transform.position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Platform")
            {
                canJump = true;
                break;
            }
        }
        //animation stuff
        //if pressing A/D, isRunning is true
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            anim.SetBool("isRunning", true);
        else anim.SetBool("isRunning", false);

        //if we can jump, isJumping is false
        if (canJump) anim.SetBool("isJumping", false);
        else anim.SetBool("isJumping", true);

        //the moment they press the space bar, apply up force
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            space.Invoke();
        }
    }

    void left()
    {
        transform.position -=
                     new Vector3(speed * Time.deltaTime, 0);
    }
    void right()
    {
        transform.position +=
                     new Vector3(speed * Time.deltaTime, 0);
    }

    void jump()
    {
        rb2d.AddForce(new Vector3(0, jumpForce));
    }

    void assign()
    {
        //do nothing
    }


    public void setValue(string input, string action)
    {
        //switch case checking if input is d,a, or "space"
        switch (input)
        {
            case "a":
                switch (action)
                {
                    case "left":
                        a.AddListener(left);
                        break;
                    case "right":
                        a.AddListener(right);
                        break;
                    case "assign":
                        a.AddListener(assign);
                        break;
                    case "jump":
                        a.AddListener(jump);
                        break;

                }
                break;
            case "d":
                switch (action)
                {
                    case "left":
                        d.AddListener(left);
                        break;
                    case "right":
                        d.AddListener(right);
                        break;
                    case "assign":
                        d.AddListener(assign);
                        break;
                    case "jump":
                        d.AddListener(jump);
                        break;

                }
                break;

            case "space":
                switch (action)
                {
                    case "left":
                        space.AddListener(left);
                        break;
                    case "right":
                        space.AddListener(right);
                        break;
                    case "assign":
                        space.AddListener(assign);
                        break;
                    case "jump":
                        space.AddListener(jump);
                        break;

                }
                break;
            case "start":
                switch (action)
                {
                    case "left":
                        start.AddListener(left);
                        break;
                    case "right":
                        start.AddListener(right);
                        break;
                    case "assign":
                        start.AddListener(assign);
                        break;
                    case "jump":
                        start.AddListener(jump);
                        break;

                }
                break;
        }
            

    }
    }



