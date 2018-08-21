using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRange : MonoBehaviour {

	//keep a list of enemies we can potentially target and interact with.
	public List<Enemy> targetableEnemies = new List<Enemy>();

	//currently selected enemy index
	private int selectedIndex = -1;

	public Enemy GetNextEnemy()
	{
		//we try and select the next enemy in the list.  If there's less enemies
		//than our current index, we reset the index to the first one. 
		//If there's no enemies in the list, we simply return null.
		
		selectedIndex++;

		if (targetableEnemies.Count > 0)
		{
			if (selectedIndex >= targetableEnemies.Count)
				selectedIndex = 0;

			return targetableEnemies[selectedIndex];

		}

		return null;
	}


	private void OnTriggerEnter(Collider other)
	{
		//If the other object entering the trigger is an enemy, we add it to the
		//potential enemy selection list if it's not already added
		
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
		//If an enemy leaves the trigger area, we remove it from the list if its
		//still part of the list.
		
		if (other.CompareTag("Enemy"))
		{
			if (targetableEnemies.Contains(other.GetComponent<Enemy>()))
			{
				targetableEnemies.Remove(other.GetComponent<Enemy>());
			}
		}
	}

}
