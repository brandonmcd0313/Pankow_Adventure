using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public GameObject flame;
    public GameObject end;

    void Start()
    {
        end = GameObject.Find("EndGame");
    }
    private void OnMouseDown()
    {
        //if not enabled automatic 0
        if(!flame.GetComponent<SpriteRenderer>().enabled)
        {
            end.GetComponent<EndingGame>().intro = "As you know, no fire means points./nYou got 0 points./nResulting in a grade of GRADE%";
            end.GetComponent<EndingGame>().grade = 0; 
           end.GetComponent<EndingGame>().EndGame();
        }
        //if flame animator is not set to white
        if(flame.GetComponent<FireController>().getAnimatorType() != "white")
        {
            //grade is 50%
            end.GetComponent<EndingGame>().intro = "You needed to add the powder in order to pass, as you only completed half of the " +
                "procedure you will only recieve half of the points/nResulting in a grade of GRADE%";
            end.GetComponent<EndingGame>().grade = 50;
            end.GetComponent<EndingGame>().EndGame();
        }
        //set this sprite to be more gray
        this.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
        //find the offset from ideal colour (0.7 green)
        Color c = flame.GetComponent<SpriteRenderer>().color;
        float ideal = 0.7f; float current = c.g;
        //score will be # of shades away from ideal value
        end.GetComponent<EndingGame>().score = Mathf.RoundToInt(Mathf.Abs(ideal - current) / 0.15f);
        //use to calculate a grade
        //every shade off = -10%
        end.GetComponent<EndingGame>().grade = 100 - (end.GetComponent<EndingGame>().score * 10);
        end.GetComponent<EndingGame>().EndGame();
        
    }
}
