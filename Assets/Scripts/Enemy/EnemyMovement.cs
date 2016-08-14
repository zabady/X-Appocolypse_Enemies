using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public static bool canMove = true;

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
		if (canMove) {
			nav.SetDestination (playerTransform.position);
			nav.Resume ();
			transform.LookAt (new Vector3 (playerTransform.position.x, 0, playerTransform.position.z));
		} else if (enemyHealth.currentHealth > 0) {
			nav.Stop ();
		}

		// Will only execute once, when the zomvie dies
		if (enemyHealth.currentHealth == 0 && canMove) {
			canMove = false;
			nav.Stop ();
		}
	}

	void OnTriggerEnter (Collider otherGameObject)
	{
		if (otherGameObject.tag == "Barrier")
		{
			canMove = false;
			anim.SetBool ("Barrier", true);
		}
	}

//	void OnTriggerExit(Collider otherGameObject)
//	{
//		if (otherGameObject.tag == "Player")
//		{
//			anim.SetBool ("PlayerInRange", false);
//		}
//	}

	// Jumping is the latest issue in shaa allah
}
