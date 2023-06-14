using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReportCardManager : MonoBehaviour
{
    public SpriteRenderer card, cardback;
    public TextMeshPro classes, scores, grades;
    static bool declared;
    public static string[] classList; bool showing;
   public  static int[] scoreList; public GameObject blocker;
    // Start is called before the first frame update
    void Start()
    {
        HideReportCard();
        if(declared)
        { return;  }
        //set classList based on program
        if(PlayerController.program == "CTE")
        {
            classList = new string[3];
            scoreList = new int[3];
            classList[0] = "Culinary";
            classList[1] = "Early Childhood Deveolpment";
            classList[2] = "TV Broadcast Media";
        }
        else if(PlayerController.program == "MST")
        {
            classList = new string[4];
            scoreList = new int[4];
            
            classList[0] = "Algebra";
            classList[1] = "Chemistry";
            classList[2] = "PC Servicing";
            classList[3] = "Game Design";
        }
        else if(PlayerController.program == "PPA")
        {
            classList = new string[2];
            scoreList = new int[2];
            classList[0] = "Theatre Acting";
            classList[1] = "Dance";
        }

        for (int i = 0; i < scoreList.Length; i++)
        {
            
                scoreList[i] = -1;
        }

        declared = true;
    }

    private void Update()
    {
        this.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 1, 0);
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (showing)
            {
                HideReportCard();
            }
            else
            {
                ShowReportCard();
            }
        }
    }
    
    void ShowReportCard()
    {
        showing = true;
        card.enabled = true;
        cardback.enabled = true;
        classes.text = "";
        classes.text = "Class Name\n";
        foreach(string s in classList)
        {
            classes.text += s;
            classes.text += "\n\n";
        }
        classes.enabled = true;
        scores.text = "Grade\n";
        foreach (int s in scoreList)
        {
            if(s == -1)
            {

                scores.text += "\n\n\n";

            }
            else
            {

                scores.text += s;
                scores.text += "\n\n";
            }
        }
        scores.enabled = true;
        grades.text = "\n";
        foreach (int s in scoreList)
        {
            if (s == -1)
            {

                grades.text += "\n\n\n";

            }
            else
            {
                //find a letter grade based on score A+ A A-...
                grades.text += GetLetterGrade(s) + "\n\n";
            }
        }
        grades.enabled = true;

        //if none of the grades are -1
        if(Array.IndexOf(scoreList, -1) != -1)
        {
            blocker.GetComponent<Collider2D>().isTrigger = true;
        }
                
    }

    void HideReportCard()
    {
        showing = false;
        card.enabled = false;
        cardback.enabled = false;
        classes.enabled = false;
        scores.enabled = false;
        grades.enabled = false;
    }
    string GetLetterGrade(int score)
    {
        if (score >= 97)
        {
            return "A+";
        }
        else if (score >= 93)
        {
            return "A";
        }
        else if (score >= 90)
        {
            return "A-";
        }
        else if (score >= 87)
        {
            return "B+";
        }
        else if (score >= 83)
        {
            return "B";
        }
        else if (score >= 80)
        {
            return "B-";
        }
        else if (score >= 77)
        {
            return "C+";
        }
        else if (score >= 73)
        {
            return "C";
        }
        else if (score >= 70)
        {
            return "C-";
        }
        else if (score >= 67)
        {
            return "D+";
        }
        else if (score >= 63)
        {
            return "D";
        }
        else
        {
            return "F";
        }
    }

}
