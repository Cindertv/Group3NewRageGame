using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

	//This script updates the selected enemy's health to the UI system
	
	[Space]
	[Header("UI elements")]
	
	public Text healthText;
	public Slider healthBar;

	public Player player;

	private void FixedUpdate () {
		if (player.targetedEnemy == null) 
		{
			
			healthBar.gameObject.SetActive(false);

		} else
		{
		
			UpdateHealthUI();
	
		}
	}

	private void UpdateHealthUI()
	{
		healthBar.value = player.targetedEnemy.GetHealthPertcentage();
		healthText.text = Mathf.Round(player.targetedEnemy.GetHealthPertcentage() * 100) + "%";
	}
}
