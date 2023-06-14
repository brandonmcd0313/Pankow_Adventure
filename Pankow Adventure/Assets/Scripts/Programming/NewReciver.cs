using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NewReciver : MonoBehaviour
{
    [SerializeField] int position;
    public bool locked = false;
    public int denySpot;
    [Tooltip("a,d,space,start")]
    public string inputType;
    public int getPosition()
    {
        return position;
    }

    public void lockSpot(string actionType)
    {
        locked = true;
        GameObject.Find("player").GetComponent<PlayerControllerB>().SetValue(inputType, actionType);
    }

    public void unlockSpot()
    {
        locked = false;
    }
}



