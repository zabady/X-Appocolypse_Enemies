using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 10;
    public int currentHealth;
    public AudioClip hurtClip;
    public AudioClip deathClip;
    // public float flashSpeed = 5f;
    // public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    FirstPersonController playerMovement;
    // PlayerShooting playerShooting;
    public bool isDead;
    bool damaged;

    void Awake ()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<FirstPersonController>();
        // playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
	}
	
	void Update ()
    {
        // if (damaged) { }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        playerAudio.clip = hurtClip;
        playerAudio.Play();

        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }

    }

    void Death()
    {
        isDead = true;

        playerAudio.clip = deathClip;
        playerAudio.Play();

        // playerMovement.enabled = false;
        // playerShooting.enabled = false;
        // playerShooting.DisableEffects();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
