﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyState
{
    Patrol,
    Attack,
	Dead
}

public class Enemy : MonoBehaviour
{

	[Space]
	[Header("Player Stats")]
	public float maxHealth = 100;
	public string enemyName;
	protected float currentHealth;
	public float minAttackDelay = 1, maxAttackdelay = 4;
	private float timeToNextAttack = 0;

	[Space]
	[Header("UI elements")]
	public Text enemyNameText;
	public Text healthText;
	public Slider healthBar;
	public Renderer mesh;

	private EnemyState state = EnemyState.Patrol;
    private WaypointSolver wpSolver;
    private Player player;
	private Animator anim;



    // Use this for initialization
    void Start()
	{
		wpSolver = GetComponent<WaypointSolver>();
		player = FindObjectOfType<Player>();
		anim = GetComponent<Animator>();

		enemyNameText.text = enemyName;
		currentHealth = maxHealth;
		UpdateHealthUI();

       
	}

	private void Update()
	{
		if (state == EnemyState.Attack && timeToNextAttack < 0)
		{
			timeToNextAttack = Random.Range(minAttackDelay, maxAttackdelay);
			anim.SetTrigger("Attack");
			player.TakeDamage(10);
		}

		timeToNextAttack -= Time.deltaTime;

	}

	private void UpdateHealthUI()
	{
		healthBar.value = currentHealth / maxHealth;
		healthText.text = Mathf.Round(currentHealth / maxHealth * 100) + "%";
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wpSolver.StartPursuit();
            state = EnemyState.Attack;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (state == EnemyState.Attack)
            {
                state = EnemyState.Patrol;
                wpSolver.StartPatrolling();
            }
        }
    }

	public void TakeDamage(float attackDamage)
	{
		//simple example
		if (Random.value < 0.1f)
		{
			currentHealth -= attackDamage;
		} else
		{
			currentHealth -= attackDamage * 0.2f;  //this would ultimately be determined by the minitgations
		}

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			wpSolver.SetState(PatrolState.Dead);

			Color color = mesh.material.color;
			color.a = 0;
			mesh.material.color = color;

		}

		UpdateHealthUI();
	}

	public float GetHealthPertcentage()
	{
		return currentHealth / maxHealth;
	}
    
}