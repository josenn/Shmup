/* This script is the damage dealer of our game
 * 
 * It's pretty simple really
 * 
 * This class has a certain amount of damage that can be dealt
 * 
 * A function to get said damage (so other classes can know how much damage to take)
 * 
 * And a function to process the hit of an object
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 0;

    public int GetDamage() { return damage;  }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
