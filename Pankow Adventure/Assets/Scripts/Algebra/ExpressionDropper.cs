using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExpressionDropper : MonoBehaviour
{
  
    public GameObject dropPrefab;
    public BoxCollider2D spawnZone;
    public float spawnProb, spawnTime, gameTime, dropSpeed, dropVariablitiy;
   public  int goodDrops = 0;
    // Start is called before the first frame update
    void Start()
    {
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
            print(time + " vs " + gameTime);
            //attempt to spawn
            if (UnityEngine.Random.Range(0, 1) < spawnProb)
            {
                //spawn
                GameObject drop = Instantiate(dropPrefab);
                if(drop.GetComponent<DropItem>().rational)
                {
                    goodDrops++;
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
            if (time > 10 && !time10)
            {
                dropSpeed *= 1.30f;
                time10 = true;
            }

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame(); 
        }
    }
}
