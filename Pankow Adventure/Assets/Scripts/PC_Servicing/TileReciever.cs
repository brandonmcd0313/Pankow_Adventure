
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileReciever : MonoBehaviour
{
    public int position;
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
    

        
