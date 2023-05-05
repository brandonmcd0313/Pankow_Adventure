using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    //this script is placed on a gameobject with a box collider
    //when the box collider is hit the next SET of interaction messages will run

    Collider2D col;
    [SerializeField] string[] messages1;
    [SerializeField] string[] messages2;
    [SerializeField] string[] messages3;
    [SerializeField] string[] idleMessages;
    string[][] messageSet = new string[3][];
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
        if (messages2.Length > 0)
        {
            messageSet[1] = messages2;
        }
        if (messages3.Length > 0)
        {
            messageSet[2] = messages3;
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
                StartCoroutine(Interaction());
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
