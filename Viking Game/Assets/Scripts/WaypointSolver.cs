using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PatrolState
{
	Idle,		//Wait for instructions
	Patrol,     //Waypoint to waypooint
	Gaurd,      //Gaurds closest waypoint
	Wait,		//Wait a few moments, them move on
	Pursue,		//Follow the player
	Dead		//Pretty obvious
}

public class WaypointSolver : MonoBehaviour
{
	public WaypointGroup waypoints;
	public float tolerance = 0.5f;
	private NavMeshAgent agent;
	private Transform currentWaypoint = null;
	private Player player;
    public int waypointindex;

	public PatrolState patrolState = PatrolState.Patrol;
	public PatrolState lastPatrolState;
	public float gaurdRadius = 3;

	private int waypointIndex;
    public int waypointGroupIndex;


    // Use this for initialization
    void Start ()
	{
        waypoints = WaypointManager.instance.waypoints[waypointGroupIndex];

        agent = GetComponent<NavMeshAgent>();
		player = FindObjectOfType<Player>();
		lastPatrolState = patrolState;
		waypointIndex = waypoints.GetNearestWaypointIndex(transform);
		SetWaypoint();

	}

	private void SetWaypoint()
	{
		if (patrolState == PatrolState.Patrol)
		{
			currentWaypoint = waypoints.GetWaypoint(waypointIndex);
			agent.SetDestination(currentWaypoint.position);
		}

		if (patrolState == PatrolState.Gaurd)
		{
			Vector3 randomPoint = Random.insideUnitSphere * gaurdRadius;
			randomPoint.y = 0;
			currentWaypoint = waypoints.GetWaypoint(waypointIndex);
			agent.SetDestination(currentWaypoint.position + randomPoint);
		}
	}

	private void Update()
	{
		if (patrolState == PatrolState.Dead) return;

		if (patrolState == PatrolState.Patrol && agent.remainingDistance < tolerance)
		{
			patrolState = PatrolState.Wait;
			Invoke("ResumePatrol", 3);
		}

		if (patrolState == PatrolState.Gaurd)
		{
			patrolState = PatrolState.Wait;
			Invoke("ResumeGaurd", 3);
		}

		if (patrolState == PatrolState.Pursue)
		{
			agent.SetDestination(player.transform.position);
		}
	}

	internal void SetState(PatrolState newState)
	{
		patrolState = newState;

		if (patrolState == PatrolState.Dead)
		{
			agent.isStopped = true;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}

	private void ResumePatrol()
	{
		patrolState = lastPatrolState = PatrolState.Patrol;
		waypointIndex = waypoints.IncrementIndex(waypointIndex);
		SetWaypoint();
	}

	private void ResumeGaurd()
	{
		patrolState = lastPatrolState = PatrolState.Gaurd;

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
