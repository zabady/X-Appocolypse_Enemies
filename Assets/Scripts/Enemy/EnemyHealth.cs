using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 5;
	public int currentHealth;
	public float sinkSpeed = 2.5f;
	public int scoreValue = 10;
	public int headshotScoreValue = 30;
	public AudioClip deathClip;

	Animator anim;
	AudioSource enemyAudio;
	ParticleSystem hitParticles;
	CapsuleCollider capsuleCollider;
	bool isDead;
	bool isSinking;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		enemyAudio = GetComponent<AudioSource> ();
		hitParticles = GetComponentInChildren<ParticleSystem> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isSinking)
		{
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}

	public void TakeDamage(int amount, Vector3 hitPoint)
	{
		if (isDead) {
			return;
		}

		enemyAudio.Play ();
		currentHealth -= amount;

		hitParticles.transform.position = hitPoint;
		hitParticles.Play ();

		if (currentHealth <= 0) {
			Death ();
		}
	}

	void Death()
	{
		isDead = true;

		capsuleCollider.isTrigger = true;

		anim.SetTrigger ("Dead");

		enemyAudio.clip = deathClip;
		enemyAudio.Play ();
	}

	public void StartSinking()
	{
		Debug.Log ("START SINKING MOTHER FUCKER!");
		GetComponent<NavMeshAgent> ().enabled = false;
		GetComponent<Rigidbody> ().isKinematic = true;
		isSinking = true;
		// ScoreManager.score += scoreValue;
		Destroy(gameObject, 2);
	}
}
