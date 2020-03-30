/* This class represents a player
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
    [Tooltip("The sound that will be made when a player is destroyed")]
    [SerializeField] AudioClip playerDestroyClip;
    [Tooltip("The sound that will be made when a player is hit")]
    [SerializeField] AudioClip playerHitClip;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

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

        // TODO lots of refactoring and commenting
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
            // Creating this as a gameobject
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
            Destroy(gameObject);
            TriggerPlayerDestroySound();
        }
    }

    private void TriggerPlayerHitSound()
    {
        if (health > 0)
        {
            AudioSource.PlayClipAtPoint(playerHitClip, transform.position);

        }
    }

    private void TriggerPlayerDestroySound()
    {
        AudioSource.PlayClipAtPoint(playerDestroyClip, transform.position);
    }
}
