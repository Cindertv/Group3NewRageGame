using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour {

	/*
	 * This is similar to the TrackObject script, except for adding an offset
	 * to maintain a constant distance between two objects.
	 *
	 * We're using it to track the second camera for the mini-map
	 * to be always a certain distance away from our player
	 *
	 */
	
	public Player player;

	Vector3 offset;

	private void Start()
	{
		offset = new Vector3(0, transform.position.y, 0);
	}

	void Update () {
		transform.position = player.transform.position + offset;
	}
}
