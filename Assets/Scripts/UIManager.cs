using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // used to communicate with UI elements in Unity

public class UIManager : MonoBehaviour
{
  // we needed to bring in UnityEngine.UI (above) to have the Text variable since it's a UI element
  [SerializeField]
  private Text _scoreText, _bestScoreText;
  // get a handle to text
  // Start is called before the first frame update

  private int _bestScore;

  // The actual image field that you will use to alternate between the 4 live sprites. (The Lives_Display_img in the Canvas)
  [SerializeField]
  private Image _LivesImg;


  [SerializeField]
  private Image _P2LivesImg;


  // used to get the index of 4 to show the "more than 3 lives" sprite in _liveSprites without having to set the currentlives to 4 (If you had 7 lives, it would be set to 4)
  private int multiLives;

  // Sprites to alternate between depending on how many lives left (assign the sprites in the Inspector in Unity by changing the size of this field to 4 and dragging & dropping the 4 sprites)
  [SerializeField]
  private Sprite[] _liveSprites;

  [SerializeField]
  private Text _gameOverText;

  [SerializeField]
  private Text _restartText;

  private GameManager _gameManager;

  [SerializeField]
  private Text multiLivesText;

  [SerializeField]
  Player player1;

  [SerializeField]
  Player player2;


  void Start()
  {

    // assign text component to the handle so when game starts, there is a score that can be updated
    _scoreText.text = "Score: " + 0;
    // set this object to false to not display "Game Over" at the start of the game. Since _gameOverText is not a GameObject type, we must put .gameObject first

    // Retrieve the best score that was saved under the key "HighScore" (in CheckForBestScore()). If it's the first game and there is none saved, we default it to 0 by passing that as the 2nd argument
    _bestScore = PlayerPrefs.GetInt("HighScore", 0);

    // actually assign the text field the loaded best score
    _bestScoreText.text = "Best: " + _bestScore;

    _gameOverText.gameObject.SetActive(false);

    _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

    if (_gameManager == null)
    {
      Debug.LogError("Game Manager is NULL");
    }

  }

  // Update is called once per frame
  public void UpdateScore(int playerScore)
  {
    // don't forget to put ".text" b/c you can't assign anything to just _scoreText
    _scoreText.text = "Score: " + playerScore;
  }

  public void CheckForBestScore(int currentScore)
  {
    if (currentScore > _bestScore)
    {
      _bestScore = currentScore;

      // use PlayerPrefs to save the score under the key "HighScore" You will use that key when you want to load the data (best score)
      PlayerPrefs.SetInt("HighScore", _bestScore);
      _bestScoreText.text = "Best: " + _bestScore;

    }
  }

  public void UpdateLives(int currentLives, string playerName)
  {
    if (playerName == "Player_1")
    {
      displayLives(currentLives, _LivesImg);

    }
    else if (playerName == "Player_2")
    {
      displayLives(currentLives, _P2LivesImg);
    }

  }

  // all code for a Game Over is in here
  void GameOverSequence()
  {
    // If we are in Single player mode 
    if (_gameManager.getIsCoopMode() == false && player1.getLivesLeft() <= 0)
    {
      // notify Game Manager that the game is over so it can trigger a restart when player presses the "R" key
      _gameManager.GameOver();
      _gameOverText.gameObject.SetActive(true);
      // Show restart text
      _restartText.gameObject.SetActive(true);

      StartCoroutine(GameOverFlicker());
    }
    else if (_gameManager.getIsCoopMode() == true && player1.getLivesLeft() < 1 && player2.getLivesLeft() < 1)
    {

      _gameManager.GameOver();
      _gameOverText.gameObject.SetActive(true);
      _restartText.gameObject.SetActive(true);

      StartCoroutine(GameOverFlicker());

    }


  }

  // causes the flickering Game Over effect
  IEnumerator GameOverFlicker()
  {

    while (true)
    {
      // turn off 
      _gameOverText.gameObject.SetActive(false);

      // wait half a second
      yield return new WaitForSeconds(0.5f);

      // turn on 
      _gameOverText.gameObject.SetActive(true);

      // wait half a second (the flicker won't work if there's no wait time here so don't forget to add it )
      yield return new WaitForSeconds(0.5f);

    }
  }

  public void ResumeGame()
  {
    _gameManager.Unpause();
  }

  public void backToMainMenu()
  {
    _gameManager.returnToMainMenu();
  }

  private void displayLives(int amountOfLives, Image spriteReference)
  {

    if (amountOfLives < 0)
    {
      amountOfLives = 0; // prevent negative lives
    }

    // when out of lives, GameOverSequence() will display game over text by setting it's object active. This code should NOT go in the Player script b/c displaying UI things is the UI Manager's job
    else if (amountOfLives < 1)
    {
      GameOverSequence();
    }

    // Sprite Assignment

    if (amountOfLives > 4)
    {
      multiLives = 4; // so we get the correct sprite at index 4 to represent more than 3 lives

      // show multiple lives sprite
      spriteReference.sprite = _liveSprites[multiLives];
      // set multiple lives text active
      multiLivesText.gameObject.SetActive(true);
      // Assign amount of lives
      multiLivesText.text = "x " + amountOfLives;

    }
    else
    {
      // access display image sprite and give it a new one based on currentLives
      spriteReference.sprite = _liveSprites[amountOfLives];
    }

  }

}