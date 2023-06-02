using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Animator anim; bool facingRight = true;
    public GameObject map; bool canJump;
    public float maxX, minX, maxY, minY; 
    GameObject j; public float jumpForce,speed;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        //j is the child of this object
        j = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        print(canJump);

        //jumping stuff
        
        if (!canJump)
        {
            map.transform.position += Vector3.up * Time.deltaTime * speed;
        }


        //move left
        if (Input.GetKey(KeyCode.A))
        {
            //move the map right
            map.transform.position += Vector3.right * Time.deltaTime *speed ;
        }
        //move right
        else if (Input.GetKey(KeyCode.D))
        {
            //move the map left
            map.transform.position += Vector3.left * Time.deltaTime *speed;
        }
        //jump
        else if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(jump());
        }

        if (map.transform.position.x > maxX)
        {
            map.transform.position = new Vector3(maxX, map.transform.position.y, map.transform.position.z);
        }
        if (map.transform.position.x < minX)
        {
            map.transform.position = new Vector3(minX, map.transform.position.y, map.transform.position.z);
        }
        if (map.transform.position.y > maxY)
        {
            map.transform.position = new Vector3(map.transform.position.x, maxY, map.transform.position.z);
            //this is deatha
        }
        if (map.transform.position.y < minY)
        {
            map.transform.position = new Vector3(map.transform.position.x, minY, map.transform.position.z);
           
            
        }

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

        
        //if pressing A/D, isRunning is true
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            anim.SetBool("isRunning", true);
        else anim.SetBool("isRunning", false);

        //if we can jump, isJumping is false
        if (canJump) anim.SetBool("isJumping", false);
        else anim.SetBool("isJumping", true);
    }

    IEnumerator jump()
    {
        canJump = false;
        for (int i = 0; i < 100; i++)
        {
            //move the map down
            map.transform.position += Vector3.down * jumpForce;
            yield return new WaitForSeconds(0.01f);
            canJump = false;

        }

    }
}

