﻿/* This class represents enemies in the game
 * 
 * Enemies have health, a speed at which their projectiles move, and time between the next shot
 * 
 * Which is a follow between a minTimeBetweenShots and a MaxTimeBetweenShots
 * 
 * They have the ability to fire as well as the ability to be hit and be destroyed
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Information about the enemy
    [Header("Enemy")]
    [SerializeField] float health = 1000F;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float shotTimer;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;

    [Header("Projectile")]
    // References
    [SerializeField] GameObject laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        shotTimer = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
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
        shotTimer = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Fire()
    {
        // Creating this as a gameobject
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    // "Other" means the game object that collided with this thing
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; } // More tidy to keep braces on this line in this case
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        damageDealer.Hit();

        // To fix cases of going into the negatives by mistake we add the < sign
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
