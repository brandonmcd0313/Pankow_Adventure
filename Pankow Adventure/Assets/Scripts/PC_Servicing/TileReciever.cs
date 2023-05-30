using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;

public class TileReciever : MonoBehaviour
{
    [SerializeField] int position;
        public  bool locked = false;
    public int denySpot;
    public int getPosition()
    {
        return position;
    }

    public void lockSpot()
    {
        locked = true;
    }

    public void unlockSpot()
    {
        locked = false;
    }
}
    

        
