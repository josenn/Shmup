/* This class makes sure our music player is a singleton
 * 
 * The method I commented out is supposed to check for more than 1 music player but there is only 1
 * 
 * Music Player (located in the start scene) I'm keeping it here as an example
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerSingleton : MonoBehaviour
{
    private void Awake()
    {
        //SetUpSingleton();

        DontDestroyOnLoad(gameObject);
    }

    /*private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }*/
}
