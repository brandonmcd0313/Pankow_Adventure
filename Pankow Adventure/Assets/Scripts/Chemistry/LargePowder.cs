using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargePowder : MonoBehaviour
{
    public GameObject littlePowderPrefab;
    public int maxDraws; int draws;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
        //make a little powder
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        GameObject littlePowder = Instantiate(littlePowderPrefab, pos, Quaternion.identity);
        draws++;
        //decrease the scale of this object by scale
        float scale = 1f - (maxDraws / 100f);
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * scale, gameObject.transform.localScale.y * scale, gameObject.transform.localScale.z);
        if (draws == maxDraws)
        {
            //play sound
            Destroy(gameObject);
        }

    }
}
