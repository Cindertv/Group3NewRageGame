using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour {

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
