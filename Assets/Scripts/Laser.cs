﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Defines behavior for the laser the player shoots 
public class Laser : MonoBehaviour
{

  [SerializeField]
  //speed the laser travels (8 meter/second)
  // It is good practice to make the variable a float and not an int, in case another developer wants to change this value to a decimal for testing purposes
  private float _speed = 8.0f;


  // Update is called once per frame
  void Update()
  {

    // Move the laser up
    // getting the laser's y position with transform.position.y and just adding the speed variable continuously may have worked, but using Vector3.up is a cleaner solution.
    // We use Vector3.up when we want to make the laser move up (the direction the laer will move, not an actual coordinate)
    transform.Translate(Vector3.up * _speed * Time.deltaTime);

    // if the laser's y position is greater than 8 (it'll be offscreen by then)
    // destroy the laser object (otherwise it'll keep going up and using more space on the computer)

    if (transform.position.y > 8.0f)
    {
      // Destroy() will destroy the gameobject you pass in. Since we want to destroy the game object this script is attached to, we can say this.gameObject, b/c "gameObject" is a reserved word for the game object this script is attached to. So there's no need for a GameObject variable.
      // We could also omit the "this" part but this way is more clear
      // Also, as your intellisense tells you, you can pass in a second argument, the amount of time until the object will be destroyed. If you want the game object destroyed in 5 seconds regardless of where it is, pass in 5.0f
      Destroy(this.gameObject);
    }



  }
}