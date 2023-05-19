using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PeiceDeposit : MonoBehaviour
{
    public Sprite[] peiceSprites;
    public Vector2[] peicePoints;
  SpriteRenderer sr;
  int peiceIndex = 0; bool canMove = true;

    //timer
    public float duration;
    public GameObject spacebarClock;
    private Image timerImage;
    private float currentTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
        //this script is on the hand so get the renderer of first child
        sr = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        //shuffle arrays 
        for (int i = 0; i < peiceSprites.Length; i++)
        {
            Sprite tempSprite = peiceSprites[i];
            Vector2 tempPoint = peicePoints[i];
            int randomIndex = Random.Range(i, peiceSprites.Length);
            peiceSprites[i] = peiceSprites[randomIndex];
            peicePoints[i] = peicePoints[randomIndex];
            peiceSprites[randomIndex] = tempSprite;
            peicePoints[randomIndex] = tempPoint;
        }
        

        //timer
        timerImage = spacebarClock.GetComponent<Image>();
        timerImage.type = Image.Type.Filled; // Set the image type to Filled
        timerImage.fillMethod = Image.FillMethod.Radial360; // Set the fill method to Radial360
        timerImage.fillOrigin = (int)Image.Origin360.Top; // Set the fill origin to the top

        currentTime = duration;
    }

    // Update is called once per frame
    void Update()
    {
        //timer
        if(Input.GetKey(KeyCode.Space) && canMove)
        {
            if (currentTime > 0f)
            {
                currentTime -= Time.deltaTime;
                timerImage.fillAmount = (currentTime / duration);
            }
            else
            {
                canMove = false;
                // Timer has reached zero, invoke the method or action
                DepositPeice();
            }
        }
        else
        {
            //reset timer
            timerImage.fillAmount = 0;
            currentTime = duration;
        }
    }
    

    public void DepositPeice()
    {
        //check if the peice is in the right place with 20% error
        if (Mathf.Abs((transform.position.x - peicePoints[peiceIndex].x) / peicePoints[peiceIndex].x) <= 0.2f &&
            Mathf.Abs((transform.position.y - peicePoints[peiceIndex].y) / peicePoints[peiceIndex].y) <= 0.2f)
        {
            //Instatiate good sign

            //add to score 
            
            


        }
        else
        {
            //instatiate bad

            
        }

        //check if peices left
        if(peiceIndex + 1 < peicePoints.Length)
        {
            //move to next peice and reset
            peiceIndex++;
            sr.sprite = null;
            this.GetComponent<HandController>().reset();
        }
        else
        {
            //end game
        }
        
    }

    public void NewPeice()
    {
        
        sr.sprite = peiceSprites[peiceIndex];
        canMove = true;
    }
}
