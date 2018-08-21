using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PatrolState
{
    Idle, //Wait for instructions
    Patrol, //Waypoint to waypooint
    Pursue, //Follow the player
    Dead //Pretty obvious
}

public class WaypointSolver : MonoBehaviour
{
    /*
     * The waypoint solver is responsible to move our enemies around the map.
     *
     * Depending on the patrol state, it will perform the appropriate action
     */

    public WaypointGroup waypoints;
    public float tolerance = 0.5f;
    private NavMeshAgent agent;
    private Transform currentWaypoint = null;
    private Player player;

    public PatrolState patrolState = PatrolState.Patrol;
    public PatrolState lastPatrolState;
    public float gaurdRadius = 3;

    private int waypointIndex;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
        lastPatrolState = patrolState;
        waypointIndex = waypoints.GetNearestWaypointIndex(transform);
        SetWaypoint();
    }

    private void SetWaypoint()
    {
        /*
         * We're setting the waypoint based on the current patrol state
         */

        switch (patrolState)
        {
            case PatrolState.Patrol:
                currentWaypoint = waypoints.GetWaypoint(waypointIndex);
                agent.SetDestination(currentWaypoint.position);
                break;

        
        }
    }

    private void Update()
    {
        /*
         * Update is performed based on the current patrol state
         */
        
        switch (patrolState)
        {
            case PatrolState.Dead:
                return;
            
            case PatrolState.Patrol:
                if (agent.remainingDistance < tolerance)
                {
                    patrolState = PatrolState.Patrol;
                    Invoke("ResumePatrol", 3);
                }

                break;


            case PatrolState.Pursue:
                agent.SetDestination(player.transform.position);
                break;
        }
    }

    internal void SetState(PatrolState newState)
    {
        patrolState = newState;

        if (patrolState != PatrolState.Dead) return;
        
        agent.isStopped = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void ResumePatrol()
    {
        patrolState = lastPatrolState = PatrolState.Patrol;
        waypointIndex = waypoints.IncrementIndex(waypointIndex);
        SetWaypoint();
    }


    public void StartPursuit()
    {
        patrolState = PatrolState.Pursue;
    }

    public void StartPatrolling()
    {
        patrolState = lastPatrolState;
        waypointIndex = waypoints.GetNearestWaypointIndex(transform);
        SetWaypoint();
    }
}