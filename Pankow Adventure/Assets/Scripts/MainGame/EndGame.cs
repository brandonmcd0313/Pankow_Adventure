using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    //FUCK WHY DID I CALL IT THIS

    //on player uhhhh
public GameObject cover, endTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EndGameBox")
        {
            //set cover and end txt to same pos as camera
            cover.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);

            //enable both things
            cover.GetComponent<SpriteRenderer>().enabled = true;
            endTxt.GetComponent<TextMeshPro>().enabled = true;
            PlayerController.canMove = false;
        }
    }
}
