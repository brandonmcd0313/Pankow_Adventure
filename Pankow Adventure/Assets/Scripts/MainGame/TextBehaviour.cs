using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBehaviour : MonoBehaviour
{
    /*
     * This class controls the text and UI behavoiur
     * calls: setText(sting)  disableText()
     * setText automatically enables UI
     */ 
    

    
    public static GameObject textBox;
    static Text text;
    Boolean isPrinting = false;
   Boolean ui = false;
    static TextBehaviour instance;
    // Start is called before the first frame update
    void Awake()
    {
        
        
        textBox = GameObject.Find("TextBox");
        //disable textbox at start
        textBox.SetActive(false);
        //get text object of child of textbox
        text = textBox.transform.GetChild(0).GetComponent<Text>();
        //set text to empty
        text.text = "";

        //set instance to this
        instance = this;

    }

    public static void setText(string s)
    {
        if (instance.isPrinting)
        {
            disableText();
        }
        instance.StartCoroutine(instance.printMessage(s));
    }

    IEnumerator printMessage(string message)
    {
        
        if (!ui && !isPrinting)
        {
            textBox.SetActive(true);
            ui = true;
        }
      //  print(message + "started");
       if(isPrinting)
        {
           yield return new WaitUntil(() => !isPrinting); //in queue
        }
            //reset
            isPrinting = true;
            text.text = "";
        
            for (int i = 0; i < message.Length; i++)
            {//make sure ui still there
            if (!ui)
            {
                yield break;
            }
            PlayerController.canMove = false;
            //puncuation pause
            if (message[i].Equals((char)44) || message[i].Equals((char)46)
                            || message[i].Equals((char)33) || message[i].Equals((char)63))
                    {
                        text.text += message[i];
                        yield return new WaitForSeconds(0.75f);

                    }
                    else
                    {
                        yield return new WaitForSeconds(0.03f);
                        text.text += message[i];


                    }
                

            }

        //wait for interaction then disable text
        yield return new WaitUntil(() => Input.GetButtonDown("Interact"));
        disableText();

    }

    public static void disableText()
    {
        instance.disableUI();
        PlayerController.canMove = true;
    }
    
    void disableUI()
    {
        if (!isPrinting)
        {
            textBox.SetActive(false);
            text.text = "";
            ui = false;
            isPrinting = false;
        }
        else
        {
            //stop all coroutines
            StopAllCoroutines();
            textBox.SetActive(false);
            text.text = "";
            ui = false;
            isPrinting = false;
        }
    }
}


        

