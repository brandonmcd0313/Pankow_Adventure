using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExpressionDropper : MonoBehaviour
{
    GameObject end;
    public GameObject dropPrefab;
    public BoxCollider2D spawnZone;
    public float spawnProb, spawnTime, gameTime, dropSpeed, dropVariablitiy;
   public  int goodDrops = 0;

 
    // Start is called before the first frame update
    void Start()
    {
        end = GameObject.Find("EndGame");
        StartCoroutine(gameplay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator gameplay()
    {
        bool time10 = false;
        float time = 0;
        while (gameTime > time)
        {
            //attempt to spawn
            if (UnityEngine.Random.Range(0, 1) < spawnProb)
            {
                //spawn
                GameObject drop = Instantiate(dropPrefab);
                yield return new WaitForEndOfFrame();
                if (drop.GetComponent<DropItem>().rational)
                {
                    goodDrops++;
                    end.GetComponent<EndingGame>().possible = goodDrops;
                    print(end.GetComponent<EndingGame>().possible);
                }
                drop.transform.position = new Vector2(UnityEngine.Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x), spawnZone.bounds.max.y);
                drop.GetComponent<Rigidbody2D>().gravityScale = dropSpeed * UnityEngine.Random.Range(1-dropVariablitiy, 1 + dropVariablitiy);
                //apply a slight angular veloity
                drop.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 0);
                drop.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-10, 10);
                drop.GetComponent<Rigidbody2D>().rotation = UnityEngine.Random.Range(-30, 30);
                //decleare if a radical or not

                yield return new WaitForSeconds(spawnTime);
                time += spawnTime;
                
            }
            //at 10 sec increase drop speed
            if (time < 10 && !time10)
            {
                dropSpeed *= 1.80f;
                time10 = true;
            }

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame(); 
        }
        //calculate score
        int score = (int)end.GetComponent<EndingGame>().score;
        int possible = (int)(end.GetComponent<EndingGame>().possible * 0.9f);

        float normalizedScore = Mathf.Clamp01((float)score / possible);
        float gradePercentage = Mathf.Sqrt(normalizedScore) * 100;

        int grade = Mathf.RoundToInt(gradePercentage);
        if (grade > 100)
        {
            //recalc no curve
             score = (int)end.GetComponent<EndingGame>().score;
            possible = (int)(end.GetComponent<EndingGame>().possible);
            gradePercentage = Mathf.Clamp01((float)score / possible);
            grade = Mathf.RoundToInt(gradePercentage * 100);
        }

        EndingGame endingGame = end.GetComponent<EndingGame>();
        endingGame.grade = grade;
        endingGame.EndGame();

    }
}
