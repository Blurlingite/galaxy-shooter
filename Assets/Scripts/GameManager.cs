using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  [SerializeField]
  private bool _isGameOver; // false by default

  private bool _isGamePaused = false;

  public bool isCoopMode = false;
  // private SpawnManager _spawnManager;
  private Scene currentScene;

  [SerializeField]
  private GameObject _pauseMenuPanel;

  private void Start()
  {
    // check which Scene we're in by index, and if we are in Coop Mode change bool to true

    // When game starts let everything run at normal speed by setting Time.timeScale to 1.0f
    // This fixed an erro where starting a game from the main menu would make the game freeze b/c I set Time.timeScale to 0 somewhere else in my code. If I had never used Time.timeScale this error would not occur, but I needed to use it to enable pausing of the game
    Time.timeScale = 1.0f;
    currentScene = SceneManager.GetActiveScene();

    if (currentScene == null)
    {
      Debug.LogError("Scene is NULL ::GameManager::Start()");
    }

    int sceneIndex = currentScene.buildIndex;
    if (sceneIndex == 2)
    {
      isCoopMode = true;
    }


  }

  private void Update()
  {

    // if the r key is pressed when game is over in single layer mode, restart the game by reloading the current scene
    if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true && isCoopMode == false)
    {
      // we can reload the scene by it's number or it's name. We named our game "Game" as you can see in the "Scenes" folder in Unity or by it's index (which you can see by adding the Scene to your Build Settings in Unity)
      SceneManager.LoadScene(1);

      // else if we're in Coop mode, reload the Coop scene 
    }
    else if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true && isCoopMode == true)
    {
      SceneManager.LoadScene(2); // 

    }

    // if ESCAPE key is pressed, quit the application
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      // This won't work in the editor, so you have to make a new build to test this out. It should allow you to exit fullscreen
      // There is a way to quit it in the editor but is pointless if you are going to upload your game somewhere anyways (b/c it won't work on where you upload to)
      Application.Quit();
    }

    // if P key is pressed while unpaused, pause the game & enable pause menu. If pressed while paused, unpause & diable pause menu

    if (Input.GetKeyDown(KeyCode.P))
    {

      if (_isGamePaused == false)
      {

        Pause();

      }
      else if (_isGamePaused == true)
      {
        Unpause();

      }

    }

  }

  public void GameOver()
  {
    _isGameOver = true;
  }

  private void Pause()
  {
    // controls the speed at which the game is running. if set to 0, the everything (that is frame dependent) will freeze. If set to a number between 0 and 1, the game will be in slow motion
    _isGamePaused = true;
    Time.timeScale = 0f;
    _pauseMenuPanel.SetActive(true);
  }

  // this is public, so we can also press the resume button to unpause the game
  public void Unpause()
  {
    _isGamePaused = false;
    Time.timeScale = 1.0f;
    _pauseMenuPanel.SetActive(false);
  }

  public void returnToMainMenu()
  {
    SceneManager.LoadScene(0);
  }

  public bool getIsCoopMode()
  {
    return isCoopMode;
  }

}