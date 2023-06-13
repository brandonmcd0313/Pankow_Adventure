using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextInput : MonoBehaviour
{
    public GameObject exampleObj, coverObj, inputObj;
    TextMeshProUGUI exampleText, cover, input; //cover is the same colour as the background
    float score, perfectScore, accuracy; GameObject end; 
    // Start is called before the first frame update
    void Start()
    {
        end = GameObject.Find("EndGame");
        exampleText = exampleObj.GetComponent<TextMeshProUGUI>();
        cover = coverObj.GetComponent<TextMeshProUGUI>();
        input = inputObj.GetComponent<TextMeshProUGUI>();
        perfectScore = exampleText.text.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 1)
        {
            return;
        }
        //take a key input and add it to input
        //if input is equal to ideal, then do something
        // Check for key input
        if (Input.anyKeyDown)
        {
            
            foreach (char c in Input.inputString)
            {
                if (c == ' ' && input.text.Length == 0)
                { return; }
                // Check if the input is a letter or punctuation or a space
                if (char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsWhiteSpace(c))
                {

                    // Add the input character to the string
                    input.text += c;
                    //add cover
                    cover.text += exampleText.text[input.text.Length - 1];
                    //check if this char equals the same char in ideal
                    if (input.text[input.text.Length - 1] == exampleText.text[input.text.Length - 1])
                    {
                        score++;
                        input.color = Color.green;
                    }
                    else
                    {
                        
                        score += 0.75f;
                       
                        input.color = Color.red;
                    }
                }

                //if backspace
                else if (c == '\b')
                {
                    score -= 1.5f;
                    // Remove the last character from the string
                    input.text = input.text.Substring(0, input.text.Length - 1);
                    //remove cover
                    cover.text = cover.text.Substring(0, cover.text.Length - 1);
                }
                if(input.text.Length == exampleText.text.Length)
                {
                    //end of game

                    //for this one score will be an accuracy percent
                    //check how many characters are incorrect
                   for(int i = 0; i < exampleText.text.Length; i++)
                    {
                        if (input.text[i] != exampleText.text[i])
                        {
                            accuracy++;
                        }
                    }
                    end.GetComponent<EndingGame>().score = (int)(((perfectScore - accuracy) / perfectScore) * 100);
                  int grade = (int)((score / perfectScore) * 100);
                    //clamp to between 0 and 100
                    end.GetComponent<EndingGame>().grade = Mathf.Clamp(grade, 0, 100);
                    end.GetComponent<EndingGame>().EndGame();
                }
            }
        }

    }
}

