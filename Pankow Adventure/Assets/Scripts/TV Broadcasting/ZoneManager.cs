using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public GameObject target; 
    private Vector3 initialScale;
    public BoxCollider2D goodZone;
    BoxCollider2D col; Vector2 initSize;
    public  BoxCollider2D checker;
    void Start()
    {
        initialScale = transform.localScale; // Store the initial scale 
        col = GetComponent<BoxCollider2D>();
        initSize = col.size;
        checker.size = col.size;
        
    }

  void Update()
    {
        //set checker to init values
        checker.size = initSize;
        float scaleFactor = 1f / transform.parent.localScale.x; // Inverse of the parent's X scale
        checker.gameObject.transform.localScale = Vector3.Scale(target.transform.localScale, initialScale) * scaleFactor;

        // Check if checekr is contained
        if (!IsContainedInGoodZone())
        {
            //set it to be contained @ max
            col.size = new Vector2(1.85f, 1.85f);

            //set to same scale as goodZone
            transform.localScale = goodZone.transform.localScale;
        }
        else
        {
            //set it to be contained @ normal
            col.size = initSize;

            //set to same scale as goodZone
            transform.localScale = Vector3.Scale(target.transform.localScale, initialScale) * scaleFactor;
        }
    }

    bool IsContainedInGoodZone()
    {
        Bounds colBounds = checker.bounds;
        Bounds goodZoneBounds = goodZone.bounds;

        return goodZoneBounds.Contains(colBounds.min) && goodZoneBounds.Contains(colBounds.max);
    }

}
