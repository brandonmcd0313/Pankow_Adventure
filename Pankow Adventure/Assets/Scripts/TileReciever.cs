using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;

public class TileReciever : MonoBehaviour
{
    [SerializeField] int position;

    public int getPosition()
    {
        return position;
    }

    
}
        
