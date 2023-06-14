using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
   
    //TODO: make this inroduction more thematic
    private string[] intro = new string[]
    {
        "Hello, Welcome To Pankow!",
        "You are part of the " + PlayerController.program + " program.",
        "Travel the school using WASD controls and find your classes.",
        "You can find your class list/ report card by pressing R",
        "Press the SPACE bar to skip dialogue",
        "When you are done with all your classes you can leave!"
    };
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(starting());
    }
    
    IEnumerator starting()
    {

     
        //go through each index and wait until interacted to print next
        foreach (string s in intro)
        {
            yield return new WaitForEndOfFrame();
            TextBehaviour.setText(s);
            yield return new WaitUntil(() => Input.GetButtonDown("Interact"));
          //  print("next");
            
        }
        //disable text
        TextBehaviour.disableText();
    }
}
    

