using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRange : MonoBehaviour {

	public List<Enemy> targetableEnemies = new List<Enemy>();

	int selectedIndex = -1;

	public Enemy GetNextEnemy()
	{
		selectedIndex++;

		if (targetableEnemies.Count > 0)
		{
			if (selectedIndex >= targetableEnemies.Count)
				selectedIndex = 0;

			return targetableEnemies[selectedIndex];

		} else
		{
			return null;
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			if (!targetableEnemies.Contains(other.GetComponent<Enemy>()))
			{
				targetableEnemies.Add(other.GetComponent<Enemy>());
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			if (targetableEnemies.Contains(other.GetComponent<Enemy>()))
			{
				targetableEnemies.Remove(other.GetComponent<Enemy>());
			}
		}
	}

}
