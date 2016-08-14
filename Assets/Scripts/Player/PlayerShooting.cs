using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	public int damagePerShot = 1;
	public int damagePerHeadShot = 5;
	public float timeBetweenBullets = 0.15f;
	public float range = 100;

	float timer;
	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;
	ParticleSystem gunParticles;
	LineRenderer gunLine;
	AudioSource gunAudio;
	Light gunLight;
	float effectsDisplayTime = 0.2f;

	void Awake ()
	{
		shootableMask = LayerMask.GetMask ("Shootable");
		gunParticles = GetComponent<ParticleSystem> ();
		gunLine = GetComponent<LineRenderer> ();
		gunAudio = GetComponent<AudioSource> ();
		gunLight = GetComponent<Light> ();
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets) 
		{
			Shoot ();
		}

		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects ();
		}
	}

	void OnGUI()
	{
		GUI.Label(new Rect (Screen.width / 2.01f, Screen.height / 2.12f, 200.0f, 200.0f), "X");
	}

	void DisableEffects()
	{
		gunLine.enabled = false;
		gunLight.enabled = false;
	}

	void Shoot()
	{
		timer = 0f;

		gunAudio.Play ();

		gunLight.enabled = true;

		gunParticles.Stop ();
		gunParticles.Play ();

		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);

		// shootRay.origin = transform.position;
		// shootRay.direction = transform.forward;

		Vector3 vec = new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0.0f);
		Ray camRay = Camera.main.ScreenPointToRay (vec);

		if (Physics.Raycast (camRay, out shootHit, range, shootableMask))
		{
			EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth> ();
			if (enemyHealth != null)
			{
				enemyHealth.TakeDamage (damagePerShot, shootHit.point);
			}
			else if(shootHit.collider.GetComponent<EnemyHead> () != null)
			{
				enemyHealth = shootHit.collider.GetComponentInParent<EnemyHealth> ();
				enemyHealth.TakeDamage(damagePerHeadShot, shootHit.point);
			}
				
			gunLine.SetPosition (1, shootHit.point);
		}
		else
		{
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}
}
