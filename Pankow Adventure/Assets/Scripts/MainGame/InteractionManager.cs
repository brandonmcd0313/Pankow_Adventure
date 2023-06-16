using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class InteractionManager : MonoBehaviour
{
    //this script is placed on a gameobject with a box collider
    //when the box collider is hit the next SET of interaction messages will run
    public string program = "none"; public int scene;
    Collider2D col; bool visited = false; public GameObject paper,cover;
    [SerializeField] string[] messages1; AudioSource aud; public AudioClip bell;
    [SerializeField] string[] idleMessages;
    string[][] messageSet = new string[1][];
    public  int messageIndex = 0;
    bool isPrinting;

    private void Awake()
    {
        LoadVisitedState();
    }
    
    // Start is called before the first frame update

    void Start()
    {
   
        paper = GameObject.Find("EventSystem").GetComponent<InteractionManager>().paper;
        cover = GameObject.Find("EventSystem").GetComponent<InteractionManager>().cover;
        bell = GameObject.Find("EventSystem").GetComponent<InteractionManager>().bell;
        aud = Camera.main.GetComponent<AudioSource>();
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



    private void LoadVisitedState()
    {
        string key = GetGameObjectKey();
        visited = PlayerPrefs.GetInt(key, 0) == 1;
    }

    private void SaveVisitedState()
    {
        string key = GetGameObjectKey();
        int value = visited ? 1 : 0;
        PlayerPrefs.SetInt(key, value);
    }

    private string GetGameObjectKey()
    {
        return gameObject.name;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isPrinting)
            {
                if (program == PlayerController.program && !visited)
                {
                    visited = true;
                    SaveVisitedState();
                    idleMessages[0] = "You have already taken this class today.";
                    idleMessages[1] = "Go to your next class or leave for the day";
                    StartCoroutine(LoadClass());
                }
                else if(program == PlayerController.program)
                {
                    idleMessages[0] = "You have already taken this class today.";
                    idleMessages[1] = "Go to your next class or leave for the day if you completed all classes";
                    StartCoroutine(Interaction());
                }
                else
                {
                    StartCoroutine(Interaction());
                }
            }
        }  
    }
    
    IEnumerator LoadClass()
    {
        PlayerController.canMove = false;
        GameObject.Find("Player").GetComponent<PlayerController>().NewScene();
        cover = Instantiate(cover);
        paper = Instantiate(paper);
        if(aud != null)
        {

            aud.PlayOneShot(bell);
        }
        //set both to alpha val 0
        cover.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        paper.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        
        //set paper  camera
        paper.transform.position = Camera.main.transform.position;
        //set cover to same pos as camera
        cover.transform.position = Camera.main.transform.position;
        //set both to zpos 0 
        cover.transform.position = new Vector3(cover.transform.position.x, cover.transform.position.y, 0);
        paper.transform.position = new Vector3(paper.transform.position.x, paper.transform.position.y, 0);

        paper.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
        cover.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
        //fade out audio fade in cover and paper

        for (float i = 0; i <= 1; i += 0.01f)
        {
            //set paper  camera
            paper.transform.position = Camera.main.transform.position;
            //set cover to same pos as camera
            cover.transform.position = Camera.main.transform.position;
            //set both to zpos 0 
            cover.transform.position = new Vector3(cover.transform.position.x, cover.transform.position.y, 0);
            paper.transform.position = new Vector3(paper.transform.position.x, paper.transform.position.y, 0);
          
            paper.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i);
            if (i < 0.91f) { cover.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f, i); }
            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene(scene);
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
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));


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
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            }
        }
        messageIndex++;
        
        //disable text
        TextBehaviour.disableText();
        isPrinting = false;
    }
}
