using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  [SerializeField]
  private bool _isGameOver; // false by default

  public bool isCoopMode = false;
  // private SpawnManager _spawnManager;
  private Scene currentScene;
  private void Start () {
    // check which Scene we're in by index, and if we are in Coop Mode change bool to true

    currentScene = SceneManager.GetActiveScene ();

    if (currentScene == null) {
      Debug.LogError ("Scene is NULL ::GameManager::Start()");
    }

    int sceneIndex = currentScene.buildIndex;
    if (sceneIndex == 2) {
      isCoopMode = true;
    }

    // _spawnManager = GameObject.Find ("Spawn_Manager").GetComponent<SpawnManager> ();

  }

  private void Update () {

    // if the r key is pressed when game is over in single layer mode, restart the game by reloading the current scene
    if (Input.GetKeyDown (KeyCode.R) && _isGameOver == true && isCoopMode == false) {
      // we can reload the scene by it's number or it's name. We named our game "Game" as you can see in the "Scenes" folder in Unity or by it's index (which you can see by adding the Scene to your Build Settings in Unity)
      SceneManager.LoadScene (1);

      // else if we're in Coop mode, reload the Coop scene 
    } else if (Input.GetKeyDown (KeyCode.R) && _isGameOver == true && isCoopMode == true) {
      SceneManager.LoadScene (2); // 

    }

    // if ESCAPE key is pressed, quit the application
    if (Input.GetKeyDown (KeyCode.Escape)) {
      // This won't work in the editor, so you have to make a new build to test this out. It should allow you to exit fullscreen
      // There is a way to quit it in the editor but is pointless if you are going to upload your game somewhere anyways (b/c it won't work on where you upload to)
      Application.Quit ();
    }
  }

  public void GameOver () {
    _isGameOver = true;
  }

}