using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour {

	public Transform target = null;
	
	void LateUpdate () {
		if (target != null)
		{
			transform.position = target.transform.position;
		}
	}
}
