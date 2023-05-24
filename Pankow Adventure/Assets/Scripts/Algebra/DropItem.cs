using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DropItem : MonoBehaviour
{
    char radical = Convert.ToChar(8730);
    public bool rational = false;
    
    public float gameTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        //50% chance to be rational
        if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
        {
            rational = true;
            //set text to a number 1-100
            this.transform.GetChild(0).GetComponent<TextMeshPro>().text = UnityEngine.Random.Range(1, 100).ToString();
        }
        else
        {
            rational = false;
            //set text to a number 1-100
            int num = UnityEngine.Random.Range(2, 100);
            this.transform.GetChild(0).GetComponent<TextMeshPro>().text = radical + num.ToString();
            //if a square number set to rational
            if (Math.Sqrt(num) % 1 == 0)
            {
                rational = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        //check if out of screen bounds
        if (this.transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

   void OnTriggerEnter2D(Collider2D other)
    {
        //if the other objectis also a drop item
        if(other.gameObject.GetComponent<DropItem>() != null)
        {
            
            //check if this is the older object
            if (this.gameTime > other.gameObject.GetComponent<DropItem>().gameTime)
            {
                //order this up
                GetComponent<SpriteRenderer>().sortingOrder = other.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 3;
                this.transform.GetChild(0).GetComponent<TextMeshPro>().sortingOrder = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
            else
            {
                //add  velocity left or rightward
                if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
                {
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(1, -1);
                }
                else
                {
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -1);
                }
            }



        }
    }
}
