using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupScoreCalc : MonoBehaviour
{
    public static int stirCount;
    public static int itemCount;
    public static int score;
    float modifier; GameObject end;
    
    public void CalcScore()
    {
        end = GameObject.Find("EndGame");
        modifier = 1f;
        //compare stir count to # of items
        if(stirCount < itemCount * 2)
        {
            modifier = 0.9f;
            if (stirCount < itemCount)
            {
                modifier = 0.8f;
            }
        }

       float  finalscore = score * modifier;
        //make sure score is between 0 and 100
        if (finalscore > 100)
        {
            finalscore = 100;
        }
        if (finalscore < 0)
        {
            finalscore = 0;
        }
        end.GetComponent<EndingGame>().score = finalscore;
    }
}
