using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PeiceDeposit : MonoBehaviour
{
    GameObject end;
    AudioSource aud; 
    public AudioClip pos, neg;
    public Sprite[] peiceSprites;
    public Vector2[] peicePoints;
  SpriteRenderer sr; bool running = true;
  int peiceIndex = 0; bool canMove = true;
   public GameObject particleEffect;
    //timer
    public float duration;
    public GameObject spacebarClock;
    private Image timerImage;
    private float currentTime;
    
    // Start is called before the first frame update
    void Start()
    {
        end = GameObject.Find("EndGame");
        aud = this.GetComponent<AudioSource>();
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

        end.GetComponent<EndingGame>().possible = 7;
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
        if(!running)
        {
            return;
        }
       
        //check if the peice is in the right place with 20% error
        if (Mathf.Abs((transform.position.x - peicePoints[peiceIndex].x) / peicePoints[peiceIndex].x) <= 0.2f &&
            Mathf.Abs((transform.position.y - peicePoints[peiceIndex].y) / peicePoints[peiceIndex].y) <= 0.2f)
        {
            //Instatiate particle effect and set to green

            ParticleSystem.MainModule settings = particleEffect.GetComponent<ParticleSystem>().main;
            settings.startColor = Color.green;

            //instatiate at hand position
            Instantiate(particleEffect, new Vector3(this.transform.position.x, 
                this.transform.position.y + 1.6f, this.transform.position.z), Quaternion.Euler(-90, 0, 0));


            //add to score 

            end.GetComponent<EndingGame>().score++;

            aud.PlayOneShot(pos);
        }
        else
        {
            //instatiate bad
            ParticleSystem.MainModule settings = particleEffect.GetComponent<ParticleSystem>().main;
            settings.startColor = Color.red;

            //instatiate at hand position
            Instantiate(particleEffect, new Vector3(this.transform.position.x, 
                this.transform.position.y + 1.6f, this.transform.position.z), Quaternion.Euler(-90, 0, 0));

            aud.PlayOneShot(neg);
        }

        //check if peices left
        if (peiceIndex < peicePoints.Length - 1)
        {
            //move to next peice and reset
            peiceIndex++;
            sr.sprite = null;
            this.GetComponent<HandController>().reset();
        }
        else
        {
            //end game
            
            running = false;
            // 6/7 = 90 5/7 = 80
            end.GetComponent<EndingGame>().grade = 100 - ((end.GetComponent<EndingGame>().possible - end.GetComponent<EndingGame>().score) * 10);
            end.GetComponent<EndingGame>().EndGame();
            
        }
        
    }

    public void NewPeice()
    {

        sr.sprite = peiceSprites[peiceIndex];
        //if index out of bounds
        
        canMove = true;
    }
}
