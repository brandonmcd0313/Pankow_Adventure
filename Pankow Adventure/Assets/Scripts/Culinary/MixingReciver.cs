using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MixingReciver : MonoBehaviour
{
    bool stirring, cooling;
    public float lowSpeed;
    public float highSpeed;
    float speed;
    public void addItem(GameObject go)
    {
        if (go.GetComponent<MixingItem>() == null)
        {
            return;
        }
        //child this object
        go.transform.parent = transform;
        //set to child pos
        go.transform.localPosition = go.GetComponent<MixingItem>().childPos;
        //set to sprite
        if (go.GetComponent<MixingItem>().mixedSprite != null)
        {

            GetComponent<SpriteRenderer>().sprite = go.GetComponent<MixingItem>().mixedSprite;
        }

    }

    public void stirObj()
    {
        StartCoroutine(stir());
    }

    IEnumerator stir()
    {

        if(stirring)
        {
            if(speed < highSpeed * 2)
            {
                speed *= 1.25f;
            }
            yield break;
        }
        if(!cooling)
        {
            speed = UnityEngine.Random.Range(lowSpeed, highSpeed);
        }
        stirring = true;
        //rotate object slowly and a random amount for 2 - 3 seconds
        float time = 0;
        float goal = UnityEngine.Random.Range(0f, 1f);
        float rotationZ = transform.rotation.eulerAngles.z; // Store the initial z-axis rotation
        while (time < goal)
        {
            
            rotationZ += (speed * Time.deltaTime);
            //speeds up at start, slows at end.
            time += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, rotationZ);
            print((speed * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
        stirring = false;
        StartCoroutine(cooldown());
    }

    IEnumerator cooldown()
    {
        //just wait before reassining speed
        cooling = true;
        yield return new WaitForSeconds(1f);
        if(!stirring)
        {

            speed = UnityEngine.Random.Range(lowSpeed, highSpeed);
        }
        cooling = false;

    }
}

