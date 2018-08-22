using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointGroup : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

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