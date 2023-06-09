using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Collider2D target, perfectZone, okZone;

    [SerializeField] GameObject circle;
    SpriteRenderer circleRenderer;
    GameObject end;
    public float score;
    public float perfectScore;
    // Start is called before the first frame update
    void Start()
    {
        end = GameObject.Find("EndGame");
        circleRenderer = circle.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if target is contained in perfect zone
        if (perfectZone.bounds.Contains(target.bounds.min) && perfectZone.bounds.Contains(target.bounds.max))
        {
            score += 3.5f;
            circleRenderer.color = Color.green;
        }
        //check if target is contained in ok zone
        else if (okZone.bounds.Contains(target.bounds.min) && okZone.bounds.Contains(target.bounds.max))
        {
            // create a ratio by compare the size of the perfect zone and the ok zone (based on bounds)
            float ratio = (perfectZone.bounds.size.x / okZone.bounds.size.x);
            //perfect ratio = 0.567

            
            score += (1.76f * ratio);
            circleRenderer.color = Color.yellow;
        }
        else
        {
            score += 0;
            circleRenderer.color = Color.red;
        }

        perfectScore += 1; //this a "100%" score

        
        end.GetComponent<EndingGame>().grade = (int) (Mathf.Pow((score / perfectScore),(0.3f)) * 100);
        if (end.GetComponent<EndingGame>().grade > 100)
        {
            end.GetComponent<EndingGame>().grade = 100;
        }
        
    }

    
}
