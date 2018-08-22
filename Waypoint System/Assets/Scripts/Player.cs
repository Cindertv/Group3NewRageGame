using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour {

	public Enemy targetedEnemy = null;
	public TargetRange targetRange;
	public TrackObject trackObject;

	[Space]
	[Header("Player Stats")]
	public float maxHealth = 100;
	protected float currentHealth;

	[Space]
	[Header("UI elements")]
	public Text healthText;
	public Slider healthBar;
	
	private void Start()
	{
		trackObject.gameObject.SetActive(false);
		currentHealth = maxHealth;
		UpdateHealthUI();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
			SelectEnemy();

		if (Input.GetMouseButtonDown(0))
			AttackEnemy();

	}

	private void AttackEnemy()
	{
		if (targetedEnemy != null && Vector3.Distance(transform.position, targetedEnemy.transform.position) < 2)
		{
			targetedEnemy.TakeDamage(20);
		} else
		{
			print("Target too far away!");
		}


	}

	private void SelectEnemy()
	{
		targetedEnemy = targetRange.GetNextEnemy();
		if (targetedEnemy != null)
		{
			trackObject.gameObject.SetActive(true);
			trackObject.target = targetedEnemy.transform;
		}
		else
		{
			trackObject.gameObject.SetActive(false);
		}
	}

	public void TakeDamage(float attackDamage)
	{
		print("Attacked");
		//simple example
		if (Random.value < 0.1f)
		{
			currentHealth -= attackDamage;
		}
		else
		{
			currentHealth -= attackDamage * 0.2f;  //this would ultimately be determined by the minitgations
		}

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			GetComponent<RigidbodyFirstPersonController>().enabled = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			
			print("You're DEAD!");
			
		}

		UpdateHealthUI();
	}

	private void UpdateHealthUI()
	{
		healthBar.value = currentHealth / maxHealth;
		healthText.text = Mathf.Round(currentHealth / maxHealth * 100) + "%";
	}
}
