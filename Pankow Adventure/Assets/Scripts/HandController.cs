using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Vector2[] goalPositions;
    Vector2 goalPosition; public Vector2 startingPoint;
    int index = 0;
    public float maxY, minY;
    public float speed = 0.1f;
    public float playerEffect = 0.1f;
    public GameObject clock;
    bool onscreen = false;
    void Start()
    {
        startingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        print(goalPosition);
        //check if the child has a null sprite and if 13 < x < 14
        if (transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == null && transform.position.x > 13 && transform.position.x < 14)
        {
            print("begin");
            onscreen = true;
            begin();
        }
        
        //move this object towards the goal position
        if(goalPosition == startingPoint || transform.position.x > 8)
        {

            transform.position = Vector2.MoveTowards(transform.position, goalPosition, 3 * speed);
        }
        else
        {

            transform.position = Vector2.MoveTowards(transform.position, goalPosition, speed);
        }
        
        //allow the player to also move the object ON CLICK ONLY
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.position += new Vector3(playerEffect, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.transform.position += new Vector3(-playerEffect, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.transform.position += new Vector3(0, playerEffect, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.transform.position += new Vector3(0, -playerEffect, 0);
        }


        //outofbounds
        if (transform.position.y > maxY )
        {
            transform.position = new Vector2(transform.position.x, maxY);
        }
        else if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, minY);
        }


        //if this object has reached the goal position
        if (Mathf.Abs((transform.position.x - goalPosition.x) / goalPosition.x) <= 0.1f &&
            Mathf.Abs((transform.position.y - goalPosition.y) / goalPosition.y) <= 0.1f)
            {
           
            //increment the index
            index++;

            //if the index is greater than the length of the goal positions array
            if (index >= goalPositions.Length)
            {
                //reset the index to 0
                index = 0;
                //shuffle the array
                for (int i = 0; i < goalPositions.Length; i++)
                {
                    int rnd = UnityEngine.Random.Range(0, goalPositions.Length);
                    Vector2 temp = goalPositions[rnd];
                    goalPositions[rnd] = goalPositions[i];
                    goalPositions[i] = temp;
                }
            }
            goalPosition = goalPositions[index];
        }
    }

    public void setGoalPosition(Vector2 goalPos)
    {
        goalPosition = goalPos;
    }

    public void reset()
    {
        goalPosition = startingPoint;
        clock.GetComponent<ClockController>().ResetTimer();
    }

    void begin()
    {
        //start timer
        clock.GetComponent<ClockController>().StartTimer();
        //run new peice
       GetComponent<PeiceDeposit>().NewPeice();
    }
}
    


        
        
    

