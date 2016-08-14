using UnityEngine;
using System.Collections;
using System;

public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 1f;
    public int attackDamage = 1;

	Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
    NavMeshAgent navMesh;
	bool playerInRange;
    bool playerJustDied;
    float timer;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		enemyHealth = GetComponent<EnemyHealth> ();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        navMesh = GetComponent<NavMeshAgent>();

    }

	void OnTriggerEnter (Collider otherGameObject)
	{
		if (otherGameObject.tag == "Player")
		{
			anim.SetBool ("PlayerInRange", true);
            playerInRange = true;
        }
	}

	void OnTriggerExit(Collider otherGameObject)
	{
		if (otherGameObject.tag == "Player")
		{
			anim.SetBool ("PlayerInRange", false);
            playerInRange = false;
        }
	}

	void Update ()
    {
        if(playerHealth.currentHealth <= 0 && !playerJustDied)
        {
            playerJustDied = true;
            navMesh.Stop();
            anim.SetTrigger("PlayerDead");
        }
	}

	// This function get called by the ATTACK animation
    public void AttackPlayer()
    {
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
