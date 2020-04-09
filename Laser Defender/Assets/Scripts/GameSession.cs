/* This class controls game elements such as the player's score
 * 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    // Information about the game session
    // Current Score
    int score = 0;

    // Awake is the very first function in the execution order
    private void Awake()
    {
        SetUpSingleton();
    }

    // This manner of singleton that I keep implementing could be a nice code snippet
    // TODO makes this a code snippet and store it somewhere
    private void SetUpSingleton()
    {
        // The number of game sessions in the scene
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;

        // If there is more than 1 game session destroy all other game sessions or don't destroy yourself
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Gets the score
    public int GetScore()
    {
        return score;
    }
    
    // Adds scoreValue to the score
    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    // Resets the game
    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
