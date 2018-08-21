using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour {

	//This script will set the transform of the object its attached to 
	//to the same transform of a targeted object.
	
	//We're using this script to track the currently selected enemy's
	//selection ring with the enemy.
	
	public Transform target = null;
	
	void LateUpdate () {
		if (target != null)
		{
			transform.position = target.transform.position;
		}
	}
}
