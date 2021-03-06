﻿/* This class represents a player
 * 
 * A player has a move speed, health, projectile speed, how often the projectile fires, controls, etc etc
 * 
 * Also has a function to keep the player within the boundaries of the play area
 * 
 * More explanation will be given on a function to function basis (lot to unpack here)
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // The header attribute adds a header above the variables to keep them better organized!
    // Information about the player
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    // Cached references
    [SerializeField] GameObject laserPrefab;

    // Projectile values
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    // Information about audio that is played when certain things happen to the player
    [Header("Audio")]
    [Tooltip("The sound that will be made when the player is destroyed")]
    [SerializeField] AudioClip playerIsDestroyedClip;
    [Tooltip("The volume of the player destroy clip")] [RangeAttribute(0.0f, 1.0f)]
    [SerializeField] float playerIsDestroyedClipVolume;

    [Tooltip("The sound that will be made when the player is hit")]
    [SerializeField] AudioClip playerIsHitClip;
    [Tooltip("The volume of the player hit clip")] [RangeAttribute(0.0f, 1.0f)]
    [SerializeField] float playerIsHitClipVolume;

    Coroutine firingCoroutine;

    float xMin, xMax, yMin, yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    // Might need to change and merge these 2 later who knows
    private void Move()
    {
        // Delta is referring to a change in where we are and where we want to be
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp((transform.position.x + deltaX), xMin, xMax);
        var newYPos = Mathf.Clamp((transform.position.y + deltaY), yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        // Protects against potential null references where the data may not exist
        if (!damageDealer) { return; }

        TriggerPlayerHitSound();

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        // Destroy the laser
        damageDealer.Hit();
        
        DestroyPlayer();
    }

    // When the player's health is low enough destroy them
    private void DestroyPlayer()
    {
        if (health <= 0)
        {
            // Load game over screen
            FindObjectOfType<LevelLoader>().LoadGameOver();
            Destroy(gameObject);
            TriggerPlayerDestroySound();
        }
    }

    // Play the sound that will be made when the player is hit
    private void TriggerPlayerHitSound()
    {
        if (health > 0)
        {
            AudioSource.PlayClipAtPoint(playerIsHitClip, Camera.main.transform.position, playerIsHitClipVolume);
        }
    }

    // Play the sound that will be made when the player is destroyed
    private void TriggerPlayerDestroySound()
    {
        AudioSource.PlayClipAtPoint(playerIsDestroyedClip, Camera.main.transform.position, playerIsDestroyedClipVolume);
    }

    public int GetPlayerHealth()
    {
        return health;
    }
}
