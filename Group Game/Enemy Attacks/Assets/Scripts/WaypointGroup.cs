using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointGroup : MonoBehaviour
{
    /*
     * This is a set of helper methods to return the nearest waypoint index and returning a
     * particular waypoint based on the selected index
     */
    
    public int GetNearestWaypointIndex(Transform other)
    {
        float nearestDistance = float.PositiveInfinity;
        int currentNearestIndex = -1;

        foreach (Transform child in transform)
        {
            if (Vector3.Distance(other.position, child.position) < nearestDistance)
            {
                nearestDistance = Vector3.Distance(other.position, child.position);
                currentNearestIndex = child.GetSiblingIndex();
            }
        }

        return currentNearestIndex;
    }

    public Transform GetWaypoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    public int IncrementIndex(int currentIndex)
    {
        currentIndex++;
        currentIndex %= transform.childCount;
        return currentIndex;
    }
}