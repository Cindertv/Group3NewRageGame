using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour {

	public Player player;

    public bool rotateCamera = true;

	Vector3 offset;

	private void Start()
	{
		offset = transform.position - player.transform.position;
	}

	void Update () {
		transform.position = player.transform.position + offset;
        if(rotateCamera)     transform.rotation = player.transform.rotation;
	}
}
