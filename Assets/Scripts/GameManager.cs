using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  [SerializeField]
  private bool _isGameOver; // false by default

  private void Update()
  {
    // if the r key is pressed when game is over, restart the game by restarting the current scene
    if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
    {
      // we can reload the scene by it's number or it's name. We named our game "Game" as you can see in the "Scenes" folder in Unity or by it's index (which you can see by adding the Scene to your Build Settings in Unity)
      SceneManager.LoadScene(1); // Current Game Scene
    }

    // if ESCAPE key is pressed, quit the application
    if (Input.GetKeyDown(KeyCode.LeftShift))
    {
      // This won't work in the editor, so you have to make a new build to test this out. It should allow you to exit fullscreen
      // There is a way to quit it in the editor but is pointless if you are going to upload your game somewhere anyways (b/c it won't work on where you upload to)
      Application.Quit();
    }
  }

  public void GameOver()
  {
    _isGameOver = true;
  }
}
