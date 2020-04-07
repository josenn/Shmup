/* This class controls loading various scenes
 * 
 * It has functions for loading the start menu, the main game, and the game over screen as well as enemy data, ship data, and a tutorial
 * 
 * I might add more levels in the future
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 3.0f;

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
        FindObjectOfType<GameSession>().ResetGame();
    }

    // Loads the game over screen
    public void LoadGameOver()
    {
        StartCoroutine(GameOver());
    }

    // Wait and then load the game over scene
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(delayInSeconds);

        SceneManager.LoadScene("Game Over");
    }

    // Should this be a new scene or could I do another UI element? For now scene seems to be the easiest for my level
    public void LoadShipData()
    {
        // Load ship data
    }

    public void LoadEnemyData()
    {
        // Load enemy data
    }

    public void LoadTutorial()
    {
        // Load tutorial
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
