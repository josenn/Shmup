/* This class makes sure our music player is a singleton
 * 
 * If there is more than one music player delete it and keep yourself
 * 
 * There will always be more than one because there is one in every scene for testing purposes
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerSingleton : MonoBehaviour
{
    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
