/* This class controls loading various scenes
 * 
 * It has functions for loading the start menu, the main game, and the game over screen
 * 
 * I might add more levels in the future
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Loads the start menu
  public void LoadStartMenu()
    {
        // This isn't very scalable and can mess up if done wrong so be careful (I'll make it more scalable later)
        // The start menu is always at index 0
        SceneManager.LoadScene(0);
    }

    // Loads the game
    public void LoadGame()
    {
        // This is just an example of another way to load a scene
        // Normally we don't like string references because if we change the name things will mess up
        SceneManager.LoadScene("Game");

        //Debug.Log("Start Button pressed!");
    }

    // Loads the game over screen
    public void LoadGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    // Quits the game
    public void QuitGame()
    {
        // Note this doesn't work when playing through the editor
        // I'll have to make a build to test it
        Application.Quit();

        //Debug.Log("Quit button pressed!");
    }
}
