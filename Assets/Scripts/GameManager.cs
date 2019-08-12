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
      SceneManager.LoadScene(0); // Current Game Scene
    }
  }

  public void GameOver()
  {
    _isGameOver = true;
  }
}
