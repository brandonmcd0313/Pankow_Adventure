using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StartingGame : MonoBehaviour
{
    public GameObject cover, paper;
    TextMeshPro introText;
    public string intro;
    public AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        cover = Instantiate(cover);
       paper = Instantiate(paper);
        //replace /n with a line break
        intro = intro.Replace("/n", "\n");
        introText = paper.transform.GetChild(0).GetComponent<TextMeshPro>();
        introText.text = intro;
        //set paper to 5 units above camera
        paper.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        //set cover to same pos as camera
        cover.transform.position = Camera.main.transform.position;
        //set both to zpos 0 
        cover.transform.position = new Vector3(cover.transform.position.x, cover.transform.position.y, 0);
        paper.transform.position = new Vector3(paper.transform.position.x, paper.transform.position.y, 0);

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            if (aud != null)
            {

                aud.Play();
            }
            Destroy(paper);
            Destroy(cover);
            Destroy(this.gameObject);
            
        }
    }
    
  
}
