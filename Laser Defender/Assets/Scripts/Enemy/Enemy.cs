﻿/* This class represents enemies in the game
 * 
 * Enemies have health, a speed at which their projectiles move, and time between the next shot
 * 
 * Which is a follow between a minTimeBetweenShots and a MaxTimeBetweenShots
 * 
 * They have the ability to fire as well as the ability to be hit and be destroyed
 * 
 * Thay also have a certain amount of points that they give the player when destroyed
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Information about the enemy
    [Header("Enemy Stats")]
    [SerializeField] float health = 1000f;
    [SerializeField] int scoreValue = 100;

    [Header("Projectile")]
    // References
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 5.0f;
    [SerializeField] float shotTimer;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;

    [Header("Particle Effect")]
    [SerializeField] GameObject particleEffectPrefab;
    [SerializeField] float durationOfExplosion = 1.0f;

    // Information about audio that is played when certain things happen to an enemy (the tooltips pretty much say it all)
    [Header("Audio")]
    [Tooltip("The sound that will be played when an enemy is destroyed")]
    [SerializeField] AudioClip enemyIsDestroyedClip;
    [Tooltip("The volume of the enemy destroy clip")] [RangeAttribute(0.0f, 1.0f)] // Because AudioSource volume is in the range 0.0-1.0
    [SerializeField] float enemyIsDestroyedClipVolume;

    [Tooltip("The sound that will be made when the enemy shoots")]
    [SerializeField] AudioClip enemyShootClip;
    [Tooltip("The volume of the enemy shoot clip")] [RangeAttribute(0.0f, 1.0f)]
    [SerializeField] float enemyShootClipVolume;

    [Tooltip("he sound that will be made when an enemy is hit by a player")]
    [SerializeField] AudioClip enemyIsHitClip;
    [Tooltip("The volume of the enemy hit clip")] [RangeAttribute(0.0f, 1.0f)]
    [SerializeField] float enemyIsHitClipVolume;

    // Start is called before the first frame update
    void Start()
    {
        ShotTimerReset();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        // Our shot counter needs to be going down for however long our frame takes
        shotTimer -= Time.deltaTime; // The time that our frame takes

        if (shotTimer <= 0f)
        {
            Fire();
            ShotTimerReset();
        }
        
    }

    private void ShotTimerReset()
    {
        shotTimer = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Fire()
    {
        // Creating this as a gameobject
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        TriggerEnemyShootSound();
    }

    // "Other" means the game object that collided with this thing
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        
        if(!damageDealer) { return; } // More tidy to keep braces on this line in this case

        TriggerEnemyHitSound();

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        damageDealer.Hit();

        DestroyEnemy();
    }

    // When the enemy's health is low enough destroy them
    private void DestroyEnemy()
    {
        // To fix cases of going into the negatives by mistake we add the < sign
        if (health <= 0)
        {
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
            Destroy(gameObject);
            TriggerParticleEffect();
            TriggerEnemyDestroySound();
        }
    }

    // Creates the particle effect that is triggered when an enemy is destroyed
    private void TriggerParticleEffect()
    {
        GameObject explosion = Instantiate(particleEffectPrefab, transform.position, transform.rotation);

        Destroy(explosion, durationOfExplosion);
    }

    // Play the sound made when an enemy shoots
    private void TriggerEnemyShootSound()
    {
        AudioSource.PlayClipAtPoint(enemyShootClip, Camera.main.transform.position, enemyShootClipVolume);
    }

    // Plays the sound made when an enemy is destroyed
    private void TriggerEnemyDestroySound()
    {
        AudioSource.PlayClipAtPoint(enemyIsDestroyedClip, Camera.main.transform.position, enemyIsDestroyedClipVolume);
    }

    // Plays the sound made when an enemy is hit
    private void TriggerEnemyHitSound()
    {
        // Don't play the hit sound if the enemy is destroyed by a hit
        if(health > 0)
        {
            // AudioClips can be softer or louder depending on their distance from the audio listener?
            AudioSource.PlayClipAtPoint(enemyIsHitClip, Camera.main.transform.position, enemyIsHitClipVolume);
        }
    }
}
