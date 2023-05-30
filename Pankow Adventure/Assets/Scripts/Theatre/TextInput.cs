using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextInput : MonoBehaviour
{
    public GameObject exampleObj, coverObj, inputObj;
    TextMeshProUGUI exampleText, cover, input; //cover is the same colour as the background
    float score, perfectScore;
    // Start is called before the first frame update
    void Start()
    {

        exampleText = exampleObj.GetComponent<TextMeshProUGUI>();
        cover = coverObj.GetComponent<TextMeshProUGUI>();
        input = inputObj.GetComponent<TextMeshProUGUI>();
        perfectScore = exampleText.text.Length;
    }

    // Update is called once per frame
    void Update()
    {
        //take a key input and add it to input
        //if input is equal to ideal, then do something
        // Check for key input
        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
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
            }
        }

    }
}

