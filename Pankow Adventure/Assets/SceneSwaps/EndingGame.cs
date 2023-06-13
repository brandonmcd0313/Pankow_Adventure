using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingGame : MonoBehaviour
{
    public float possible, score, grade;
    public GameObject cover, paper; bool gameEnded;
    public AudioSource aud;
    TextMeshPro exitText;
    [Tooltip("use SCORE for score, POS for possible points, GRADE for the grade(1-100)")]
    public string intro; 
    // Start is called before the first frame update
    void Start()
    {
        gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameEnded)
        {
            //go to mainscene
            SceneManager.LoadScene(1);
        }
    }

    public void EndGame()
    {
        StartCoroutine(EndingGametime());
    }

    IEnumerator EndingGametime()
    {

        gameEnded = true;

        cover = Instantiate(cover);
        paper = Instantiate(paper);
        exitText = paper.transform.GetChild(0).GetComponent<TextMeshPro>();

        TextMeshPro dexc = cover.transform.GetChild(0).GetComponent<TextMeshPro>();
        dexc.text = "Press Space to Exit Class";
        //set both to alpha val 0
        cover.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        paper.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        exitText.color = new Color(0, 0, 0, 0);
        //replace score and possible with actual values casted to ints
        intro = intro.Replace("SCORE", ((int)score).ToString());
        intro = intro.Replace("POS", ((int)possible).ToString());
        intro = intro.Replace("GRADE", ((int)grade).ToString());

        
        intro = intro.Replace("/n", "\n");
        exitText.text = intro;
        //set text to be center alligned
        exitText.alignment = TextAlignmentOptions.Center;
        //set paper  camera
        paper.transform.position = Camera.main.transform.position;
        //set cover to same pos as camera
        cover.transform.position = Camera.main.transform.position;
        //set both to zpos 0 
        cover.transform.position = new Vector3(cover.transform.position.x, cover.transform.position.y, 0);
        paper.transform.position = new Vector3(paper.transform.position.x, paper.transform.position.y, 0);

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
            if (aud != null)
            {
                aud.volume = 1 - i;
            }
            exitText.color = new Color(0, 0, 0, i);
            paper.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i);
            if (i < 0.91f) { cover.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f, i); }
            yield return new WaitForSeconds(0.01f);
        }

        Time.timeScale = 0;

    }
}

