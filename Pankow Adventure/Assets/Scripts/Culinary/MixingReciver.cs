using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MixingReciver : MonoBehaviour
{
    bool stirring, cooling;
    public float lowSpeed;
    public float highSpeed;
    float speed; int layer = 2;
    public void addItem(GameObject go)
    {
        if (go.GetComponent<MixingItem>() == null)
        {
            return;
        }
        //child this object
        go.transform.parent = transform;
        //set to child pos
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = new Vector3(1, 1, 1);
        go.transform.localRotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));
        //remove collider
        Destroy(go.GetComponent<Collider2D>());
        //set to sprite
        if (go.GetComponent<MixingItem>().mixedSprite != null)
        {

            go.GetComponent<SpriteRenderer>().sprite = go.GetComponent<MixingItem>().mixedSprite;
            //set layer
            GetComponent<SpriteRenderer>().sortingOrder = layer;
            layer++;
        }

    }

    public void stirObj()
    {
        float goal = UnityEngine.Random.Range(0.5f, 1.2f);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<MixingItem>().stirObj(lowSpeed,highSpeed, goal);
        }
    }

}

