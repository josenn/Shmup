/* This class controls the rotation of certain enemy projectiles */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float speedOfSpin = 1f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the projectile (note in 2-D space z is the rotation we want)
        transform.Rotate(0, 0, speedOfSpin * Time.deltaTime);
    }
}
