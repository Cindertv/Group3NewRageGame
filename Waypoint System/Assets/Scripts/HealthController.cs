using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

	[Space]
	[Header("UI elements")]
	public Text enemyNameText;
	public Text healthText;
	

	public Player player;
	
	
	// Update is called once per frame
	void FixedUpdate () {
		if (player.targetedEnemy == null) 
		{
			enemyNameText.text = "Select Enemy";
			

		} else
		{
			if (enemyNameText.text != player.targetedEnemy.enemyName)
			{
				
				enemyNameText.text = player.targetedEnemy.enemyName;
			}
			UpdateHealthUI();
	
		}


	}

	private void UpdateHealthUI()
	{
		
		healthText.text = Mathf.Round(player.targetedEnemy.GetHealthPertcentage() * 100) + "%";
	}
}
