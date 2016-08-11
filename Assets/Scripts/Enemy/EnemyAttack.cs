using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;

	Animator anim;
	EnemyHealth enemyHealth;
	// GameObject player;
	// bool playerInRange;

	void Awake()
	{
		// player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent<Animator> ();
		enemyHealth = GetComponent<EnemyHealth> ();
	}

	void OnTriggerEnter (Collider otherGameObject)
	{
		if (otherGameObject.tag == "Player")
		{
			anim.SetBool ("PlayerInRange", true);
		}
	}

	void OnTriggerExit(Collider otherGameObject)
	{
		if (otherGameObject.tag == "Player")
		{
			Debug.Log ("Ana Msheet");
			anim.SetBool ("PlayerInRange", false);
		}
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
