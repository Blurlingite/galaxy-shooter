using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
  public void LoadGame()
  {
    // Start the game by loading the Game scene (which now has an index of 1 after re-dropping it into the Build Settings in Unity)
    SceneManager.LoadScene(1);
  }
}
