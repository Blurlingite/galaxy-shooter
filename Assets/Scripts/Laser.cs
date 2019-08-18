using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Defines behavior for the laser the player shoots 
public class Laser : MonoBehaviour {

  [SerializeField]
  //speed the laser travels (8 meter/second)
  // It is good practice to make the variable a float and not an int, in case another developer wants to change this value to a decimal for testing purposes
  private float _speed = 8.0f;

  private Vector3 direction;

  private bool _isEnemyLaser = false;

  private bool _isDirectionReversed = false;

  // Update is called once per frame
  void Update () {

    if (_isEnemyLaser == false) {
      MoveUp ();
    } else {
      if (_isDirectionReversed == false) {
        MoveDown ();

      } else {
        MoveUp ();
      }
    }

  }

  void MoveUp () {
    // Move the laser up
    // getting the laser's y position with transform.position.y and just adding the speed variable continuously may have worked, but using Vector3.up is a cleaner solution.
    // We use Vector3.up when we want to make the laser move up (the direction the laer will move, not an actual coordinate)
    direction = Vector3.up;
    transform.Translate (direction * _speed * Time.deltaTime);

    // if the laser's y position is greater than 8 (it'll be offscreen by then)
    // destroy the laser object (otherwise it'll keep going up and using more space on the computer)

    if (transform.position.y > 8.0f) {
      // check if laser has a parent and if it does destroy the parent too

      if (transform.parent != null) {
        // Destroy() will destroy the gameobject you pass in. We want to destroy the parent object. First we get the parent with transform.parent and then we get the actual parent object with .gameObject. 
        Destroy (transform.parent.gameObject);
      }

      // Destroy() will destroy the gameobject you pass in. Since we want to destroy the game object this script is attached to, we can say this.gameObject, b/c "gameObject" is a reserved word for the game object this script is attached to. So there's no need for a GameObject variable.
      // We could also omit the "this" part but this way is more clear
      // Also, as your intellisense tells you, you can pass in a second argument, the amount of time until the object will be destroyed. If you want the game object destroyed in 5 seconds regardless of where it is, pass in 5.0f
      Destroy (this.gameObject);
    }
  }

  void MoveDown () {
    // Move the laser up
    // getting the laser's y position with transform.position.y and just adding the speed variable continuously may have worked, but using Vector3.up is a cleaner solution.
    // We use Vector3.up when we want to make the laser move up (the direction the laer will move, not an actual coordinate)

    direction = Vector3.down;
    transform.Translate (direction * _speed * Time.deltaTime);

    // if the laser's y position is greater than 8 (it'll be offscreen by then)
    // destroy the laser object (otherwise it'll keep going up and using more space on the computer)

    if (transform.position.y < -8.0f) {
      // check if laser has a parent and if it does destroy the parent too

      if (transform.parent != null) {
        // Destroy() will destroy the gameobject you pass in. We want to destroy the parent object. First we get the parent with transform.parent and then we get the actual parent object with .gameObject. 
        Destroy (transform.parent.gameObject);
      }

      // Destroy() will destroy the gameobject you pass in. Since we want to destroy the game object this script is attached to, we can say this.gameObject, b/c "gameObject" is a reserved word for the game object this script is attached to. So there's no need for a GameObject variable.
      // We could also omit the "this" part but this way is more clear
      // Also, as your intellisense tells you, you can pass in a second argument, the amount of time until the object will be destroyed. If you want the game object destroyed in 5 seconds regardless of where it is, pass in 5.0f
      Destroy (this.gameObject);
    }
  }

  public void AssignEnemyLaser () {
    _isEnemyLaser = true;
  }

  // check if laser collided with player (enemy's laser) and damage the player if yes
  void OnTriggerEnter2D (Collider2D other) {
    if (other.gameObject.CompareTag ("Player") && _isEnemyLaser == true) {
      Player player = other.GetComponent<Player> ();

      if (player.getIsReflectorActive () == true) {
        _isDirectionReversed = true;
      }

      // if (player.getIsReflectorActive () == true) {
      //   Debug.Log ("YES");
      //   Vector3 reverseDirection = new Vector3 (0, 0, 0);

      //   if (direction == Vector3.down) {
      //     direction = Vector3.up;
      //   }

      //   transform.Translate (reverseDirection * _speed * Time.deltaTime);

      // }
      if (player != null) {
        player.Damage ();
      }

    }

  }

}