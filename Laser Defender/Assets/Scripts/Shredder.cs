/* This class is used to shred excess objects (mainly enemy and player projectiles)
 * 
 * If an object has surpassed the play area it gets destroyed
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
