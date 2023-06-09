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

        float finalscore = score * modifier;

        string endMsg = "*smacks lips* that soup was... /n";
        //based on score add descriptor
        if (finalscore < 0)
        {
            endMsg += "BLOODY DISGUSTING! GET OUT OFF THIS CLASSROOM!";
        }
        else if (finalscore < 20)
        {
            endMsg += "FLAVOURLESS FILTH. DISAPOINTING!";
        }
        else if (finalscore <= 40)
        {
            endMsg += "BLAND AND UNORIGINAL!";
        }
        else if (finalscore < 60)
        {
            endMsg += "uneventful.";
        }
        else if (finalscore <= 90)
        {
            endMsg += "Nothing to write home about, yet not the worst today.";
        }
        else
        {
            endMsg += "DELIGHTFUL! BEST SOUP I'VE EVER HAD!";
        }
        endMsg += "/nA well deserved GRADE%";
        
        //make sure score is between 0 and 100
        if (finalscore > 100)
        {
            finalscore = 100;
        }
        if (finalscore < 0)
        {
            finalscore = 0;
        }
        end.GetComponent<EndingGame>().grade = (int)finalscore;
        end.GetComponent<EndingGame>().intro = endMsg;

        end.GetComponent<EndingGame>().EndGame();

    }
}
