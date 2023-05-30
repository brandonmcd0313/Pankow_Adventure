using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class InputManager : MonoBehaviour
{
    public string[] prompts;
    public float[] times;
    public GameObject promtText;
    float time = 0f;  //0.005f;
    GameObject lowest;
   ArrayList active = new ArrayList();
    public bool InfiniteRandomModeForFun = false;
    private void Start()
    {
       
        if (InfiniteRandomModeForFun)
        {
            StartCoroutine(InfinitegamePlay());
        }
        else
        {
            StartCoroutine(gamePlay());
        }
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
        while(!Input.GetKey(KeyCode.Backspace))
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
    IEnumerator gamePlay()
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
        for (int i = 0; i < prompts.Length; i++)
        {
            KeyCode p = KeyCode.Alpha0;
            float t = times[i];
            char prompt = prompts[i].ToUpper()[0];
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

    IEnumerator prompter(float t, GameObject current, KeyCode p)
    {
        active.Add(current);
        bool hit = false;
        //t = seconds to move down 6 units
        float distance = 12f;
        float distancePerSecond = distance / (2 * t); //2 t to move all the way down therefore 1 t to move 6 units

        for (float elapsedTime = 0.0f; elapsedTime < (2*t); elapsedTime += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
            if (current != null)
            { current.transform.position -= new Vector3(0, distancePerSecond * Time.deltaTime, 0); }
      
            if((Mathf.Abs(elapsedTime - t) / t) <= 1.3f && elapsedTime > t && !hit)
            {
                hit = true;
                // Set current to red
                current.GetComponent<TextMeshPro>().color = Color.red;
                // Point stuff
            }
            if (Input.GetKey(p) && !hit && (active.IndexOf(current) == closestToMiddle()))
            {
                print("got" + current);
                active.Remove(current);
                float currentTime = elapsedTime;
                float onTimeThreshold = 0.25f;

                if ((Mathf.Abs(currentTime - t)/t) <= onTimeThreshold)
                {
                    hit = true;
                    // Set to green
                    current.GetComponent<TextMeshPro>().color = Color.green;
                    // Point stuff
                }
                else
                {
                    hit = true;
                    // Set current to red
                    current.GetComponent<TextMeshPro>().color = Color.red;
                    // Point stuff
                }

            }

        }
        //if this object is still in active arraylist remove it and everything before it
        if(active.IndexOf(current) != -1)
        {
            int index = active.IndexOf(current);
            for (int i = index; i >= 0; i--)
            {
                active.RemoveAt(i);
            }
        }
        Destroy(current);
        yield return null;
    }
    
  int closestToMiddle()
    {
        int index = -1; 
        float closestDistance = 5 ;

        for (int i = 0; i < active.Count; i++)
        {
            Vector3 objectPosition = ((GameObject)active[i]).transform.position;

            // Check if the object is within the y-axis range (-5 to 5)
            if (objectPosition.y > -5 && objectPosition.y < 5)
            {
                float distance = Vector3.Distance(Vector3.zero, objectPosition);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    index = i;
                }
            }
        }

        return index;
    }
}
