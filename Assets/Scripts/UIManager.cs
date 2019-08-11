using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // used to communicate with UI elements in Unity

public class UIManager : MonoBehaviour
{
  // we needed to bring in UnityEngine.UI (above) to have the Text variable since it's a UI element
  [SerializeField]
  private Text _scoreText;
  // get a handle to text
  // Start is called before the first frame update

  // The actual image field that you will use to alternate between the 4 live sprites. (The Lives_Display_img in the Canvas)
  [SerializeField]
  private Image _LivesImg;
  // Sprites to alternate between depending on how many lives left (assign the sprites in the Inspector in Unity by changing the size of this field to 4 and dragging & dropping the 4 sprites)
  [SerializeField]
  private Sprite[] _liveSprites;
  void Start()
  {
    // assign text component to the handle so when game starts, there is a score that can be updated
    _scoreText.text = "Score: " + 0;
  }

  // Update is called once per frame
  public void UpdateScore(int playerScore)
  {
    // don't forget to put ".text" b/c you can't assign anything to just _scoreText
    _scoreText.text = "Score: " + playerScore;
  }

  public void UpdateLives(int currentLives)
  {
    // access display image sprite and give it a new one based on currentLives
    _LivesImg.sprite = _liveSprites[currentLives];
  }
}
