using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager instance;

    public WaypointGroup [] waypoints;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        } 
    }
}
