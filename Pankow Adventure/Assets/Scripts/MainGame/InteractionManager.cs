using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionManager : MonoBehaviour
{
    //this script is placed on a gameobject with a box collider
    //when the box collider is hit the next SET of interaction messages will run
    public string program = "none"; public int scene;
    Collider2D col; bool visted = false;
    [SerializeField] string[] messages1;
    [SerializeField] string[] idleMessages;
    string[][] messageSet = new string[1][];
    public  int messageIndex = 0;
    bool isPrinting;
    // Start is called before the first frame update
    void Start()
    {
        //assign collider
        col = GetComponent<Collider2D>();

        //combine messages array into 2d arraylist
        //if array is empty do not add it
        if (messages1.Length > 0)
        {
            messageSet[0] = messages1;
        }
        else
        {
            messageSet[0] = idleMessages;
        }

    }


    //when object tagged player hits the collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if no coroutine is running
            if (!isPrinting)
            {
                if (program == PlayerController.program && !visted)
                {
                    visted = true;
                    messages1[0] = "You have already taken this class today.";
                    messages1[1] = "Go to your next class or leave for the day";
                    SceneManager.LoadScene(scene); 
                    
                   
                }
                else
                {

                    StartCoroutine(Interaction());
                }
            }
        }
    }

    IEnumerator Interaction()
    {
        isPrinting = true;
        if (messageIndex >= messageSet.Length)
        {
            //if all messages have been displayed
            //display idle messages
            foreach (string s in idleMessages)
            {
                if (s != "")
                {
                    yield return new WaitForEndOfFrame();
                    TextBehaviour.setText(s);
                    yield return new WaitUntil(() => Input.GetButtonDown("Interact"));
                    
                }
            }

            //disable text
            TextBehaviour.disableText();
            isPrinting = false;
            yield break;
        }
        //go through each index and wait until interacted to print next
        foreach (string s in messageSet[messageIndex])
        {
            if (s != "")
            {
                yield return new WaitForEndOfFrame();
                TextBehaviour.setText(s);
                yield return new WaitUntil(() => Input.GetButtonDown("Interact"));   
            }
        }
        messageIndex++;
        
        //disable text
        TextBehaviour.disableText();
        isPrinting = false;
    }
}
