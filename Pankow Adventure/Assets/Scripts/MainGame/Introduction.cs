using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
   
    //TODO: make this inroduction more thematic
    private string[] intro = new string[]
    {
        "hi welcome to the game",
        "space or top left button to interact",
        "wasd or left joystick to move"
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
    

