using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static List<Transform> List = new List<Transform>(); 

    public static int CurrentIndex { get; set; }

    private void Awake()
    {
        foreach (Transform waypoint in transform)
        {
            List.Add(waypoint);
        }
    }
}
