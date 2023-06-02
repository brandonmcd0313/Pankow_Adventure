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
    bool canJump = false, setJump = false;
    public GameObject j; 
    public bool facingRight = true; bool dead = false;

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
        if(transform.position.y < -13f && !dead)
        {
            StartCoroutine(death());
            dead = true;
        }
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
        if (Input.GetKey(KeyCode.Space))
        {

            space.Invoke();
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("isRunning", false);
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

        //if we can jump, isJumping is false
        if (canJump) anim.SetBool("isJumping", false);
        else anim.SetBool("isJumping", true);
        

    }
    

    void left()
    {
        transform.position -=
                     new Vector3(speed * Time.deltaTime, 0);
        anim.SetBool("isRunning", true);
        if (facingRight)
        {
            facingRight = false;
            //flip the x, leave the y and z alone
            transform.localScale = new Vector3(
                -transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z);
        }
    }
    void right()
    {
        transform.position +=
                     new Vector3(speed * Time.deltaTime, 0);
        anim.SetBool("isRunning", true);
        anim.SetBool("isRunning", true);
        if (!facingRight)
        {
            facingRight = true;
            //flip the x, leave the y and z alone
            transform.localScale = new Vector3(
                -transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z);
        }
    }

    void jump()
    {
        print("jump");
        if (!canJump || !setJump) return;
        print("jumping");
        anim.SetBool("isRunning", false);
        rb2d.AddForce(new Vector3(0, jumpForce));
        //canJump = false;
    }

    void assign()
    {
        print("assign");
        //do nothing
        setJump = true;
    }


    public void SetValue(string input, string action)
    {
        // Switch case checking if input is "a", "d", "space", or "start"
        switch (input)
        {
            case "a":
                a.RemoveAllListeners();
                switch (action)
                {
                    case "left":
                        a.AddListener(left);
                        d.RemoveListener(left);
                        space.RemoveListener(left);
                        start.RemoveListener(left);
                        break;
                    case "right":
                        a.AddListener(right);
                        d.RemoveListener(right);
                        space.RemoveListener(right);
                        start.RemoveListener(right);
                        break;
                    case "assign":
                        a.AddListener(assign);
                        d.RemoveListener(assign);
                        space.RemoveListener(assign);
                        start.RemoveListener(assign);
                        break;
                    case "jump":
                        a.AddListener(jump);
                        d.RemoveListener(jump);
                        space.RemoveListener(jump);
                        start.RemoveListener(jump);
                        break;
                }
                break;

            case "d":
                d.RemoveAllListeners();
                switch (action)
                {
                    case "left":
                        d.AddListener(left);
                        a.RemoveListener(left);
                        space.RemoveListener(left);
                        start.RemoveListener(left);
                        break;
                    case "right":
                        d.AddListener(right);
                        a.RemoveListener(right);
                        space.RemoveListener(right);
                        start.RemoveListener(right);
                        break;
                    case "assign":
                        d.AddListener(assign);
                        a.RemoveListener(assign);
                        space.RemoveListener(assign);
                        start.RemoveListener(assign);
                        break;
                    case "jump":
                        d.AddListener(jump);
                        a.RemoveListener(jump);
                        space.RemoveListener(jump);
                        start.RemoveListener(jump);
                        break;
                }
                break;

            case "space":
                space.RemoveAllListeners();
                switch (action)
                {
                    case "left":
                        space.AddListener(left);
                        a.RemoveListener(left);
                        d.RemoveListener(left);
                        start.RemoveListener(left);
                        break;
                    case "right":
                        space.AddListener(right);
                        a.RemoveListener(right);
                        d.RemoveListener(right);
                        start.RemoveListener(right);
                        break;
                    case "assign":
                        space.AddListener(assign);
                        a.RemoveListener(assign);
                        d.RemoveListener(assign);
                        start.RemoveListener(assign);
                        break;
                    case "jump":
                        space.AddListener(jump);
                        a.RemoveListener(jump);
                        d.RemoveListener(jump);
                        start.RemoveListener(jump);
                        break;
                }
                break;

            case "start":
                start.RemoveAllListeners();
                switch (action)
                {
                    case "left":
                        start.AddListener(left);
                        a.RemoveListener(left);
                        d.RemoveListener(left);
                        space.RemoveListener(left);
                        break;
                    case "right":
                        start.AddListener(right);
                        a.RemoveListener(right);
                        d.RemoveListener(right);
                        space.RemoveListener(right);
                        break;
                    case "assign":
                        start.AddListener(assign);
                        a.RemoveListener(assign);
                        d.RemoveListener(assign);
                        space.RemoveListener(assign);
                        setJump = true;
                        break;
                    case "jump":
                        start.AddListener(jump);
                        a.RemoveListener(jump);
                        d.RemoveListener(jump);
                        space.RemoveListener(jump);
                        break;
                }
                break;
        }
    }
    
    IEnumerator death()
    {
        //flash character sprite
       
            //fade out
            for(float j = 0; j < 1; j += 0.04f)
            {

               
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - ((j) / 1));
            yield return new WaitForSeconds(0.03f);
        }
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        this.transform.position = new Vector2(-2f, -0.5f);
        dead = false;
        //fade back in
        for (float j = 0; j < 1; j += 0.04f)
            {

            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, j / 1);
            yield return new WaitForSeconds(0.03f);
        }

       
    }
 }



