using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class InputManager : MonoBehaviour
{
    public float minTime, maxTime, gameRuntime;
    public AudioSource aud;
    // public string[] prompts;
    //public float[] times;
    public GameObject promtText;
    float time = 0f;  //0.005f;
    GameObject lowest; GameObject end;
   ArrayList active = new ArrayList();
    public bool InfiniteRandomModeForFun = false;
    private void Start()
    {
        end = GameObject.Find("EndGame");

        if (InfiniteRandomModeForFun)
        {
            StartCoroutine(InfinitegamePlay());
        }
        else
        {
            StartCoroutine(gamePlay());
        }
    }
   
    IEnumerator gamePlay()
    {
        GameObject start = Instantiate(promtText, new Vector3(0, 0, 0), Quaternion.identity);

        start.transform.GetChild(0).localScale = new Vector3(9.55f, 1.07f, 0);
        start.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
        start.GetComponent<TextMeshPro>().text = "Press the keys when they enter the square!";
        start.GetComponent<TextMeshPro>().enableWordWrapping = false;
        start.GetComponent<TextMeshPro>().fontSize = 7.7f;
        //move down to 0 0
        yield return new WaitForSeconds(2f);
        //start the audio siurce
        aud.Play();
        yield return new WaitForSeconds(2f);
        Destroy(start);
        time = 0;
        while (time < gameRuntime)
        {
            time += Time.deltaTime;
            KeyCode p = KeyCode.Alpha0;
            float t = Random.Range(minTime, maxTime);
            //random letter U R D L
            char prompt = ' ';
            switch (Random.Range(0, 4))
            {
                case 0:
                    prompt = 'U';
                    break;
                case 1:
                    prompt = 'R';
                    break;
                case 2:
                    prompt = 'D';
                    break;
                case 3:
                    prompt = 'L';
                    break;
                default:
                    Debug.Log("Invalid prompt: " + prompt);
                    break;
            }
            GameObject current = Instantiate(promtText, new Vector3(0, 6, 0), Quaternion.identity);
            end.GetComponent<EndingGame>().possible++;
            float zRotation = 0.0f;
            switch (prompt)
            {
                case 'U':
                    zRotation = 0.0f;
                    p = KeyCode.UpArrow;
                    break;
                case 'R':
                    zRotation = 270.0f;
                    p = KeyCode.RightArrow;
                    break;
                case 'D':
                    zRotation = 180.0f;
                    p = KeyCode.DownArrow;
                    break;
                case 'L':
                    zRotation = 90.0f;
                    p = KeyCode.LeftArrow;
                    break;
                default:
                    Debug.Log("Invalid prompt: " + prompt);
                    break;
            }
            current.transform.rotation = Quaternion.Euler(0, 0, zRotation);
            //running it in a seperate corountine allows multiple keys at once
            //one is going offscreen while the other is coming on ideally
            //
            StartCoroutine(prompter(t, current, p));
            yield return new WaitForSeconds(t);
            time += t;
           
        }
        //destroy all prompts by searching scene
        GameObject[] prompts = GameObject.FindGameObjectsWithTag("Prompt");
        foreach (GameObject p in prompts)
        {
            Destroy(p);
        }
        //end game
        //square root curve
        float prescore = Mathf.Sqrt(end.GetComponent<EndingGame>().score / end.GetComponent<EndingGame>().possible);
        end.GetComponent<EndingGame>().grade = (int)(prescore * 100);
        end.GetComponent<EndingGame>().EndGame();
    }

    IEnumerator prompter(float t, GameObject current, KeyCode p)
    {
        bool hit = false;
        //t = seconds to move down 6 units
        float distance = 12f;
        float distancePerSecond = distance / (2 * t); //2 t to move all the way down therefore 1 t to move 6 units

        for (float elapsedTime = 0.0f; elapsedTime < (2*t); elapsedTime += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
            if (current != null)
            { current.transform.position -= new Vector3(0, distancePerSecond * Time.deltaTime, 0); }
            else
            {
                yield return null;
            }
           
            if (Input.GetKeyDown(p) && !hit && current.transform.position.y < 2f && current.transform.position.y > -2f)
            {
                print("got" + current);
                hit = true;
                    // Set to green
                    current.GetComponent<TextMeshPro>().color = Color.green;
                end.GetComponent<EndingGame>().score++;

            }

            //if below -1 set to red
            if (current.transform.position.y < -2f && !hit)
            {
                hit = true;
                // Set to red
                current.GetComponent<TextMeshPro>().color = Color.red;
            }

        }
        Destroy(current);
        yield return null;
    }



    IEnumerator InfinitegamePlay()
    {
        GameObject start = Instantiate(promtText, new Vector3(0, 6, 0), Quaternion.identity);
        start.GetComponent<TextMeshPro>().text = "Press the keys to the beat!";
        start.GetComponent<TextMeshPro>().enableWordWrapping = false;
        start.GetComponent<TextMeshPro>().fontSize = 14;
        //move down to 0 0
        for (int i = 0; i < 600; i++)
        {
            yield return new WaitForSeconds(time);

            start.transform.position -= new Vector3(0, 0.01f, 0);

        }
        yield return new WaitForSeconds(2);
        Destroy(start);
        while (!Input.GetKey(KeyCode.Backspace))
        {
            KeyCode p = KeyCode.Alpha0;
            //random time 0.5f to 3.0f
            float t = Random.Range(0.5f, 0.75f);

            //random letter U R D L
            char prompt = ' ';
            switch (Random.Range(0, 4))
            {
                case 0:
                    prompt = 'U';
                    break;
                case 1:
                    prompt = 'R';
                    break;
                case 2:
                    prompt = 'D';
                    break;
                case 3:
                    prompt = 'L';
                    break;
                default:
                    Debug.Log("Invalid prompt: " + prompt);
                    break;
            }
            GameObject current = Instantiate(promtText, new Vector3(0, 6, 0), Quaternion.identity);

            float zRotation = 0.0f;
            switch (prompt)
            {
                case 'U':
                    zRotation = 0.0f;
                    p = KeyCode.UpArrow;
                    break;
                case 'R':
                    zRotation = 270.0f;
                    p = KeyCode.RightArrow;
                    break;
                case 'D':
                    zRotation = 180.0f;
                    p = KeyCode.DownArrow;
                    break;
                case 'L':
                    zRotation = 90.0f;
                    p = KeyCode.LeftArrow;
                    break;
                default:
                    Debug.Log("Invalid prompt: " + prompt);
                    break;
            }
            current.transform.rotation = Quaternion.Euler(0, 0, zRotation);
            //running it in a seperate corountine allows multiple keys at once
            //one is going offscreen while the other is coming on ideally
            //
            StartCoroutine(prompter(t, current, p));
            yield return new WaitForSeconds(t);

        }
    }
}
