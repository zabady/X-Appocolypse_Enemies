using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public bool canMove = true;
	public static bool playerDead = false;

	Transform playerTransform;
	NavMeshAgent nav;
	EnemyHealth enemyHealth;
	Animator anim;

	void Awake () {
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
		enemyHealth = GetComponent<EnemyHealth> ();
		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (canMove)
		{
			nav.SetDestination (playerTransform.position);
			nav.Resume ();
			transform.LookAt (new Vector3 (playerTransform.position.x, 0, playerTransform.position.z));
		}
		else if (enemyHealth.currentHealth > 0)
		{
			nav.Stop ();
		}

		// Will only execute once, when the zombie dies or when the player dies
		if (enemyHealth.currentHealth <= 0 && canMove || playerDead) {
			canMove = false;
			nav.Stop ();
		}
	}

	// Coillider Trigger
	void OnTriggerEnter (Collider otherGameObject)
	{
		if (otherGameObject.tag == "Barrier")
		{
			anim.SetBool ("Barrier", true);
		}
	}

	// This function is being called by the JUMPUP animation
	public void StopNavMeshMoving()
	{
		GetComponent<Rigidbody> ().isKinematic = true;
		canMove = false;
	}

	// This function is being called by the TAKEHIT and JUMPDOWN animations
	public void MoveAgainAfterAnimation()
	{
		this.canMove = true;
		if (GetComponent<Rigidbody> ().isKinematic) {
			GetComponent<Rigidbody> ().isKinematic = false;
		}
	}
}
