using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    AudioSource aud;
    public AudioClip pos, neg;
    public int score = 0;
    public float moveSpeed;
    GameObject end;
    public float xMin, xMax;
    TextMeshPro tm;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        end = GameObject.Find("EndGame");
        tm = this.transform.GetChild(0).GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
     void Update()
    {
        tm.text = "Score: " + score.ToString();
        end.GetComponent<EndingGame>().score = score;
        //left and right movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += (Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += (Vector3.right * moveSpeed * Time.deltaTime);
        }

        if (transform.position.x < xMin)
        {
            transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xMax)
        {
            transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
        }

    }

    //when a drop item collides with the catcher
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the other object is a drop item
        if (other.gameObject.GetComponent<DropItem>() != null)
        {
            //if the drop item is rational
            if (other.gameObject.GetComponent<DropItem>().rational)
            {
                //add 1 to score
                score++;
                StartCoroutine(feedbackText(Color.green));
                aud.PlayOneShot(pos);
            }
            else
            {
                aud.PlayOneShot(neg);
                //50% chance to decrease score
                if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
                {
                    score--;
                    StartCoroutine(feedbackText(Color.red));
                }
                else
                {
                    StartCoroutine(feedbackText(Color.red));
                }
            }
            //destroy the drop item
            Destroy(other.gameObject);
        }
    }

    IEnumerator feedbackText(Color c)
    {
        //set text to color 
        tm.color = c;
        //wait
        yield return new WaitForSeconds(0.5f);
        //set text to black
        tm.color = Color.black;

    }
}
