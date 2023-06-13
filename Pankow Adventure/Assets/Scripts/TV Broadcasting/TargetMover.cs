using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    //move the target between points and sizes
    [Tooltip("Z pos is the size of the object")]
    public Vector3[] points;
    public float gameTime; GameObject end;
    public float speed; 
    void Start()
    {
        end = GameObject.Find("EndGame");
        StartCoroutine(move());
    }

    IEnumerator move()
    {
        float time = 0;
        //move between points and sizes
        while (time < gameTime)
        {
            
            for (int i = 0; i < points.Length; i++)
            {
                print(time);
                //init and final values
                Vector2 goalPosition = points[i];
                float goalSize = points[i].z;
                Vector3 startPosition = transform.position;
                Vector3 startScale = transform.localScale;
                
                //find values to use
                float distance = Vector2.Distance(startPosition, goalPosition);
                float sizeDifference = Mathf.Abs(goalSize - startScale.x);
                float transitionTime = Mathf.Max(distance / speed, sizeDifference / speed) * UnityEngine.Random.Range(0.8f, 1.2f);

                float elapsedTime = 0;
                while (elapsedTime < transitionTime)
                {

                    float t = (elapsedTime / transitionTime); //20% variation in speed
                    transform.position = Vector2.Lerp(startPosition, goalPosition, t);
                    transform.localScale = Vector3.Lerp(startScale, new Vector3(goalSize, goalSize, 1), t);
                    yield return new WaitForEndOfFrame();
                    time += Time.deltaTime;
                    elapsedTime += Time.deltaTime;
                   
                }

                transform.position = goalPosition; //just to be sure
                transform.localScale = new Vector3(goalSize, goalSize, 1);
                
            }

            //shuffle points 
            for (int i = 0; i < points.Length; i++)
            {
                Vector3 temp = points[i];
                int randomIndex = UnityEngine.Random.Range(i, points.Length);
                points[i] = points[randomIndex];
                points[randomIndex] = temp;
           
            }
        }
        end.GetComponent<EndingGame>().EndGame();
    }
    


}
